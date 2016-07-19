﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Raw;
using System.IO;
using RCPA.Utils;
using RCPA.Proteomics.Summary;
using System.Threading.Tasks;
using System.Threading;
using RCPA.Proteomics.Mascot;
using System.Xml.Linq;
using RCPA.R;
using System.Collections.Concurrent;

namespace RCPA.Proteomics.Quantification.Labelfree
{
  public class ChromatographProfileBuilder : AbstractThreadProcessor
  {
    private class FullMS
    {
      public int Scan { get; set; }
      public double RetentionTime { get; set; }
      public PeakList<Peak> Peaks { get; set; }
    }

    private ChromatographProfileBuilderOptions options;

    private IObservedProfileFinder profileFinder = new ObservedProfileOptimizationFinder();

    private static string BoundaryR = Path.Combine(FileUtils.GetTemplateDir().FullName, "ProfileChroBoundary.r");
    private static string ImageR = Path.Combine(FileUtils.GetTemplateDir().FullName, "ProfileChroLabelfree.r");

    public ChromatographProfileBuilder(ChromatographProfileBuilderOptions options)
    {
      this.options = options;
    }

    public override IEnumerable<string> Process()
    {
      if (!File.Exists(options.OutputFile) || options.Overwrite)
      {
        var format = GetPeptideReader();
        var spectra = format.ReadFromFile(options.InputFile);
        var peptideMap = spectra.ToGroupDictionary(m => m.Query.FileScan.Experimental.ToLower());
        var rawfiles = Directory.GetFiles(options.RawDirectory, "*.raw", SearchOption.AllDirectories).ToDictionary(m => Path.GetFileNameWithoutExtension(m).ToLower());
        var rententionWindow = options.RetentionTimeWindow;

        var missed = peptideMap.Keys.Except(rawfiles.Keys).ToArray();
        if (missed.Length > 0)
        {
          throw new Exception(string.Format("Cannot find raw file of {0} in directory {1}", missed.Merge("/"), options.RawDirectory));
        }

        var optionThreadCount = options.ThreadCount == 0 ? Environment.ProcessorCount : options.ThreadCount;
        var option = new ParallelOptions()
        {
          MaxDegreeOfParallelism = Math.Min(optionThreadCount, peptideMap.Count),
        };

        var chroMap = new List<Tuple<string, List<ChromatographProfile>>>();
        foreach (var raw in peptideMap)
        {
          var peptides = raw.Value;

          var waitingPeaks = new List<ChromatographProfile>();
          foreach (var peptide in peptides)
          {
            var chro = new ChromatographProfile()
            {
              Experimental = peptide.Query.FileScan.Experimental,
              IdentifiedScan = peptide.Query.FileScan.FirstScan,
              IdentifiedRetentionTime = peptide.Query.FileScan.RetentionTime,
              ObservedMz = peptide.GetPrecursorMz(),
              TheoreticalMz = peptide.GetTheoreticalMz(),
              Charge = peptide.Query.Charge,
              Sequence = peptide.Peptide.PureSequence,
              FileName = GetTargetFile(peptide),
              SubFileName = GetTargetSubFile(peptide)
            };
            chro.InitializeIsotopicIons(options.MzTolerancePPM, options.MinimumIsotopicPercentage);
            waitingPeaks.Add(chro);
          }

          chroMap.Add(new Tuple<string, List<ChromatographProfile>>(raw.Key, waitingPeaks));
        }

        ConcurrentBag<ChromatographProfile> detected = new ConcurrentBag<ChromatographProfile>();

        Parallel.ForEach(chroMap, option, raw =>
        {
          var rawFileName = raw.Item1;
          var waitingPeaks = raw.Item2;

          Dictionary<string, List<ChromatographProfile>> resultMap = new Dictionary<string, List<ChromatographProfile>>();

          List<FullMS> fullMSList = new List<FullMS>();
          Progress.SetMessage("Reading full ms list from " + rawfiles[rawFileName] + "...");
          using (var rawReader = new CacheRawFile(RawFileFactory.GetRawFileReader(rawfiles[rawFileName])))
          {
            var firstScan = rawReader.GetFirstSpectrumNumber();
            var lastScan = rawReader.GetLastSpectrumNumber();
            for (int scan = firstScan; scan <= lastScan; scan++)
            {
              var mslevel = rawReader.GetMsLevel(scan);
              if (mslevel == 1)
              {
                fullMSList.Add(new FullMS()
                {
                  Scan = scan,
                  RetentionTime = rawReader.ScanToRetentionTime(scan),
                  Peaks = null
                });
              }
            }

            foreach (var chro in waitingPeaks)
            {
              if (chro.IdentifiedScan == 0 && chro.IdentifiedRetentionTime > 0)
              {
                for (int i = 1; i < fullMSList.Count; i++)
                {
                  if (chro.IdentifiedRetentionTime < fullMSList[i].RetentionTime)
                  {
                    break;
                  }
                  chro.IdentifiedScan = fullMSList[i].Scan + 1;
                }
              }
            }

            var chroGroups = waitingPeaks.GroupBy(chro => chro.GetPeptideId());
            foreach (var chroGroup in chroGroups)
            {
              List<ChromatographProfile> profileChros = new List<ChromatographProfile>();
              foreach (var chro in chroGroup.OrderBy(m => m.IdentifiedScan))
              {
                var masterScanIndex = 0;
                for (int i = 1; i < fullMSList.Count; i++)
                {
                  if (chro.IdentifiedScan < fullMSList[i].Scan)
                  {
                    break;
                  }
                  masterScanIndex = i;
                }
                var masterScan = fullMSList[masterScanIndex].Scan;
                var masterRetentionTime = fullMSList[masterScanIndex].RetentionTime;

                bool bExist = false;
                foreach (var profileChro in profileChros)
                {
                  foreach (var pkl in profileChro.Profiles)
                  {
                    if (pkl.Scan == fullMSList[masterScanIndex].Scan)
                    {
                      pkl.Identified = true;
                      bExist = true;
                      break;
                    }
                  }

                  if (bExist)
                  {
                    break;
                  }
                }

                if (bExist)
                {
                  continue;
                }

                //Progress.SetMessage("Processing {0} : {1:0.#####} : {2} : {3}", chro.Sequence, chro.ObservedMz, chro.IdentifiedScan, Path.GetFileName(chro.FileName));

                for (int scanIndex = masterScanIndex; scanIndex >= 0; scanIndex--)
                {
                  if (Progress.IsCancellationPending())
                  {
                    throw new UserTerminatedException();
                  }

                  var curRetentionTime = fullMSList[scanIndex].RetentionTime;
                  if (masterRetentionTime - curRetentionTime > rententionWindow)
                  {
                    break;
                  }

                  if (!AddEnvelope(chro, rawReader, fullMSList, scanIndex))
                  {
                    break;
                  }

                  if (scanIndex == masterScanIndex)
                  {
                    chro.Profiles.Last().Identified = true;
                  }
                }
                chro.Profiles.Reverse();

                for (int scanIndex = masterScanIndex + 1; scanIndex < fullMSList.Count; scanIndex++)
                {
                  if (Progress.IsCancellationPending())
                  {
                    throw new UserTerminatedException();
                  }

                  var curRetentionTime = fullMSList[scanIndex].RetentionTime;
                  if (curRetentionTime - masterRetentionTime > rententionWindow)
                  {
                    break;
                  }

                  if (!AddEnvelope(chro, rawReader, fullMSList, scanIndex))
                  {
                    break;
                  }
                }

                profileChros.Add(chro);
              }

              profileChros.RemoveAll(l => l.Profiles.Count < options.MinimumScanCount);
              profileChros.Sort((m1, m2) => m2.Profiles.Count.CompareTo(m1.Profiles.Count));

              bool bMain = true;
              foreach (var chro in profileChros)
              {
                var filename = bMain ? chro.FileName : chro.SubFileName;
                if (bMain)
                {
                  detected.Add(chro);
                }

                bMain = false;

                new ChromatographProfileTextWriter().WriteToFile(filename, chro);
                new ChromatographProfileXmlFormat().WriteToFile(filename + ".xml", chro);
              }
            }
          }
        }
        );

        var chroList = new List<ChromatographProfile>(detected);
        chroList.Sort((m1, m2) => m1.FileName.CompareTo(m2.FileName));

        if (chroList.Count == 0)
        {
          throw new Exception("Cannot find chromotograph!");
        }

        var boundaryInput = Path.ChangeExtension(options.OutputFile, ".chros.tsv");
        using (var sw = new StreamWriter(boundaryInput))
        {
          sw.WriteLine("ChroDirectory\tChroFile\tSample\tPeptideId\tTheoreticalMz\tCharge\tIdentifiedScan");
          foreach (var chro in chroList)
          {
            sw.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}",
              Path.GetDirectoryName(chro.FileName).Replace("\\", "/"),
              Path.GetFileNameWithoutExtension(chro.FileName),
              chro.Experimental,
              chro.GetPeptideId(),
              chro.TheoreticalMz,
              chro.Charge,
              chro.IdentifiedScan);
          }
        }

        //var boundaryDataInput = boundaryInput + ".data";
        //using (var sw = new StreamWriter(boundaryDataInput))
        //{
        //  var header = File.ReadAllLines(chroList[0].FileName).First();
        //  sw.WriteLine("ChroFile\t{0}", header);

        //  foreach (var chroFile in chroList)
        //  {
        //    foreach (var line in File.ReadAllLines(chroFile.FileName).Skip(1))
        //    {
        //      sw.WriteLine("{0}\t{1}", Path.GetFileNameWithoutExtension(chroFile.FileName), line);
        //    }
        //  }
        //}

        Progress.SetMessage("Finding boundaries ...");
        var boundaryOptions = new RTemplateProcessorOptions()
        {
          InputFile = boundaryInput,
          OutputFile = options.OutputFile,
          RTemplate = BoundaryR,
          RExecute = ExternalProgramConfig.GetExternalProgram("R"),
          CreateNoWindow = true
        };
        new RTemplateProcessor(boundaryOptions) { Progress = this.Progress }.Process();
      }

      if (options.DrawImage)
      {
        Progress.SetMessage("Drawing images ...");

        var imageOptions = new RTemplateProcessorOptions()
        {
          InputFile = options.OutputFile,
          OutputFile = Path.ChangeExtension(options.OutputFile, ".image"),
          RTemplate = ImageR,
          RExecute = SystemUtils.GetRExecuteLocation(),
          CreateNoWindow = true,
          NoResultFile = true
        };
        new RTemplateProcessor(imageOptions) { Progress = this.Progress }.Process();
      }

      return new string[] { options.OutputFile };
    }

    private IFileReader<List<IIdentifiedSpectrum>> GetPeptideReader()
    {
      IFileReader<List<IIdentifiedSpectrum>> result;
      using (var sr = new StreamReader(options.InputFile))
      {
        var header = sr.ReadLine();
        if (header.Contains("PredictionRetentionTime"))
        {
          result = new RetentionTimePredictionFormat();
        }
        else
        {
          result = new MascotPeptideTextFormat();
        }
      }
      return result;
    }

    public string GetTargetDirectory()
    {
      var result = Path.GetFullPath(Path.ChangeExtension(options.OutputFile, ".chros"));
      if (!Directory.Exists(result))
      {
        Directory.CreateDirectory(result);
      }
      return result;
    }

    private static PeakList<Peak> ReadFullMS(CacheRawFile rawReader, List<FullMS> fullMSList, int scanIndex)
    {
      if (fullMSList[scanIndex].Peaks == null)
      {
        fullMSList[scanIndex].Peaks = rawReader.GetPeakList(fullMSList[scanIndex].Scan);
      }

      return fullMSList[scanIndex].Peaks;
    }

    private bool AddEnvelope(ChromatographProfile chro, CacheRawFile rawReader, List<FullMS> fullMSList, int scanIndex)
    {
      PeakList<Peak> ms1 = ReadFullMS(rawReader, fullMSList, scanIndex);

      ChromatographProfileScan envelope = null;

      if (!profileFinder.Find(ms1, chro, options.MzTolerancePPM, options.ProfileLength, ref envelope))
      {
        return false;
      }

      if (envelope.CalculateProfileCorrelation(chro.IsotopicIntensities) < options.MinimumCorrelation)
      {
        return false;
      }

      chro.Profiles.Add(envelope);
      return true;
    }

    public string GetTargetFile(IIdentifiedSpectrum peptide)
    {
      var exp = peptide.Query.FileScan.Experimental.Replace(" ", "_");
      var targetdir = String.Format(@"{0}\{1}", GetTargetDirectory(), exp);
      if (!Directory.Exists(targetdir))
      {
        Directory.CreateDirectory(targetdir);
      }

      var result = String.Format(@"{0}\{1}_{2}_{3}_{4}.chro.tsv",
        targetdir,
        exp,
        peptide.Peptide.PureSequence,
        Math.Round(peptide.GetTheoreticalMz()),
        peptide.Query.FileScan.FirstScan);
      return new FileInfo(result).FullName;
    }

    private string GetTargetSubFile(IIdentifiedSpectrum peptide)
    {
      var targetFile = GetTargetFile(peptide);
      var targetDir = Path.GetDirectoryName(targetFile);
      var targetName = Path.GetFileName(targetFile);

      var targetSubDir = String.Format(@"{0}\sub", targetDir);
      if (!Directory.Exists(targetSubDir))
      {
        Directory.CreateDirectory(targetSubDir);
      }

      var targetSubName = Path.ChangeExtension(targetName, ".sub.tsv");

      var result = String.Format(@"{0}\{1}",
        targetSubDir,
        targetSubName);

      return new FileInfo(result).FullName;
    }


    public string GetTargetImageFile(IIdentifiedSpectrum peptide)
    {
      return GetTargetFile(peptide) + ".png";
    }
  }
}