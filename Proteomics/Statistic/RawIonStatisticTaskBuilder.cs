﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Spectrum;
using System.IO;
using System.Threading.Tasks;
using MathNet.Numerics.Statistics;
using RCPA.Utils;

namespace RCPA.Proteomics.Statistic
{
  public class RawIonStatisticTaskBuilder : AbstractParallelTaskProcessor
  {
    private const int FULLMS_CHARGE = -1;

    private RawIonStatisticTaskBuilderOptions options;

    public RawIonStatisticTaskBuilder(RawIonStatisticTaskBuilderOptions options)
    {
      this.options = options;
    }

    private class SourcePeak
    {
      public SourcePeak(int scan, double mz, double intensity)
      {
        this.Scan = scan;
        this.Mz = mz;
        this.Intensity = intensity;
      }

      public int Scan { get; set; }
      public double Mz { get; set; }
      public double Intensity { get; set; }
    }

    private class PeakEntry
    {
      public PeakEntry()
      {
        this.Intensities = new List<SourcePeak>();
      }

      public Peak Ion { get; set; }
      public double FromMz { get; set; }
      public double ToMz { get; set; }
      public List<SourcePeak> Intensities { get; set; }
      public override string ToString()
      {
        return string.Format("{0:0.0000}", Ion.Mz);
      }
    }

    public override IEnumerable<string> Process(string fileName)
    {
      Dictionary<int, Dictionary<int, List<PeakEntry>>> maps = new Dictionary<int, Dictionary<int, List<PeakEntry>>>();
      Dictionary<int, Dictionary<int, List<PeakEntry>>> compmaps = new Dictionary<int, Dictionary<int, List<PeakEntry>>>();
      Dictionary<int, int> scanCounts = new Dictionary<int, int>();
      using (var reader = RawFileFactory.GetRawFileReader(fileName))
      {
        var firstScan = reader.GetFirstSpectrumNumber();
        var lastScan = reader.GetLastSpectrumNumber();
        //var firstScan = 17047;
        //var lastScan = 17047;

        for (int i = firstScan; i <= lastScan; i++)
        {
          Peak precursor;
          if (reader.GetMsLevel(i) == 1)
          {
            precursor = new Peak(0.0, 0.0, FULLMS_CHARGE);
          }
          else
          {
            precursor = reader.GetPrecursorPeak(i);
          }

          if (!scanCounts.ContainsKey(precursor.Charge))
          {
            scanCounts[precursor.Charge] = 1;
          }
          else
          {
            scanCounts[precursor.Charge] = scanCounts[precursor.Charge] + 1;
          }

          var pkl = reader.GetPeakList(i);
          if (pkl.Count == 0)
          {
            continue;
          }

          //if (i == 17047)
          //{
          //  pkl.ForEach(m => Console.WriteLine("{0}\t{1}", m.Mz, m.Intensity));
          //  return null;
          //}

          if (Progress.IsCancellationPending() || IsLoopStopped)
          {
            return null;
          }

          if (!maps.ContainsKey(precursor.Charge))
          {
            maps[precursor.Charge] = new Dictionary<int, List<PeakEntry>>();
            compmaps[precursor.Charge] = new Dictionary<int, List<PeakEntry>>();
          }

          var map = maps[precursor.Charge];
          var compmap = compmaps[precursor.Charge];

          var maxPeak = pkl.FindMaxIntensityPeak();
          var minIntensity = maxPeak.Intensity * options.MinRelativeIntensity;

          double precursorMass = precursor.Charge > 0 ? PrecursorUtils.MzToMass(precursor.Mz, precursor.Charge, true) : 0.0;
          foreach (var peak in pkl)
          {
            if (peak.Intensity > minIntensity)
            {
              AddPeak(map, maxPeak.Intensity, i, peak);

              if (precursor.Charge > 0)
              {
                var peakMass = peak.Charge == 0 ? peak.Mz : PrecursorUtils.MzToMass(peak.Mz, peak.Charge, true);
                peakMass = precursorMass - peakMass;
                AddPeak(compmap, maxPeak.Intensity, i, new Peak(peakMass, peak.Intensity, peak.Charge));
              }
            }
          }
        }
      }

      var keys = (from charge in maps.Keys
                  orderby charge
                  select charge).ToList();

      var resultFile1 = new FileInfo(options.TargetDirectory + "//" + new FileInfo(fileName).Name + ".forward.ionfrequency").FullName;
      WriteMap(scanCounts, keys, resultFile1, maps, true);

      var resultFile2 = new FileInfo(options.TargetDirectory + "//" + new FileInfo(fileName).Name + ".backward.ionfrequency").FullName;
      WriteMap(scanCounts, keys, resultFile2, compmaps, false);

      return new string[] { resultFile1, resultFile2 };
    }

    private void WriteMap(Dictionary<int, int> scanCounts, List<int> keys, string filename, Dictionary<int, Dictionary<int, List<PeakEntry>>> curMaps, bool exportIndividualIon)
    {
      using (var sw = new StreamWriter(filename))
      {
        foreach (var key in keys)
        {
          var totalCount = scanCounts[key];
          string subfile = string.Empty;

          //if (key != 2)
          //{
          //  continue;
          //}

          if (key == FULLMS_CHARGE)
          {
            sw.WriteLine("FullMS,ScanCount={1}", key, totalCount);
            subfile = filename + ".fullms";
          }
          else if (key == 0)
          {
            sw.WriteLine("Charge=UNKNOWN,ScanCount={1}", key, totalCount);
            subfile = filename + ".unknown";
          }
          else
          {
            sw.WriteLine("Charge={0},ScanCount={1}", key, totalCount);
            subfile = filename + ".ms2charge" + key.ToString();
          }
          var map = curMaps[key];

          sw.WriteLine("Ion\tCount\tFrequency\tMeanIntensity\tSD\tMedianIntensity");
          foreach (var e in map.Values)
          {
            MergeIons(e);
          }

          var ens = (from e in map.Values from en in e select en).ToList();
          //var ens = (from e in map.Values from en in e orderby en.Ion.Mz select en).ToList();
          //ens.ForEach(m => Console.WriteLine("{0}\t{1}", m.Ion.Mz, m.Intensities.Count));
          MergeIons(ens);

          ////remove the ions with less observations
          //var totalscan = (from en in ens
          //                 from intensity in en.Intensities
          //                 select intensity.Scan).Distinct().Count();
          //var minFrequencyCount = Math.Floor(totalscan * options.MinFrequency);
          //ens.RemoveAll(en => en.Intensities.Count < minFrequencyCount);

          //remove the duplication
          foreach (var ee in ens)
          {
            ee.Intensities = (from intt in ee.Intensities.GroupBy(m => m.Scan)
                              select (from inttt in intt
                                      orderby inttt.Intensity descending
                                      select inttt).First()).ToList();
          }

          //var ensgroup = ens.GroupBy(m => Math.Round(m.Ion.Mz)).OrderBy(m => m.Key).ToList();

          //ens = (from g in ensgroup
          //       select (from e in g
          //               orderby e.Intensities.Count descending
          //               select e).First()).ToList();

          if (exportIndividualIon)
          {
            using (var sw2 = new StreamWriter(subfile))
            {
              ens.Sort((m1, m2) => m1.Ion.Mz.CompareTo(m2.Ion.Mz));

              sw2.WriteLine("mz\tscan\tintensity");
              foreach (var ion in ens)
              {
                var grp = ion.Intensities.GroupBy(m => m.Scan).OrderBy(m => m.Key).ToList();
                foreach (var value in grp)
                {
                  if (value.Count() > 1)
                  {
                    Console.WriteLine("Multiple entry : {0}, {1}", ion.Ion.Mz, value.Key);
                  }

                  sw2.WriteLine("{0:0.00000}\t{1}\t{2:0.000}", ion.Ion.Mz, value.Key, value.First().Intensity);
                }
              }
            }

            //string outputfile;
            //var rfile = options.PrepareRFile(subfile, out outputfile);

            //new RProcessor(options.GetRCommand(), rfile, outputfile).Process();
          }

          var totalentries = (from en in ens
                              orderby en.Intensities.Count descending
                              select en).ToList();

          totalentries.ForEach(m =>
          {
            var ints = (from i in m.Intensities select i.Intensity).ToArray();
            var mean = Statistics.Mean(ints);
            var sd = Statistics.StandardDeviation(ints);
            var median = Statistics.Median(ints);

            sw.WriteLine("{0:0.0000}\t{1}\t{2:0.0000}\t{3:0.000}\t{4:0.000}\t{5:0.000}", m.Ion.Mz, m.Intensities.Count, m.Intensities.Count * 1.0 / totalCount, mean, sd, median);
          });
          sw.WriteLine();
        }
      }
    }

    private void MergeIons(List<PeakEntry> e)
    {
      e.Sort((m1, m2) => m2.Intensities.Count.CompareTo(m1.Intensities.Count));
      for (int i = e.Count - 1; i >= 0; i--)
      {
        for (int j = 0; j < i; j++)
        {
          if (e[i].Ion.Mz >= e[j].FromMz && e[i].Ion.Mz <= e[j].ToMz)
          {
            var allIntensity = e[i].Ion.Intensity + e[j].Ion.Intensity;

            e[j].Ion.Mz = (e[i].Ion.Mz * e[i].Ion.Intensity + e[j].Ion.Mz * e[j].Ion.Intensity) / allIntensity;
            e[j].Ion.Intensity = allIntensity;
            e[j].Intensities.AddRange(e[i].Intensities);

            var gap = PrecursorUtils.ppm2mz(e[j].Ion.Mz, options.ProductIonPPM);
            e[j].FromMz = e[j].Ion.Mz - gap;
            e[j].ToMz = e[j].Ion.Mz + gap;

            e.RemoveAt(i);

            break;
          }
        }
      }
    }

    private void AddPeak(Dictionary<int, List<PeakEntry>> map, double maxPeakIntensity, int scan, Peak peak)
    {
      var mz = (int)(Math.Round(peak.Mz));

      List<PeakEntry> entries;
      if (!map.TryGetValue(mz, out entries))
      {
        entries = new List<PeakEntry>();
        map[mz] = entries;
      }

      var relativeIntensity = peak.Intensity / maxPeakIntensity;

      var gap = PrecursorUtils.ppm2mz(peak.Mz, options.ProductIonPPM);
      PeakEntry entry = new PeakEntry()
      {
        Ion = new Peak(peak.Mz, peak.Intensity),
        FromMz = peak.Mz - gap,
        ToMz = peak.Mz + gap
      };
      entry.Intensities.Add(new SourcePeak(scan, peak.Mz, relativeIntensity));
      entries.Add(entry);
    }
  }
}
