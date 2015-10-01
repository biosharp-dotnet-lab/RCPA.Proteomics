﻿using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Utils;
using RCPA.Seq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCPA.Proteomics.Snp
{
  public abstract class AbstractMS3LibraryPredictor : AbstractThreadProcessor
  {
    protected MS3LibraryPredictorOptions options;

    public AbstractMS3LibraryPredictor(MS3LibraryPredictorOptions options)
    {
      this.options = options;
    }

    public override IEnumerable<string> Process()
    {
      var expRawfileMap = options.RawFiles.ToDictionary(m => Path.GetFileNameWithoutExtension(m));

      Progress.SetMessage("Reading library file ...");
      var lib = new MS2ItemXmlFormat().ReadFromFile(options.LibraryFile).GroupBy(m => m.Charge).ToDictionary(m => m.Key, m => m.ToList());

      Progress.SetMessage("Building library sequence amino acid composition ...");
      lib.ForEach(m => m.Value.ForEach(l => l.AminoacidCompsition = (from a in l.Peptide
                                                                     where options.AllowedMassChangeMap.ContainsKey(a)
                                                                     select a).Distinct().OrderBy(k => k).ToArray()));

      List<IIdentifiedSpectrum> libPeptides = new List<IIdentifiedSpectrum>();
      if (File.Exists(options.LibraryPeptideFile))
      {
        Progress.SetMessage("Reading peptides file used for excluding scan ...");
        libPeptides = new MascotPeptideTextFormat().ReadFromFile(options.LibraryPeptideFile);
      }

      var expPeptidesMap = libPeptides.GroupBy(m => m.Query.FileScan.Experimental).ToDictionary(m => m.Key, m => m.ToDictionary(l => l.Query.FileScan.FirstScan));

      Progress.SetMessage("Reading MS2/MS3 data ...");
      var result = GetCandidateMs2ItemList(expRawfileMap, expPeptidesMap);

      //new MS2ItemXmlFormat().WriteToFile(options.OutputFile + ".xml", result);

      Progress.SetMessage("Finding SAP ...");
      List<SapPredicted> predicted = new List<SapPredicted>();

      var minDeltaMass = options.AllowedMassChangeMap.Values.Min(l => l.Min(k => k.DeltaMass));
      var maxDeltaMass = options.AllowedMassChangeMap.Values.Max(l => l.Max(k => k.DeltaMass));

      Progress.SetRange(0, result.Count);
      Progress.Begin();

      FindCandidates(lib, result, predicted, minDeltaMass, maxDeltaMass);

      var groups = predicted.ToGroupDictionary(m => m.Ms2.FileScan);
      predicted.Clear();
      foreach (var g in groups.Values)
      {
        var gg = g.ToGroupDictionary(m => m.LibMs2).Values.ToList();
        gg.Sort((m1, m2) =>
        {
          return CompareSapPrecitedList(m1, m2);
        });

        var expect = gg[0].FirstOrDefault(m => m.IsExpect);
        if (expect != null)
        {
          predicted.Add(expect);
        }
        else
        {
          predicted.AddRange(gg[0]);
          for (int i = 1; i < gg.Count; i++)
          {
            if (CompareSapPrecitedList(gg[0], gg[i]) == 0)
            {
              predicted.AddRange(gg[i]);
            }
            else
            {
              break;
            }
          }
        }
      }

      if (File.Exists(options.MatchedFile))
      {
        new SapPredictedValidationWriter(options.MatchedFile).WriteToFile(options.OutputFile, predicted);
      }
      else
      {
        new SapPredictedWriter().WriteToFile(options.OutputTableFile, predicted);

        Progress.SetMessage("Generating SAP sequence ...");
        List<Sequence> predictedSeq = new List<Sequence>();
        foreach (var predict in predicted)
        {
          var seq = PeptideUtils.GetPureSequence(predict.LibMs2.Peptide);
          if (!predict.Target.IsTerminalLoss)
          {
            for (int i = 0; i < seq.Length; i++)
            {
              if (seq[i] == predict.Target.Source[0])
              {
                string targetSeq;
                if (i == 0)
                {
                  targetSeq = predict.Target.Target + seq.Substring(1);
                }
                else
                {
                  targetSeq = seq.Substring(0, i) + predict.Target.Target + seq.Substring(i + 1);
                }

                var reference = string.Format("sp|SAP_{0}|{1}_{2}_{3}_{4}", targetSeq, seq, predict.Target.Source, i + 1, predict.Target.Target);
                predictedSeq.Add(new Sequence(reference, targetSeq));
              }
            }
          }
          else
          {
            string targetSeq = predict.Target.Target;

            var reference = string.Format("sp|SAP_{0}|{1}_loss_{2}term_{3}", targetSeq, seq, predict.Target.IsNterminalLoss ? "n" : "c", seq.Substring(0, seq.Length - targetSeq.Length));
            predictedSeq.Add(new Sequence(reference, targetSeq));
          }
        }

        predictedSeq = (from g in predictedSeq.GroupBy(m => m.SeqString)
                        select g.First()).ToList();

        var databases = SequenceUtils.Read(options.DatabaseFastaFile);
        for (int i = predictedSeq.Count - 1; i >= 0; i--)
        {
          foreach (var db in databases)
          {
            if (db.SeqString.Contains(predictedSeq[i].SeqString))
            {
              predictedSeq.RemoveAt(i);
              break;
            }
          }
        }
        databases.AddRange(predictedSeq);

        Progress.SetMessage("Writing SAP sequence and original database to {0} ...", options.OutputFile);

        SequenceUtils.Write(new FastaFormat(), options.OutputFile, databases);
      }

      Progress.End();

      return new string[] { options.OutputFile, options.OutputTableFile };
    }

    protected abstract void FindCandidates(Dictionary<int, List<MS2Item>> lib, List<MS2Item> result, List<SapPredicted> predicted, double minDeltaMass, double maxDeltaMass);

    protected void CheckSAP(List<SapPredicted> predicted, MS2Item ms2, double minMz, double maxMz, MS2Item libms2, SapMatchedCount ms3match)
    {
      foreach (var aa in libms2.AminoacidCompsition)
      {
        var lst = options.AllowedMassChangeMap[aa];
        foreach (var ts in lst)
        {
          var targetMz = libms2.Precursor + ts.DeltaMass / libms2.Charge;
          if (targetMz < minMz)
          {
            continue;
          }

          if (targetMz >= maxMz)
          {
            break;
          }

          var curp = new SapPredicted()
          {
            Ms2 = ms2,
            LibMs2 = libms2,
            Matched = ms3match,
            Target = new TargetSAP()
            {
              IsNterminalLoss = false,
              Source = ts.Source,
              Target = ts.Target,
              DeltaMass = ts.DeltaMass
            }
          };

          predicted.Add(curp);
        }
      }
    }

    protected static void OutputIntervalResult(StreamWriter sw, MS2Item ms2, MS2Item libms2, SapMatchedCount ms3match)
    {
      sw.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}",
        ms2.FileScan,
        ms2.Precursor,
        ms2.Charge,
        libms2.Precursor,
        ms3match.PrecursorMatched.ConvertAll(m => m.ToString()).Merge(";"),
        ms3match.MS3Matched.ConvertAll(m => m.ToString()).Merge(";"),
        (ms2.Precursor - libms2.Precursor) * ms2.Charge,
        libms2.FileScan,
        libms2.Peptide,
        libms2.Score,
        libms2.ExpectValue,
        libms2.Proteins);
    }

    protected static int CompareSapPrecitedList(List<SapPredicted> m1, List<SapPredicted> m2)
    {
      var p1 = m1.First();
      var p2 = m2.First();
      var res = p2.Matched.PrecursorMatched.Count.CompareTo(p1.Matched.PrecursorMatched.Count);
      if (res == 0)
      {
        res = p2.Matched.MS3Matched.Count(l => l > 0).CompareTo(p1.Matched.MS3Matched.Count(l => l > 0));
        if (res == 0)
        {
          res = p2.Matched.MS3Matched.Sum().CompareTo(p1.Matched.MS3Matched.Sum());
        }
      }

      return res;
    }

    protected SapMatchedCount GetMS3MatchedCount(MS2Item libms2, MS2Item ms2, double minPrecursorMz)
    {
      var precursorMatched = new List<double>();
      var ms3Matched = new List<int>();
      foreach (var p1 in libms2.MS3Spectra)
      {
        if (p1.PrecursorMZ < minPrecursorMz)
        {
          continue;
        }

        int minCount = (int)Math.Round(p1.Count * 0.1);

        foreach (var p2 in ms2.MS3Spectra)
        {
          if (p2.PrecursorMZ < minPrecursorMz)
          {
            continue;
          }

          if (PrecursorUtils.mz2ppm(p1.PrecursorMZ, Math.Abs(p1.PrecursorMZ - p2.PrecursorMZ)) < options.PrecursorPPMTolerance)
          {
            precursorMatched.Add(p1.PrecursorMZ);

            int ionMatched = 0;
            Peak[] pp2;
            if (p2.Count <= options.MaxFragmentPeakCount)
            {
              pp2 = p2.ToArray();
            }
            else
            {
              pp2 = p2.OrderByDescending(m => m.Intensity).Take(options.MaxFragmentPeakCount).OrderBy(m => m.Mz).ToArray();
            }

            foreach (var ion1 in p1)
            {
              var mzTol = PrecursorUtils.ppm2mz(ion1.Mz, options.FragmentPPMTolerance);
              var maxMz = ion1.Mz + mzTol;
              var minMz = ion1.Mz - mzTol;
              foreach (var ion2 in pp2)
              {
                if (ion2.Mz < minMz)
                {
                  continue;
                }

                if (ion2.Mz > maxMz)
                {
                  break;
                }

                ionMatched++;
                break;
              }
            }

            ms3Matched.Add(ionMatched);
            break;
          }
        }
      }

      return new SapMatchedCount()
      {
        Item1 = libms2,
        Item2 = ms2,
        PrecursorMatched = precursorMatched,
        MS3Matched = ms3Matched
      };
    }

    private List<MS2Item> GetCandidateMs2ItemList(Dictionary<string, string> expRawfileMap, Dictionary<string, Dictionary<int, IIdentifiedSpectrum>> expPeptidesMap)
    {
      var result = new List<MS2Item>();
      foreach (var exp in expRawfileMap.Keys)
      {
        var rawfile = expRawfileMap[exp];
        var peptides = expPeptidesMap[exp];
        var experimental = Path.GetFileNameWithoutExtension(rawfile);

        using (var reader = RawFileFactory.GetRawFileReader(rawfile, false))
        {
          var firstScan = reader.GetFirstSpectrumNumber();
          var lastScan = reader.GetLastSpectrumNumber();

          for (int scan = firstScan; scan < lastScan; scan++)
          {
            var msLevel = reader.GetMsLevel(scan);
            if (msLevel != 2)
            {
              continue;
            }

            if (peptides.ContainsKey(scan))
            {
              continue;
            }

            var ms2precursor = reader.GetPrecursorPeak(scan);
            var ms2 = new MS2Item()
            {
              Precursor = ms2precursor.Mz,
              Charge = ms2precursor.Charge,
              FileScan = new SequestFilename()
              {
                Experimental = experimental,
                FirstScan = scan,
                LastScan = scan,
                Charge = ms2precursor.Charge
              }.LongFileName
            };

            for (int ms3scan = scan + 1; ms3scan < lastScan; ms3scan++)
            {
              var mslevel = reader.GetMsLevel(ms3scan);
              if (mslevel != 3)
              {
                scan = ms3scan - 1;
                break;
              }
              var pkl = reader.GetPeakList(ms3scan);
              if (pkl.Count == 0)
              {
                continue;
              }

              var ms3precursor = reader.GetPrecursorPeak(ms3scan);
              pkl.PrecursorMZ = ms3precursor.Mz;
              ms2.MS3Spectra.Add(pkl);
            }

            if (ms2.MS3Spectra.Count > 0)
            {
              result.Add(ms2);
            }
          }
        }
      }

      return result;
    }
  }
}