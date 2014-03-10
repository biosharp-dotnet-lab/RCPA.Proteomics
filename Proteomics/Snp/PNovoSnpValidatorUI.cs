using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA;
using RCPA.Gui.FileArgument;
using RCPA.Gui.Command;
using System.IO;
using RCPA.Proteomics.Quantification;
using RCPA.Proteomics.Summary;
using RCPA.Seq;

namespace RCPA.Proteomics.Snp
{
  /// <summary>
  /// ��ȡfasta�ļ���һϵ�е�pNovo��������ݸ�������Сscore����ɸѡ����ȡ��ȫø�⡢û��missλ�㡢
  /// �����ݿ��е�λ��ͻ����ĶΣ���ԭ�������ݿ⹹����һ���µ����ݿ⣬�Ա�������ݿ�������֤��
  /// </summary>
  public partial class PNovoSnpValidatorUI : AbstractFileProcessorUI
  {
    private static readonly string title = "pNovo SAP Validator";
    private static readonly string version = "2.1.5";

    private RcpaDoubleField minScore;
    private RcpaComboBox<ITitleParser> titleParsers;
    private RcpaComboBox<IAccessNumberParser> acParsers;
    private RcpaComboBox<string> proteases;
    private RcpaIntegerField threadCount;

    public PNovoSnpValidatorUI()
    {
      InitializeComponent();

      base.SetDirectoryArgument("TargetDir", "Target");

      this.Text = Constants.GetSQHTitle(title, version);

      pNovoFiles.FileArgument = new OpenFileArgument("pNovo Result", "txt");
      AddComponent(this.pNovoFiles);

      this.minScore = new RcpaDoubleField(txtMinScore, "MinScore", "Minmum score", 0.65, true);
      AddComponent(this.minScore);

      this.threadCount = new RcpaIntegerField(txtThreadCount, "ThreadCount", "Thread count", Environment.ProcessorCount - 1, true);
      AddComponent(this.threadCount);

      toolTip1.SetToolTip(txtThreadCount, MyConvert.Format("Suggest max value = {0}", Environment.ProcessorCount + 1));

      this.titleParsers = new RcpaComboBox<ITitleParser>(cbTitleFormat, "TitleFormat", TitleParserUtils.GetTitleParsers().ToArray(), -1);
      AddComponent(this.titleParsers);

      fastaFile.FileArgument = new OpenFileArgument("Fasta To Find Mutation", "fasta");

      databaseFile.FileArgument= new OpenFileArgument("Fasta To Merge Mutated Peptide", "fasta");

      this.acParsers = new RcpaComboBox<IAccessNumberParser>(cbAccessNumberPattern, "AccessNumberParser", AccessNumberParserFactory.GetParsers().ToArray(), -1);
      AddComponent(this.acParsers);

      this.proteases = new RcpaComboBox<string>(cbProtease, "Protease", ProteaseManager.GetNames().ToArray(), -1);
      AddComponent(this.proteases);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      string dbFile;
      if (fastaFile.Exists)
      {
        dbFile = fastaFile.FullName;
      }
      else
      {
        dbFile = databaseFile.FullName;
      }
      return new PNovoSnpValidator(pNovoFiles.SelectedFileNames, databaseFile.FullName, dbFile, titleParsers.SelectedItem, acParsers.SelectedItem, ProteaseManager.GetProteaseByName(proteases.SelectedItem), minScore.Value, threadCount.Value)
      {
        IgnoreNtermMutation = ignoreNTerm.Checked,
        IgnoreDeamidatedMutation = ignoreDeamidatedMutation.Checked,
        IgnoreMultipleNucleotideMutation = ignoreMultipleNucleotideMutation.Checked
      };
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Misc;
      }

      public string GetCaption()
      {
        return title;
      }

      public string GetVersion()
      {
        return version;
      }

      public void Run()
      {
        new PNovoSnpValidatorUI().MyShow();
      }

      #endregion
    }
  }
}

