using System;
using System.Linq;
using System.Collections.Generic;
using RCPA.Proteomics.Summary;
using RCPA.Utils;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using RCPA.R;

namespace RCPA.Proteomics.Quantification.O18
{
  public abstract class AbstractO18ProteinRatioRCalculator : IProteinRatioCalculator
  {
    class WaitingEntry
    {
      public IIdentifiedProteinGroup Group { get; set; }
      public string IntensityFile { get; set; }
      public double MaxReference { get; set; }
      public double MaxSample { get; set; }
    }

    protected IGetRatioIntensity intensityFunc;

    public IO18QuantificationOptions Option { get; set; }

    public AbstractO18ProteinRatioRCalculator(IGetRatioIntensity intensityFunc, IO18QuantificationOptions option)
    {
      this.intensityFunc = intensityFunc;
      this.Option = option;
    }

    public IGetRatioIntensity IntensityFunc
    {
      get { return this.intensityFunc; }
    }

    public void Calculate(IIdentifiedResult mr, Func<IIdentifiedSpectrum, bool> validFunc)
    {
      var proteinFiles = new List<WaitingEntry>();
      foreach (var mpg in mr)
      {
        var pf = DoCalculate(mpg, validFunc, false);
        if (pf != null)
        {
          proteinFiles.Add(pf);
        }
      }

      if (proteinFiles.Count > 0)
      {
        var listfile = (this.DetailDirectory + "/rlm_file.csv").Replace("\\", "/");
        using (var sw = new StreamWriter(listfile))
        {
          sw.WriteLine("Protein,IntensityFile,MaxReference,MaxSample");
          foreach (var we in proteinFiles)
          {
            sw.WriteLine("{0},{1},{2},{3}", we.Group[0].Name, we.IntensityFile, we.MaxReference, we.MaxSample);
          }
        }

        var linearfile = new FileInfo(this.DetailDirectory + "/rlm.linear").FullName.Replace("\\", "/");

        var roptions = new RTemplateProcessorOptions();

        roptions.InputFile = listfile;
        roptions.OutputFile = linearfile;
        roptions.RTemplate = FileUtils.GetTemplateDir() + "/MultipleO18Quantification.r";

        new RTemplateProcessor(roptions).Process();

        var results = (from line in File.ReadAllLines(linearfile).Skip(1)
                       let parts = line.Split(',')
                       select new
                       {
                         ProteinName = parts[0].StringAfter("\"").StringBefore("\""),
                         MaxReference = double.Parse(parts[2]),
                         MaxSample = double.Parse(parts[3]),
                         Ratio = double.Parse(parts[4]),
                         StdErr = double.Parse(parts[5]),
                         TValue = double.Parse(parts[6]),
                         PValue = double.Parse(parts[7]),
                         Count = int.Parse(parts[8])
                       }).ToDictionary(m => m.ProteinName);

        foreach (var pg in mr)
        {
          if (results.ContainsKey(pg[0].Name))
          {
            var res = results[pg[0].Name];
            var lrrr = new LinearRegressionRatioResult(res.Ratio, res.StdErr) { PointCount = res.Count, FCalculatedValue = res.TValue, FProbability = res.PValue };
            foreach (IIdentifiedProtein protein in pg)
            {
              protein.SetEnabled(true);
              protein.Annotations[O18QuantificationConstants.O18_RATIO] = lrrr;

              var sam = res.MaxSample;
              var refer = res.MaxReference;

              if (refer > sam)
              {
                sam = refer * res.Ratio;
              }
              else
              {
                refer = sam / res.Ratio;
              }
              protein.Annotations[this.intensityFunc.ReferenceKey] = refer;
              protein.Annotations[this.intensityFunc.SampleKey] = sam;
            }
          }
        }
      }
    }

    public void Calculate(IIdentifiedProteinGroup proteinGroup, Func<IIdentifiedSpectrum, bool> validFunc)
    {
      DoCalculate(proteinGroup, validFunc, true);
    }

    private WaitingEntry DoCalculate(IIdentifiedProteinGroup proteinGroup, Func<IIdentifiedSpectrum, bool> validFunc, bool runRImmediately)
    {
      List<IIdentifiedSpectrum> spectra = (from s in proteinGroup[0].GetSpectra()
                                           where validFunc(s) && s.IsEnabled(true) && HasPeptideRatio(s)
                                           select s).ToList();

      if (spectra.Count == 1)
      {
        var r = CalculatePeptideRatio(spectra[0]);
        foreach (var protein in proteinGroup)
        {
          protein.SetEnabled(true);

          protein.Annotations[O18QuantificationConstants.O18_RATIO] = new LinearRegressionRatioResult(r, 0.0) { PointCount = 1, FCalculatedValue = 0, FProbability = 1 };
          protein.Annotations[this.intensityFunc.ReferenceKey] = spectra[0].Annotations[this.intensityFunc.ReferenceKey];
          protein.Annotations[this.intensityFunc.SampleKey] = spectra[0].Annotations[this.intensityFunc.SampleKey];
        }
        return null;
      }
      else if (spectra.Count > 1)
      {
        var intensities = this.intensityFunc.ConvertToArray(spectra);

        double maxSam = intensities[0].Max();
        double maxRef = intensities[1].Max();

        LinearRegressionRatioResult ratioResult;

        if (maxSam == 0.0)
        {
          ratioResult = new LinearRegressionRatioResult(20, 0.0) { PointCount = intensities.Count(), FCalculatedValue = 0, FProbability = 0 };
          maxSam = maxRef / ratioResult.Ratio;
        }
        else
        {
          if (maxRef == 0.0)
          {
            ratioResult = new LinearRegressionRatioResult(0.05, 0.0) { PointCount = intensities.Count(), FCalculatedValue = 0, FProbability = 0 };
          }
          else
          {
            var filename = (this.DetailDirectory + "/" + proteinGroup[0].Name.Replace("|", "_") + ".csv").Replace("\\", "/");

            PrepareIntensityFile(spectra, filename);

            if (!runRImmediately)
            {
              return new WaitingEntry()
              {
                Group = proteinGroup,
                IntensityFile = filename,
                MaxReference = maxRef,
                MaxSample = maxSam
              };
            }

            var linearfile = filename + ".linear";

            var roptions = new RTemplateProcessorOptions();

            roptions.InputFile = filename;
            roptions.OutputFile = linearfile;
            roptions.RTemplate = FileUtils.GetTemplateDir() + "/O18Quantification.r";

            new RTemplateProcessor(roptions).Process();

            ratioResult = new LinearRegressionRatioResult();
            var parts = File.ReadAllLines(linearfile).Skip(1).First().Split(',');
            ratioResult.Ratio = double.Parse(parts[1]);
            ratioResult.RSquare = double.Parse(parts[2]);
            ratioResult.FCalculatedValue = double.Parse(parts[3]);
            ratioResult.FProbability = double.Parse(parts[4]);
          }
          maxRef = maxSam * ratioResult.Ratio;
        }

        foreach (IIdentifiedProtein protein in proteinGroup)
        {
          protein.SetEnabled(true);

          protein.Annotations[O18QuantificationConstants.O18_RATIO] = ratioResult;
          protein.Annotations[this.intensityFunc.ReferenceKey] = maxRef;
          protein.Annotations[this.intensityFunc.SampleKey] = maxSam;
        }
      }
      else
      {
        foreach (IIdentifiedProtein protein in proteinGroup)
        {
          protein.SetEnabled(false);
          protein.Annotations.Remove(O18QuantificationConstants.O18_RATIO);
          protein.Annotations.Remove(this.intensityFunc.ReferenceKey);
          protein.Annotations.Remove(this.intensityFunc.SampleKey);
        }
      }
      return null;
    }

    protected abstract void PrepareIntensityFile(List<IIdentifiedSpectrum> spectra, string filename);

    public double CalculatePeptideRatio(IIdentifiedSpectrum mph)
    {
      double o16 = this.intensityFunc.GetReferenceIntensity(mph);
      double o18 = this.intensityFunc.GetSampleIntensity(mph);

      double result = 0;
      if (o16 == 0)
      {
        result = 20;
      }
      else if (o18 == 0)
      {
        result = 0.05;
      }
      else
      {
        result = o18 / o16;
      }

      result = Math.Min(20, Math.Max(result, 0.05));

      return result;
    }

    public bool HasPeptideRatio(IIdentifiedSpectrum spectrum)
    {
      return intensityFunc.HasRatio(spectrum);
    }

    public bool HasProteinRatio(IIdentifiedProtein protein)
    {
      if (!protein.Annotations.ContainsKey(O18QuantificationConstants.O18_RATIO))
      {
        return false;
      }

      return protein.Annotations[O18QuantificationConstants.O18_RATIO] is LinearRegressionRatioResult;
    }

    public double GetProteinRatio(IIdentifiedProtein protein)
    {
      if (HasProteinRatio(protein))
      {
        return (protein.Annotations[O18QuantificationConstants.O18_RATIO] as LinearRegressionRatioResult).Ratio;
      }
      else
      {
        return double.NaN;
      }
    }

    public string SummaryFileDirectory { get; set; }

    public string DetailDirectory { get; set; }
  }
}