namespace RCPA.Proteomics.Format
{
  partial class MultipleRaw2MgfProcessor3UI
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultipleRaw2MgfProcessor3UI));
      this.rawFiles = new RCPA.Gui.MultipleFileField();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.cbByMsLevel = new RCPA.Gui.RcpaCheckField();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.label1 = new System.Windows.Forms.Label();
      this.cbDeconvolution = new RCPA.Gui.RcpaCheckField();
      this.txtDeisotopic = new System.Windows.Forms.TextBox();
      this.cbDeisotopic = new RCPA.Gui.RcpaCheckField();
      this.txtTopX = new System.Windows.Forms.TextBox();
      this.cbKeepTopX = new RCPA.Gui.RcpaCheckField();
      this.cbByMode = new RCPA.Gui.RcpaCheckField();
      this.cbParallel = new RCPA.Gui.RcpaCheckField();
      this.cbDefaultCharge = new System.Windows.Forms.ComboBox();
      this.label10 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.txtMinIonIntensity = new System.Windows.Forms.TextBox();
      this.label9 = new System.Windows.Forms.Label();
      this.cbTitleFormat = new System.Windows.Forms.ComboBox();
      this.label5 = new System.Windows.Forms.Label();
      this.txtMWRangeTo = new System.Windows.Forms.TextBox();
      this.txtMinIonCount = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.txtMinIonIntensityThreshold = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.txtMWRangeFrom = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.cbProteases = new System.Windows.Forms.ComboBox();
      this.cbRemoveIsobaricIonsInHighRange = new RCPA.Gui.RcpaCheckField();
      this.cbRemoveIsobaricIons = new RCPA.Gui.RcpaCheckField();
      this.cbRemoveMassRange = new RCPA.Gui.RcpaCheckField();
      this.cbRemoveIsobaricIonsInLowRange = new RCPA.Gui.RcpaCheckField();
      this.txtIsobaricIons = new System.Windows.Forms.TextBox();
      this.cbxIsobaricTypes = new System.Windows.Forms.ComboBox();
      this.cbRemoveSpecialIons = new RCPA.Gui.RcpaCheckField();
      this.txtRemoveIonWindow = new System.Windows.Forms.TextBox();
      this.txtSpecialIons = new System.Windows.Forms.TextBox();
      this.tabPage3 = new System.Windows.Forms.TabPage();
      this.panel1 = new System.Windows.Forms.Panel();
      this.panel3 = new System.Windows.Forms.Panel();
      this.label11 = new System.Windows.Forms.Label();
      this.txtRetentionTimeWindow = new System.Windows.Forms.TextBox();
      this.txtMaxShiftPPM = new System.Windows.Forms.TextBox();
      this.label7 = new System.Windows.Forms.Label();
      this.siliconePolymers = new RCPA.Proteomics.SiliconePolymerIonField();
      this.rbMassCalibrationByFixed = new RCPA.Gui.RcpaRadioField();
      this.rbMassCalibrationByFile = new RCPA.Gui.RcpaRadioField();
      this.rbMassCalibrationAutomatically = new RCPA.Gui.RcpaRadioField();
      this.txtShiftFile = new System.Windows.Forms.TextBox();
      this.btnShiftFile = new System.Windows.Forms.Button();
      this.txtPreductIonShift = new System.Windows.Forms.TextBox();
      this.txtPredursorShift = new System.Windows.Forms.TextBox();
      this.panel2 = new System.Windows.Forms.Panel();
      this.rbMassCalibration = new RCPA.Gui.RcpaRadioField();
      this.rbMassCalibrationNone = new RCPA.Gui.RcpaRadioField();
      this.cbCalibrateProductIon = new RCPA.Gui.RcpaCheckField();
      this.cbCalibratePrecursor = new RCPA.Gui.RcpaCheckField();
      this.tabPage4 = new System.Windows.Forms.TabPage();
      this.cbRemoveIonsLargerThanPrecursor = new RCPA.Gui.RcpaCheckField();
      this.txtNeutralLossComposition = new System.Windows.Forms.TextBox();
      this.cbRemovePrecursor = new RCPA.Gui.RcpaCheckField();
      this.pnlFile.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.tabPage3.SuspendLayout();
      this.panel1.SuspendLayout();
      this.panel3.SuspendLayout();
      this.panel2.SuspendLayout();
      this.tabPage4.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
      this.pnlFile.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pnlFile.Location = new System.Drawing.Point(0, 339);
      this.pnlFile.Size = new System.Drawing.Size(1083, 24);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Size = new System.Drawing.Size(837, 20);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 731);
      this.lblProgress.Size = new System.Drawing.Size(1083, 23);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 754);
      this.progressBar.Size = new System.Drawing.Size(1083, 23);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(589, 8);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(504, 8);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(419, 8);
      // 
      // rawFiles
      // 
      this.rawFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.rawFiles.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rawFiles.FileArgument = null;
      this.rawFiles.FileDescription = "Raw/mzData/mzXml files";
      this.rawFiles.FileNames = new string[0];
      this.rawFiles.Key = "File";
      this.rawFiles.Location = new System.Drawing.Point(0, 0);
      this.rawFiles.Name = "rawFiles";
      this.rawFiles.SelectedIndex = -1;
      this.rawFiles.SelectedItem = null;
      this.rawFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.rawFiles.Size = new System.Drawing.Size(1083, 339);
      this.rawFiles.TabIndex = 49;
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.tabControl1.Location = new System.Drawing.Point(0, 363);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(1083, 368);
      this.tabControl1.TabIndex = 50;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.cbByMsLevel);
      this.tabPage1.Controls.Add(this.groupBox1);
      this.tabPage1.Controls.Add(this.txtTopX);
      this.tabPage1.Controls.Add(this.cbKeepTopX);
      this.tabPage1.Controls.Add(this.cbByMode);
      this.tabPage1.Controls.Add(this.cbParallel);
      this.tabPage1.Controls.Add(this.cbDefaultCharge);
      this.tabPage1.Controls.Add(this.label10);
      this.tabPage1.Controls.Add(this.label8);
      this.tabPage1.Controls.Add(this.txtMinIonIntensity);
      this.tabPage1.Controls.Add(this.label9);
      this.tabPage1.Controls.Add(this.cbTitleFormat);
      this.tabPage1.Controls.Add(this.label5);
      this.tabPage1.Controls.Add(this.txtMWRangeTo);
      this.tabPage1.Controls.Add(this.txtMinIonCount);
      this.tabPage1.Controls.Add(this.label4);
      this.tabPage1.Controls.Add(this.txtMinIonIntensityThreshold);
      this.tabPage1.Controls.Add(this.label3);
      this.tabPage1.Controls.Add(this.txtMWRangeFrom);
      this.tabPage1.Controls.Add(this.label2);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(1075, 342);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "General option";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // cbByMsLevel
      // 
      this.cbByMsLevel.Checked = true;
      this.cbByMsLevel.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbByMsLevel.Key = "GroupByMsLevel";
      this.cbByMsLevel.Location = new System.Drawing.Point(257, 265);
      this.cbByMsLevel.Name = "cbByMsLevel";
      this.cbByMsLevel.PreCondition = null;
      this.cbByMsLevel.Size = new System.Drawing.Size(142, 21);
      this.cbByMsLevel.TabIndex = 74;
      this.cbByMsLevel.Text = "Group by ms level";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Controls.Add(this.cbDeconvolution);
      this.groupBox1.Controls.Add(this.txtDeisotopic);
      this.groupBox1.Controls.Add(this.cbDeisotopic);
      this.groupBox1.Location = new System.Drawing.Point(422, 47);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(254, 132);
      this.groupBox1.TabIndex = 73;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "High resolution MS/MS";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(9, 29);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(137, 13);
      this.label1.TabIndex = 85;
      this.label1.Text = "Product ion tolerance (ppm)";
      // 
      // cbDeconvolution
      // 
      this.cbDeconvolution.Checked = true;
      this.cbDeconvolution.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbDeconvolution.Key = "Deconvolution";
      this.cbDeconvolution.Location = new System.Drawing.Point(11, 86);
      this.cbDeconvolution.Name = "cbDeconvolution";
      this.cbDeconvolution.PreCondition = null;
      this.cbDeconvolution.Size = new System.Drawing.Size(223, 21);
      this.cbDeconvolution.TabIndex = 84;
      this.cbDeconvolution.Text = "Deconvolution to charge one";
      // 
      // txtDeisotopic
      // 
      this.txtDeisotopic.Location = new System.Drawing.Point(193, 26);
      this.txtDeisotopic.Name = "txtDeisotopic";
      this.txtDeisotopic.Size = new System.Drawing.Size(51, 20);
      this.txtDeisotopic.TabIndex = 83;
      // 
      // cbDeisotopic
      // 
      this.cbDeisotopic.Checked = true;
      this.cbDeisotopic.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbDeisotopic.Key = "Deisotopic";
      this.cbDeisotopic.Location = new System.Drawing.Point(11, 57);
      this.cbDeisotopic.Name = "cbDeisotopic";
      this.cbDeisotopic.PreCondition = null;
      this.cbDeisotopic.Size = new System.Drawing.Size(172, 21);
      this.cbDeisotopic.TabIndex = 82;
      this.cbDeisotopic.Text = "Deisotopic";
      // 
      // txtTopX
      // 
      this.txtTopX.Location = new System.Drawing.Point(461, 184);
      this.txtTopX.Name = "txtTopX";
      this.txtTopX.Size = new System.Drawing.Size(32, 20);
      this.txtTopX.TabIndex = 72;
      this.txtTopX.Text = "10";
      // 
      // cbKeepTopX
      // 
      this.cbKeepTopX.Checked = true;
      this.cbKeepTopX.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbKeepTopX.Key = "KeepTopX";
      this.cbKeepTopX.Location = new System.Drawing.Point(257, 185);
      this.cbKeepTopX.Name = "cbKeepTopX";
      this.cbKeepTopX.PreCondition = null;
      this.cbKeepTopX.Size = new System.Drawing.Size(218, 21);
      this.cbKeepTopX.TabIndex = 71;
      this.cbKeepTopX.Text = "Keep top X peaks in 100 Dalton";
      // 
      // cbByMode
      // 
      this.cbByMode.Checked = true;
      this.cbByMode.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbByMode.Key = "GroupByScanMode";
      this.cbByMode.Location = new System.Drawing.Point(257, 238);
      this.cbByMode.Name = "cbByMode";
      this.cbByMode.PreCondition = null;
      this.cbByMode.Size = new System.Drawing.Size(142, 21);
      this.cbByMode.TabIndex = 66;
      this.cbByMode.Text = "Group by scan mode";
      // 
      // cbParallel
      // 
      this.cbParallel.Checked = true;
      this.cbParallel.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbParallel.Key = "ParellelMode";
      this.cbParallel.Location = new System.Drawing.Point(257, 212);
      this.cbParallel.Name = "cbParallel";
      this.cbParallel.PreCondition = null;
      this.cbParallel.Size = new System.Drawing.Size(116, 21);
      this.cbParallel.TabIndex = 65;
      this.cbParallel.Text = "Parallel mode";
      // 
      // cbDefaultCharge
      // 
      this.cbDefaultCharge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbDefaultCharge.FormattingEnabled = true;
      this.cbDefaultCharge.Location = new System.Drawing.Point(257, 157);
      this.cbDefaultCharge.Name = "cbDefaultCharge";
      this.cbDefaultCharge.Size = new System.Drawing.Size(132, 21);
      this.cbDefaultCharge.TabIndex = 63;
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(153, 161);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(77, 13);
      this.label10.TabIndex = 62;
      this.label10.Text = "Default charge";
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(135, 21);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(83, 13);
      this.label8.TabIndex = 59;
      this.label8.Text = "Scan title format";
      // 
      // txtMinIonIntensity
      // 
      this.txtMinIonIntensity.Location = new System.Drawing.Point(257, 73);
      this.txtMinIonIntensity.Name = "txtMinIonIntensity";
      this.txtMinIonIntensity.Size = new System.Drawing.Size(132, 20);
      this.txtMinIonIntensity.TabIndex = 61;
      this.txtMinIonIntensity.Text = "1.0";
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(111, 77);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(106, 13);
      this.label9.TabIndex = 60;
      this.label9.Text = "Minimum ion intensity";
      // 
      // cbTitleFormat
      // 
      this.cbTitleFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbTitleFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbTitleFormat.FormattingEnabled = true;
      this.cbTitleFormat.Location = new System.Drawing.Point(257, 16);
      this.cbTitleFormat.Name = "cbTitleFormat";
      this.cbTitleFormat.Size = new System.Drawing.Size(812, 21);
      this.cbTitleFormat.TabIndex = 58;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(306, 49);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(13, 13);
      this.label5.TabIndex = 57;
      this.label5.Text = "--";
      // 
      // txtMWRangeTo
      // 
      this.txtMWRangeTo.Location = new System.Drawing.Point(329, 44);
      this.txtMWRangeTo.Name = "txtMWRangeTo";
      this.txtMWRangeTo.Size = new System.Drawing.Size(60, 20);
      this.txtMWRangeTo.TabIndex = 56;
      this.txtMWRangeTo.Text = "3500.00";
      // 
      // txtMinIonCount
      // 
      this.txtMinIonCount.Location = new System.Drawing.Point(257, 101);
      this.txtMinIonCount.Name = "txtMinIonCount";
      this.txtMinIonCount.Size = new System.Drawing.Size(132, 20);
      this.txtMinIonCount.TabIndex = 55;
      this.txtMinIonCount.Text = "5";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(135, 105);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(95, 13);
      this.label4.TabIndex = 54;
      this.label4.Text = "Minimum ion count";
      // 
      // txtMinIonIntensityThreshold
      // 
      this.txtMinIonIntensityThreshold.Location = new System.Drawing.Point(257, 129);
      this.txtMinIonIntensityThreshold.Name = "txtMinIonIntensityThreshold";
      this.txtMinIonIntensityThreshold.Size = new System.Drawing.Size(132, 20);
      this.txtMinIonIntensityThreshold.TabIndex = 53;
      this.txtMinIonIntensityThreshold.Text = "100";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(9, 133);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(175, 13);
      this.label3.TabIndex = 52;
      this.label3.Text = "Absolute total ion intensity threshold";
      // 
      // txtMWRangeFrom
      // 
      this.txtMWRangeFrom.Location = new System.Drawing.Point(257, 44);
      this.txtMWRangeFrom.Name = "txtMWRangeFrom";
      this.txtMWRangeFrom.Size = new System.Drawing.Size(43, 20);
      this.txtMWRangeFrom.TabIndex = 51;
      this.txtMWRangeFrom.Text = "600.00";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(75, 49);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(143, 13);
      this.label2.TabIndex = 50;
      this.label2.Text = "Precursor mass weight range";
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.cbProteases);
      this.tabPage2.Controls.Add(this.cbRemoveIsobaricIonsInHighRange);
      this.tabPage2.Controls.Add(this.cbRemoveIsobaricIonsInLowRange);
      this.tabPage2.Controls.Add(this.txtIsobaricIons);
      this.tabPage2.Controls.Add(this.cbxIsobaricTypes);
      this.tabPage2.Controls.Add(this.cbRemoveMassRange);
      this.tabPage2.Controls.Add(this.cbRemoveIsobaricIons);
      this.tabPage2.Controls.Add(this.cbRemoveSpecialIons);
      this.tabPage2.Controls.Add(this.txtRemoveIonWindow);
      this.tabPage2.Controls.Add(this.txtSpecialIons);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(1075, 342);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Isobaric Labeling";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // cbProteases
      // 
      this.cbProteases.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbProteases.FormattingEnabled = true;
      this.cbProteases.Location = new System.Drawing.Point(238, 99);
      this.cbProteases.Name = "cbProteases";
      this.cbProteases.Size = new System.Drawing.Size(124, 21);
      this.cbProteases.TabIndex = 99;
      // 
      // cbRemoveIsobaricIonsInHighRange
      // 
      this.cbRemoveIsobaricIonsInHighRange.Checked = true;
      this.cbRemoveIsobaricIonsInHighRange.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbRemoveIsobaricIonsInHighRange.Key = "rbRemoveHighMassRange";
      this.cbRemoveIsobaricIonsInHighRange.Location = new System.Drawing.Point(238, 154);
      this.cbRemoveIsobaricIonsInHighRange.Name = "cbRemoveIsobaricIonsInHighRange";
      this.cbRemoveIsobaricIonsInHighRange.PreCondition = this.cbRemoveIsobaricIons;
      this.cbRemoveIsobaricIonsInHighRange.Size = new System.Drawing.Size(322, 21);
      this.cbRemoveIsobaricIonsInHighRange.TabIndex = 98;
      this.cbRemoveIsobaricIonsInHighRange.Text = "Remove isobaric relative ion in high mass range";
      this.cbRemoveIsobaricIonsInHighRange.CheckedChanged += new System.EventHandler(this.cbxIsobaricTypes_SelectedIndexChanged);
      // 
      // cbRemoveIsobaricIons
      // 
      this.cbRemoveIsobaricIons.Key = "RemoveIsobaricIons";
      this.cbRemoveIsobaricIons.Location = new System.Drawing.Point(50, 72);
      this.cbRemoveIsobaricIons.Name = "cbRemoveIsobaricIons";
      this.cbRemoveIsobaricIons.PreCondition = this.cbRemoveMassRange;
      this.cbRemoveIsobaricIons.Size = new System.Drawing.Size(167, 21);
      this.cbRemoveIsobaricIons.TabIndex = 94;
      this.cbRemoveIsobaricIons.Text = "Remove isobaric ions:";
      // 
      // cbRemoveMassRange
      // 
      this.cbRemoveMassRange.AutoSize = true;
      this.cbRemoveMassRange.Key = "RemoveMassRange";
      this.cbRemoveMassRange.Location = new System.Drawing.Point(15, 18);
      this.cbRemoveMassRange.Name = "cbRemoveMassRange";
      this.cbRemoveMassRange.PreCondition = null;
      this.cbRemoveMassRange.Size = new System.Drawing.Size(162, 17);
      this.cbRemoveMassRange.TabIndex = 95;
      this.cbRemoveMassRange.Text = "Remove ions, window (+-Da)";
      this.cbRemoveMassRange.UseVisualStyleBackColor = true;
      // 
      // cbRemoveIsobaricIonsInLowRange
      // 
      this.cbRemoveIsobaricIonsInLowRange.Checked = true;
      this.cbRemoveIsobaricIonsInLowRange.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbRemoveIsobaricIonsInLowRange.Key = "rbRemoveLowMassRange";
      this.cbRemoveIsobaricIonsInLowRange.Location = new System.Drawing.Point(238, 127);
      this.cbRemoveIsobaricIonsInLowRange.Name = "cbRemoveIsobaricIonsInLowRange";
      this.cbRemoveIsobaricIonsInLowRange.PreCondition = this.cbRemoveIsobaricIons;
      this.cbRemoveIsobaricIonsInLowRange.Size = new System.Drawing.Size(334, 21);
      this.cbRemoveIsobaricIonsInLowRange.TabIndex = 97;
      this.cbRemoveIsobaricIonsInLowRange.Text = "Remove isobaric relative ion in low mass range";
      this.cbRemoveIsobaricIonsInLowRange.CheckedChanged += new System.EventHandler(this.cbxIsobaricTypes_SelectedIndexChanged);
      // 
      // txtIsobaricIons
      // 
      this.txtIsobaricIons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtIsobaricIons.Location = new System.Drawing.Point(368, 70);
      this.txtIsobaricIons.Name = "txtIsobaricIons";
      this.txtIsobaricIons.ReadOnly = true;
      this.txtIsobaricIons.Size = new System.Drawing.Size(640, 20);
      this.txtIsobaricIons.TabIndex = 96;
      this.txtIsobaricIons.TextChanged += new System.EventHandler(this.cbxIsobaricTypes_SelectedIndexChanged);
      // 
      // cbxIsobaricTypes
      // 
      this.cbxIsobaricTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbxIsobaricTypes.FormattingEnabled = true;
      this.cbxIsobaricTypes.Location = new System.Drawing.Point(238, 72);
      this.cbxIsobaricTypes.Name = "cbxIsobaricTypes";
      this.cbxIsobaricTypes.Size = new System.Drawing.Size(124, 21);
      this.cbxIsobaricTypes.TabIndex = 90;
      this.cbxIsobaricTypes.SelectedIndexChanged += new System.EventHandler(this.cbxIsobaricTypes_SelectedIndexChanged);
      // 
      // cbRemoveSpecialIons
      // 
      this.cbRemoveSpecialIons.Key = "RemoveSpecialIons";
      this.cbRemoveSpecialIons.Location = new System.Drawing.Point(50, 44);
      this.cbRemoveSpecialIons.Name = "cbRemoveSpecialIons";
      this.cbRemoveSpecialIons.PreCondition = this.cbRemoveMassRange;
      this.cbRemoveSpecialIons.Size = new System.Drawing.Size(182, 21);
      this.cbRemoveSpecialIons.TabIndex = 93;
      this.cbRemoveSpecialIons.Text = "Remove special ions: ";
      // 
      // txtRemoveIonWindow
      // 
      this.txtRemoveIonWindow.Location = new System.Drawing.Point(238, 15);
      this.txtRemoveIonWindow.Name = "txtRemoveIonWindow";
      this.txtRemoveIonWindow.Size = new System.Drawing.Size(124, 20);
      this.txtRemoveIonWindow.TabIndex = 91;
      this.txtRemoveIonWindow.TextChanged += new System.EventHandler(this.cbxIsobaricTypes_SelectedIndexChanged);
      // 
      // txtSpecialIons
      // 
      this.txtSpecialIons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtSpecialIons.Location = new System.Drawing.Point(238, 44);
      this.txtSpecialIons.Name = "txtSpecialIons";
      this.txtSpecialIons.Size = new System.Drawing.Size(770, 20);
      this.txtSpecialIons.TabIndex = 89;
      // 
      // tabPage3
      // 
      this.tabPage3.Controls.Add(this.panel1);
      this.tabPage3.Location = new System.Drawing.Point(4, 22);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage3.Size = new System.Drawing.Size(1075, 342);
      this.tabPage3.TabIndex = 2;
      this.tabPage3.Text = "Mass Calibration";
      this.tabPage3.UseVisualStyleBackColor = true;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.panel3);
      this.panel1.Controls.Add(this.panel2);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(3, 3);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(1069, 336);
      this.panel1.TabIndex = 0;
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.label11);
      this.panel3.Controls.Add(this.txtRetentionTimeWindow);
      this.panel3.Controls.Add(this.txtMaxShiftPPM);
      this.panel3.Controls.Add(this.label7);
      this.panel3.Controls.Add(this.siliconePolymers);
      this.panel3.Controls.Add(this.rbMassCalibrationByFixed);
      this.panel3.Controls.Add(this.rbMassCalibrationByFile);
      this.panel3.Controls.Add(this.rbMassCalibrationAutomatically);
      this.panel3.Controls.Add(this.txtShiftFile);
      this.panel3.Controls.Add(this.btnShiftFile);
      this.panel3.Controls.Add(this.txtPreductIonShift);
      this.panel3.Controls.Add(this.txtPredursorShift);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel3.Location = new System.Drawing.Point(0, 43);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(1069, 293);
      this.panel3.TabIndex = 101;
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.Location = new System.Drawing.Point(564, 65);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(185, 13);
      this.label11.TabIndex = 117;
      this.label11.Text = "smoothing retention time window (min)";
      // 
      // txtRetentionTimeWindow
      // 
      this.txtRetentionTimeWindow.Location = new System.Drawing.Point(797, 62);
      this.txtRetentionTimeWindow.Name = "txtRetentionTimeWindow";
      this.txtRetentionTimeWindow.Size = new System.Drawing.Size(58, 20);
      this.txtRetentionTimeWindow.TabIndex = 116;
      // 
      // txtMaxShiftPPM
      // 
      this.txtMaxShiftPPM.Location = new System.Drawing.Point(498, 62);
      this.txtMaxShiftPPM.Name = "txtMaxShiftPPM";
      this.txtMaxShiftPPM.Size = new System.Drawing.Size(58, 20);
      this.txtMaxShiftPPM.TabIndex = 115;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(453, 9);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(89, 13);
      this.label7.TabIndex = 114;
      this.label7.Text = "product ion (ppm)";
      // 
      // siliconePolymers
      // 
      this.siliconePolymers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.siliconePolymers.Location = new System.Drawing.Point(186, 87);
      this.siliconePolymers.Name = "siliconePolymers";
      this.siliconePolymers.SelectedIon = "355,371,388,429,445,462,503,519,536,593";
      this.siliconePolymers.Size = new System.Drawing.Size(703, 198);
      this.siliconePolymers.TabIndex = 113;
      // 
      // rbMassCalibrationByFixed
      // 
      this.rbMassCalibrationByFixed.AutoSize = true;
      this.rbMassCalibrationByFixed.Key = "ShiftByFixed";
      this.rbMassCalibrationByFixed.Location = new System.Drawing.Point(139, 7);
      this.rbMassCalibrationByFixed.Name = "rbMassCalibrationByFixed";
      this.rbMassCalibrationByFixed.PreCondition = null;
      this.rbMassCalibrationByFixed.Size = new System.Drawing.Size(187, 17);
      this.rbMassCalibrationByFixed.TabIndex = 112;
      this.rbMassCalibrationByFixed.Text = "By fixed offset, precursor ion (ppm)";
      this.rbMassCalibrationByFixed.UseVisualStyleBackColor = true;
      // 
      // rbMassCalibrationByFile
      // 
      this.rbMassCalibrationByFile.AutoSize = true;
      this.rbMassCalibrationByFile.Key = "ShiftByFile";
      this.rbMassCalibrationByFile.Location = new System.Drawing.Point(139, 35);
      this.rbMassCalibrationByFile.Name = "rbMassCalibrationByFile";
      this.rbMassCalibrationByFile.PreCondition = null;
      this.rbMassCalibrationByFile.Size = new System.Drawing.Size(82, 17);
      this.rbMassCalibrationByFile.TabIndex = 111;
      this.rbMassCalibrationByFile.Text = "By offset file";
      this.rbMassCalibrationByFile.UseVisualStyleBackColor = true;
      // 
      // rbMassCalibrationAutomatically
      // 
      this.rbMassCalibrationAutomatically.AutoSize = true;
      this.rbMassCalibrationAutomatically.Checked = true;
      this.rbMassCalibrationAutomatically.Key = "MassCalibrationAutomatically";
      this.rbMassCalibrationAutomatically.Location = new System.Drawing.Point(139, 63);
      this.rbMassCalibrationAutomatically.Name = "rbMassCalibrationAutomatically";
      this.rbMassCalibrationAutomatically.PreCondition = null;
      this.rbMassCalibrationAutomatically.Size = new System.Drawing.Size(278, 17);
      this.rbMassCalibrationAutomatically.TabIndex = 110;
      this.rbMassCalibrationAutomatically.TabStop = true;
      this.rbMassCalibrationAutomatically.Text = "By reference ion offset automatically, max offset (ppm)";
      this.rbMassCalibrationAutomatically.UseVisualStyleBackColor = true;
      // 
      // txtShiftFile
      // 
      this.txtShiftFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtShiftFile.Location = new System.Drawing.Point(455, 34);
      this.txtShiftFile.Name = "txtShiftFile";
      this.txtShiftFile.Size = new System.Drawing.Size(611, 20);
      this.txtShiftFile.TabIndex = 109;
      // 
      // btnShiftFile
      // 
      this.btnShiftFile.Location = new System.Drawing.Point(252, 31);
      this.btnShiftFile.Name = "btnShiftFile";
      this.btnShiftFile.Size = new System.Drawing.Size(195, 25);
      this.btnShiftFile.TabIndex = 108;
      this.btnShiftFile.Text = "button1";
      this.btnShiftFile.UseVisualStyleBackColor = true;
      // 
      // txtPreductIonShift
      // 
      this.txtPreductIonShift.Location = new System.Drawing.Point(566, 5);
      this.txtPreductIonShift.Name = "txtPreductIonShift";
      this.txtPreductIonShift.Size = new System.Drawing.Size(63, 20);
      this.txtPreductIonShift.TabIndex = 107;
      // 
      // txtPredursorShift
      // 
      this.txtPredursorShift.Location = new System.Drawing.Point(389, 5);
      this.txtPredursorShift.Name = "txtPredursorShift";
      this.txtPredursorShift.Size = new System.Drawing.Size(58, 20);
      this.txtPredursorShift.TabIndex = 106;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.rbMassCalibration);
      this.panel2.Controls.Add(this.rbMassCalibrationNone);
      this.panel2.Controls.Add(this.cbCalibrateProductIon);
      this.panel2.Controls.Add(this.cbCalibratePrecursor);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel2.Location = new System.Drawing.Point(0, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(1069, 43);
      this.panel2.TabIndex = 100;
      // 
      // rbMassCalibration
      // 
      this.rbMassCalibration.AutoSize = true;
      this.rbMassCalibration.Key = "MassCalibration";
      this.rbMassCalibration.Location = new System.Drawing.Point(83, 14);
      this.rbMassCalibration.Name = "rbMassCalibration";
      this.rbMassCalibration.PreCondition = null;
      this.rbMassCalibration.Size = new System.Drawing.Size(108, 17);
      this.rbMassCalibration.TabIndex = 102;
      this.rbMassCalibration.Text = "Calibrate mass of ";
      this.rbMassCalibration.UseVisualStyleBackColor = true;
      // 
      // rbMassCalibrationNone
      // 
      this.rbMassCalibrationNone.AutoSize = true;
      this.rbMassCalibrationNone.Checked = true;
      this.rbMassCalibrationNone.Key = "MassCalibrationAutomatically";
      this.rbMassCalibrationNone.Location = new System.Drawing.Point(12, 14);
      this.rbMassCalibrationNone.Name = "rbMassCalibrationNone";
      this.rbMassCalibrationNone.PreCondition = null;
      this.rbMassCalibrationNone.Size = new System.Drawing.Size(51, 17);
      this.rbMassCalibrationNone.TabIndex = 101;
      this.rbMassCalibrationNone.TabStop = true;
      this.rbMassCalibrationNone.Text = "None";
      this.rbMassCalibrationNone.UseVisualStyleBackColor = true;
      // 
      // cbCalibrateProductIon
      // 
      this.cbCalibrateProductIon.Checked = true;
      this.cbCalibrateProductIon.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbCalibrateProductIon.Key = "CalibrateProductIon";
      this.cbCalibrateProductIon.Location = new System.Drawing.Point(330, 14);
      this.cbCalibrateProductIon.Name = "cbCalibrateProductIon";
      this.cbCalibrateProductIon.PreCondition = null;
      this.cbCalibrateProductIon.Size = new System.Drawing.Size(97, 21);
      this.cbCalibrateProductIon.TabIndex = 100;
      this.cbCalibrateProductIon.Text = "Product ion";
      // 
      // cbCalibratePrecursor
      // 
      this.cbCalibratePrecursor.Checked = true;
      this.cbCalibratePrecursor.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbCalibratePrecursor.Key = "CalibratePrecursor";
      this.cbCalibratePrecursor.Location = new System.Drawing.Point(220, 14);
      this.cbCalibratePrecursor.Name = "cbCalibratePrecursor";
      this.cbCalibratePrecursor.PreCondition = null;
      this.cbCalibratePrecursor.Size = new System.Drawing.Size(104, 21);
      this.cbCalibratePrecursor.TabIndex = 99;
      this.cbCalibratePrecursor.Text = "Precursor ion";
      // 
      // tabPage4
      // 
      this.tabPage4.Controls.Add(this.cbRemoveIonsLargerThanPrecursor);
      this.tabPage4.Controls.Add(this.txtNeutralLossComposition);
      this.tabPage4.Controls.Add(this.cbRemovePrecursor);
      this.tabPage4.Location = new System.Drawing.Point(4, 22);
      this.tabPage4.Name = "tabPage4";
      this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage4.Size = new System.Drawing.Size(1075, 342);
      this.tabPage4.TabIndex = 3;
      this.tabPage4.Text = "Precursor";
      this.tabPage4.UseVisualStyleBackColor = true;
      // 
      // cbRemoveIonsLargerThanPrecursor
      // 
      this.cbRemoveIonsLargerThanPrecursor.Key = "RemoveIonsLargerThanPrecursor";
      this.cbRemoveIonsLargerThanPrecursor.Location = new System.Drawing.Point(20, 47);
      this.cbRemoveIonsLargerThanPrecursor.Name = "cbRemoveIonsLargerThanPrecursor";
      this.cbRemoveIonsLargerThanPrecursor.PreCondition = null;
      this.cbRemoveIonsLargerThanPrecursor.Size = new System.Drawing.Size(223, 21);
      this.cbRemoveIonsLargerThanPrecursor.TabIndex = 82;
      this.cbRemoveIonsLargerThanPrecursor.Text = "Remove ions larger than precursor";
      // 
      // txtNeutralLossComposition
      // 
      this.txtNeutralLossComposition.Location = new System.Drawing.Point(243, 20);
      this.txtNeutralLossComposition.Name = "txtNeutralLossComposition";
      this.txtNeutralLossComposition.Size = new System.Drawing.Size(92, 20);
      this.txtNeutralLossComposition.TabIndex = 81;
      this.txtNeutralLossComposition.Text = "NH3,OH,";
      // 
      // cbRemovePrecursor
      // 
      this.cbRemovePrecursor.Key = "RemovePrecursor";
      this.cbRemovePrecursor.Location = new System.Drawing.Point(20, 21);
      this.cbRemovePrecursor.Name = "cbRemovePrecursor";
      this.cbRemovePrecursor.PreCondition = null;
      this.cbRemovePrecursor.Size = new System.Drawing.Size(234, 21);
      this.cbRemovePrecursor.TabIndex = 80;
      this.cbRemovePrecursor.Text = "Remove precursor and neutral loss";
      // 
      // MultipleRaw2MgfProcessor3UI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1083, 816);
      this.Controls.Add(this.rawFiles);
      this.Controls.Add(this.tabControl1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "MultipleRaw2MgfProcessor3UI";
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.tabControl1, 0);
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.rawFiles, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.tabPage2.ResumeLayout(false);
      this.tabPage2.PerformLayout();
      this.tabPage3.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.panel3.ResumeLayout(false);
      this.panel3.PerformLayout();
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.tabPage4.ResumeLayout(false);
      this.tabPage4.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private RCPA.Gui.MultipleFileField rawFiles;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.ComboBox cbDefaultCharge;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.TextBox txtMinIonIntensity;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.ComboBox cbTitleFormat;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtMWRangeTo;
    private System.Windows.Forms.TextBox txtMinIonCount;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox txtMinIonIntensityThreshold;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtMWRangeFrom;
    private System.Windows.Forms.Label label2;
    private Gui.RcpaCheckField cbParallel;
    private Gui.RcpaCheckField cbByMode;
    private System.Windows.Forms.TextBox txtTopX;
    private Gui.RcpaCheckField cbKeepTopX;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.TextBox txtIsobaricIons;
    private System.Windows.Forms.ComboBox cbxIsobaricTypes;
    private Gui.RcpaCheckField cbRemoveMassRange;
    private Gui.RcpaCheckField cbRemoveIsobaricIons;
    private Gui.RcpaCheckField cbRemoveSpecialIons;
    private System.Windows.Forms.TextBox txtRemoveIonWindow;
    private System.Windows.Forms.TextBox txtSpecialIons;
    private System.Windows.Forms.TabPage tabPage3;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label label1;
    private Gui.RcpaCheckField cbDeconvolution;
    private System.Windows.Forms.TextBox txtDeisotopic;
    private Gui.RcpaCheckField cbDeisotopic;
    private System.Windows.Forms.TabPage tabPage4;
    private Gui.RcpaCheckField cbRemoveIonsLargerThanPrecursor;
    private System.Windows.Forms.TextBox txtNeutralLossComposition;
    private Gui.RcpaCheckField cbRemovePrecursor;
    private Gui.RcpaCheckField cbRemoveIsobaricIonsInHighRange;
    private Gui.RcpaCheckField cbRemoveIsobaricIonsInLowRange;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Panel panel2;
    private Gui.RcpaRadioField rbMassCalibration;
    private Gui.RcpaRadioField rbMassCalibrationNone;
    private Gui.RcpaCheckField cbCalibrateProductIon;
    private Gui.RcpaCheckField cbCalibratePrecursor;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.TextBox txtRetentionTimeWindow;
    private System.Windows.Forms.TextBox txtMaxShiftPPM;
    private System.Windows.Forms.Label label7;
    private SiliconePolymerIonField siliconePolymers;
    private Gui.RcpaRadioField rbMassCalibrationByFixed;
    private Gui.RcpaRadioField rbMassCalibrationByFile;
    private Gui.RcpaRadioField rbMassCalibrationAutomatically;
    private System.Windows.Forms.TextBox txtShiftFile;
    private System.Windows.Forms.Button btnShiftFile;
    private System.Windows.Forms.TextBox txtPreductIonShift;
    private System.Windows.Forms.TextBox txtPredursorShift;
    private Gui.RcpaCheckField cbByMsLevel;
    private System.Windows.Forms.ComboBox cbProteases;
  }
}