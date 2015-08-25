﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCPA.Proteomics.Snp
{
  public class SapPredictedValidationWriter : SapPredictedWriter
  {
    private Dictionary<string, SapValidationScan> expectScans;

    public SapPredictedValidationWriter(string matchedFile)
    {
      this.expectScans = (from line in File.ReadAllLines(matchedFile).Skip(1)
                          let parts = line.Split('\t')
                          where parts.Length > 7
                          let expects = parts[7]
                          where !string.IsNullOrEmpty(expects)
                          let mutation = GetMutation(parts[0], parts[5])
                          let scans = expects.Split('/')
                          from scan in scans
                          select new SapValidationScan() { FileScan = scan, Source = mutation.Item1, Target = mutation.Item2 }).ToDictionary(m => m.FileScan);
    }

    private Tuple<char, char> GetMutation(string p1, string p2)
    {
      for (int i = 0; i < p1.Length; i++)
      {
        if (p1[i] != p2[i])
        {
          return new Tuple<char, char>(p1[i], p2[i]);
        }
      }

      return null;
    }

    protected override string GetHeader()
    {
      return base.GetHeader() + "\tMutation\tPredictCorrect";
    }

    protected override string GetValue(SapPredicted predict)
    {
      var isExpect = expectScans.ContainsKey(predict.Ms2.FileScan.LongFileName);
      var expect = isExpect ? new TargetSAP() { Source = expectScans[predict.Ms2.FileScan.LongFileName].Source, Target = expectScans[predict.Ms2.FileScan.LongFileName].Target } : null;

      return base.GetValue(predict) + string.Format("\t{0}\t{1}",
        predict.Expect == null ? string.Empty : string.Format("{0} => {1}", predict.Expect.Source, predict.Expect.Target),
        predict.IsExpect);
    }
  }
}
