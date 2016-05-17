namespace SeqEdit
{
    partial class SeqEditForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvTable1 = new System.Windows.Forms.DataGridView();
            this.lblCableLoss = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtCableLoss = new System.Windows.Forms.TextBox();
            this.lblTableFormat = new System.Windows.Forms.Label();
            this.cmbTaleFormat = new System.Windows.Forms.ComboBox();
            this.tabSeqTables = new System.Windows.Forms.TabControl();
            this.pageTable1 = new System.Windows.Forms.TabPage();
            this.cmbPackage1 = new System.Windows.Forms.ComboBox();
            this.lblPackage1 = new System.Windows.Forms.Label();
            this.openSeqFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.lblTestTypeID = new System.Windows.Forms.Label();
            this.txtTestTypeID = new System.Windows.Forms.TextBox();
            this.lblCommon = new System.Windows.Forms.Label();
            this.txtCommon = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SegNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UplinkPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DownlinkPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Band = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NetworkSignaling = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DLChannel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UplinkLeveldBm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DownlinkPowerdBm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pattern = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CRNTI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrigSource = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.TrigSlope = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.TrigLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrigDelay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MeasStep = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Modulation = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.BandWidth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RBNumberOf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RBStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PowerMeasurement = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModulationAnalysis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SpuriousEmissionMask = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OccupiedBandwidth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AdjacentChannelLeakagePowerRatio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OffsetSegmentDuration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SegmentDuration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ULPower10dBm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaxTXPowerLimit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ULLCID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DLLCID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TXChain = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RxChain = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TXPowerControlMode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConfigOverride = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MCS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MeasureRxLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MeasureErrorRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tabSeqTables.SuspendLayout();
            this.pageTable1.SuspendLayout();
            this.menu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvTable1
            // 
            this.dgvTable1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTable1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTable1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SegNum,
            this.UplinkPort,
            this.DownlinkPort,
            this.Band,
            this.NetworkSignaling,
            this.DLChannel,
            this.UplinkLeveldBm,
            this.DownlinkPowerdBm,
            this.Pattern,
            this.CRNTI,
            this.TrigSource,
            this.TrigSlope,
            this.TrigLevel,
            this.TrigDelay,
            this.MeasStep,
            this.Modulation,
            this.BandWidth,
            this.RBNumberOf,
            this.RBStart,
            this.PowerMeasurement,
            this.ModulationAnalysis,
            this.SpuriousEmissionMask,
            this.OccupiedBandwidth,
            this.AdjacentChannelLeakagePowerRatio,
            this.OffsetSegmentDuration,
            this.SegmentDuration,
            this.ULPower10dBm,
            this.MaxTXPowerLimit,
            this.ULLCID,
            this.DLLCID,
            this.TXChain,
            this.RxChain,
            this.TXPowerControlMode,
            this.ConfigOverride,
            this.MCS,
            this.MeasureRxLevel,
            this.MeasureErrorRate,
            this.TestID});
            this.dgvTable1.Location = new System.Drawing.Point(6, 32);
            this.dgvTable1.Name = "dgvTable1";
            this.dgvTable1.RowTemplate.Height = 21;
            this.dgvTable1.Size = new System.Drawing.Size(772, 226);
            this.dgvTable1.TabIndex = 103;
            this.dgvTable1.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.dgvTable_SortCompare);
            // 
            // lblCableLoss
            // 
            this.lblCableLoss.AutoSize = true;
            this.lblCableLoss.Location = new System.Drawing.Point(6, 19);
            this.lblCableLoss.Name = "lblCableLoss";
            this.lblCableLoss.Size = new System.Drawing.Size(52, 12);
            this.lblCableLoss.TabIndex = 4;
            this.lblCableLoss.Text = "Loss File";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtCableLoss);
            this.groupBox2.Controls.Add(this.lblCableLoss);
            this.groupBox2.Location = new System.Drawing.Point(15, 78);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(332, 43);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "CableLoss";
            // 
            // txtCableLoss
            // 
            this.txtCableLoss.Location = new System.Drawing.Point(76, 16);
            this.txtCableLoss.Name = "txtCableLoss";
            this.txtCableLoss.Size = new System.Drawing.Size(218, 19);
            this.txtCableLoss.TabIndex = 5;
            // 
            // lblTableFormat
            // 
            this.lblTableFormat.AutoSize = true;
            this.lblTableFormat.Location = new System.Drawing.Point(21, 133);
            this.lblTableFormat.Name = "lblTableFormat";
            this.lblTableFormat.Size = new System.Drawing.Size(69, 12);
            this.lblTableFormat.TabIndex = 7;
            this.lblTableFormat.Text = "TableFormat";
            // 
            // cmbTaleFormat
            // 
            this.cmbTaleFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTaleFormat.FormattingEnabled = true;
            this.cmbTaleFormat.Items.AddRange(new object[] {
            "LTE",
            "WCDMA",
            "GSM"});
            this.cmbTaleFormat.Location = new System.Drawing.Point(129, 130);
            this.cmbTaleFormat.Name = "cmbTaleFormat";
            this.cmbTaleFormat.Size = new System.Drawing.Size(218, 20);
            this.cmbTaleFormat.TabIndex = 8;
            // 
            // tabSeqTables
            // 
            this.tabSeqTables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabSeqTables.Controls.Add(this.pageTable1);
            this.tabSeqTables.Location = new System.Drawing.Point(15, 156);
            this.tabSeqTables.Name = "tabSeqTables";
            this.tabSeqTables.SelectedIndex = 0;
            this.tabSeqTables.Size = new System.Drawing.Size(792, 290);
            this.tabSeqTables.TabIndex = 9;
            this.tabSeqTables.SelectedIndexChanged += new System.EventHandler(this.tabTables_SelectedIndexChanged);
            // 
            // pageTable1
            // 
            this.pageTable1.Controls.Add(this.cmbPackage1);
            this.pageTable1.Controls.Add(this.lblPackage1);
            this.pageTable1.Controls.Add(this.dgvTable1);
            this.pageTable1.Location = new System.Drawing.Point(4, 22);
            this.pageTable1.Name = "pageTable1";
            this.pageTable1.Padding = new System.Windows.Forms.Padding(3);
            this.pageTable1.Size = new System.Drawing.Size(784, 264);
            this.pageTable1.TabIndex = 100;
            this.pageTable1.Text = "Table1";
            this.pageTable1.UseVisualStyleBackColor = true;
            // 
            // cmbPackage1
            // 
            this.cmbPackage1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPackage1.FormattingEnabled = true;
            this.cmbPackage1.Location = new System.Drawing.Point(102, 6);
            this.cmbPackage1.Name = "cmbPackage1";
            this.cmbPackage1.Size = new System.Drawing.Size(218, 20);
            this.cmbPackage1.TabIndex = 102;
            // 
            // lblPackage1
            // 
            this.lblPackage1.AutoSize = true;
            this.lblPackage1.Location = new System.Drawing.Point(6, 9);
            this.lblPackage1.Name = "lblPackage1";
            this.lblPackage1.Size = new System.Drawing.Size(48, 12);
            this.lblPackage1.TabIndex = 101;
            this.lblPackage1.Text = "Package";
            // 
            // openSeqFileDlg
            // 
            this.openSeqFileDlg.AddExtension = false;
            this.openSeqFileDlg.CheckFileExists = false;
            this.openSeqFileDlg.DefaultExt = "txt";
            this.openSeqFileDlg.Title = "Open Sequence File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(50, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(49, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(819, 26);
            this.menu.TabIndex = 0;
            this.menu.Text = "menuStrip1";
            // 
            // lblTestTypeID
            // 
            this.lblTestTypeID.AutoSize = true;
            this.lblTestTypeID.Location = new System.Drawing.Point(6, 18);
            this.lblTestTypeID.Name = "lblTestTypeID";
            this.lblTestTypeID.Size = new System.Drawing.Size(64, 12);
            this.lblTestTypeID.TabIndex = 4;
            this.lblTestTypeID.Text = "TestTypeID";
            // 
            // txtTestTypeID
            // 
            this.txtTestTypeID.Location = new System.Drawing.Point(76, 15);
            this.txtTestTypeID.Name = "txtTestTypeID";
            this.txtTestTypeID.Size = new System.Drawing.Size(218, 19);
            this.txtTestTypeID.TabIndex = 5;
            // 
            // lblCommon
            // 
            this.lblCommon.AutoSize = true;
            this.lblCommon.Location = new System.Drawing.Point(391, 18);
            this.lblCommon.Name = "lblCommon";
            this.lblCommon.Size = new System.Drawing.Size(72, 12);
            this.lblCommon.TabIndex = 6;
            this.lblCommon.Text = "Common File";
            // 
            // txtCommon
            // 
            this.txtCommon.Location = new System.Drawing.Point(469, 15);
            this.txtCommon.Name = "txtCommon";
            this.txtCommon.Size = new System.Drawing.Size(218, 19);
            this.txtCommon.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCommon);
            this.groupBox1.Controls.Add(this.lblCommon);
            this.groupBox1.Controls.Add(this.txtTestTypeID);
            this.groupBox1.Controls.Add(this.lblTestTypeID);
            this.groupBox1.Location = new System.Drawing.Point(15, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(693, 43);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Common";
            // 
            // SegNum
            // 
            this.SegNum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle31.BackColor = System.Drawing.Color.LightGray;
            this.SegNum.DefaultCellStyle = dataGridViewCellStyle31;
            this.SegNum.HeaderText = "Seg#";
            this.SegNum.Name = "SegNum";
            this.SegNum.ReadOnly = true;
            this.SegNum.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SegNum.ToolTipText = "セグメント番号";
            this.SegNum.Width = 55;
            // 
            // UplinkPort
            // 
            this.UplinkPort.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle32.BackColor = System.Drawing.Color.LightGray;
            this.UplinkPort.DefaultCellStyle = dataGridViewCellStyle32;
            this.UplinkPort.HeaderText = "TXPort";
            this.UplinkPort.Name = "UplinkPort";
            this.UplinkPort.ReadOnly = true;
            this.UplinkPort.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.UplinkPort.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UplinkPort.ToolTipText = "Uplink用のテスターポート番号";
            this.UplinkPort.Width = 46;
            // 
            // DownlinkPort
            // 
            this.DownlinkPort.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle33.BackColor = System.Drawing.Color.LightGray;
            this.DownlinkPort.DefaultCellStyle = dataGridViewCellStyle33;
            this.DownlinkPort.HeaderText = "RXPort";
            this.DownlinkPort.Name = "DownlinkPort";
            this.DownlinkPort.ReadOnly = true;
            this.DownlinkPort.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DownlinkPort.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DownlinkPort.ToolTipText = "Downlink用のテスターポート番号";
            this.DownlinkPort.Width = 47;
            // 
            // Band
            // 
            this.Band.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Band.HeaderText = "BC";
            this.Band.Name = "Band";
            this.Band.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Band.Width = 46;
            // 
            // NetworkSignaling
            // 
            this.NetworkSignaling.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.NetworkSignaling.HeaderText = "NetSig";
            this.NetworkSignaling.Name = "NetworkSignaling";
            this.NetworkSignaling.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.NetworkSignaling.Width = 64;
            // 
            // DLChannel
            // 
            this.DLChannel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DLChannel.HeaderText = "DLChannel";
            this.DLChannel.Name = "DLChannel";
            this.DLChannel.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DLChannel.Width = 85;
            // 
            // UplinkLeveldBm
            // 
            this.UplinkLeveldBm.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.UplinkLeveldBm.HeaderText = "ULPow";
            this.UplinkLeveldBm.Name = "UplinkLeveldBm";
            this.UplinkLeveldBm.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.UplinkLeveldBm.Width = 65;
            // 
            // DownlinkPowerdBm
            // 
            this.DownlinkPowerdBm.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DownlinkPowerdBm.HeaderText = "DLPow";
            this.DownlinkPowerdBm.Name = "DownlinkPowerdBm";
            this.DownlinkPowerdBm.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DownlinkPowerdBm.Width = 65;
            // 
            // Pattern
            // 
            this.Pattern.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Pattern.HeaderText = "DLPAT";
            this.Pattern.Name = "Pattern";
            this.Pattern.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Pattern.Width = 66;
            // 
            // CRNTI
            // 
            this.CRNTI.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CRNTI.HeaderText = "CRNTI";
            this.CRNTI.Name = "CRNTI";
            this.CRNTI.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CRNTI.Width = 64;
            // 
            // TrigSource
            // 
            this.TrigSource.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TrigSource.HeaderText = "TrigSrc";
            this.TrigSource.Items.AddRange(new object[] {
            "FREERUN",
            "PWR",
            "FRAME"});
            this.TrigSource.Name = "TrigSource";
            this.TrigSource.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TrigSource.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.TrigSource.Width = 67;
            // 
            // TrigSlope
            // 
            this.TrigSlope.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TrigSlope.HeaderText = "TrigSlope";
            this.TrigSlope.Items.AddRange(new object[] {
            "RIZE",
            "FALL"});
            this.TrigSlope.Name = "TrigSlope";
            this.TrigSlope.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TrigSlope.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.TrigSlope.Width = 78;
            // 
            // TrigLevel
            // 
            this.TrigLevel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TrigLevel.HeaderText = "TrigLvl";
            this.TrigLevel.Name = "TrigLevel";
            this.TrigLevel.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TrigLevel.Width = 65;
            // 
            // TrigDelay
            // 
            this.TrigDelay.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TrigDelay.HeaderText = "TrigDelay";
            this.TrigDelay.Name = "TrigDelay";
            this.TrigDelay.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TrigDelay.Width = 79;
            // 
            // MeasStep
            // 
            this.MeasStep.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MeasStep.HeaderText = "MeasStep";
            this.MeasStep.Name = "MeasStep";
            this.MeasStep.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.MeasStep.Width = 80;
            // 
            // Modulation
            // 
            this.Modulation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Modulation.HeaderText = "Modulation";
            this.Modulation.Items.AddRange(new object[] {
            "QPSK",
            "16QAM"});
            this.Modulation.Name = "Modulation";
            this.Modulation.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Modulation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Modulation.Width = 85;
            // 
            // BandWidth
            // 
            this.BandWidth.HeaderText = "BW";
            this.BandWidth.Name = "BandWidth";
            // 
            // RBNumberOf
            // 
            this.RBNumberOf.HeaderText = "NumRB";
            this.RBNumberOf.Name = "RBNumberOf";
            // 
            // RBStart
            // 
            this.RBStart.HeaderText = "RBStart";
            this.RBStart.Name = "RBStart";
            // 
            // PowerMeasurement
            // 
            this.PowerMeasurement.HeaderText = "POW";
            this.PowerMeasurement.Name = "PowerMeasurement";
            // 
            // ModulationAnalysis
            // 
            this.ModulationAnalysis.HeaderText = "MOD";
            this.ModulationAnalysis.Name = "ModulationAnalysis";
            // 
            // SpuriousEmissionMask
            // 
            this.SpuriousEmissionMask.HeaderText = "SEM";
            this.SpuriousEmissionMask.Name = "SpuriousEmissionMask";
            // 
            // OccupiedBandwidth
            // 
            this.OccupiedBandwidth.HeaderText = "OBW";
            this.OccupiedBandwidth.Name = "OccupiedBandwidth";
            // 
            // AdjacentChannelLeakagePowerRatio
            // 
            this.AdjacentChannelLeakagePowerRatio.HeaderText = "ACLR";
            this.AdjacentChannelLeakagePowerRatio.Name = "AdjacentChannelLeakagePowerRatio";
            // 
            // OffsetSegmentDuration
            // 
            this.OffsetSegmentDuration.HeaderText = "Offset";
            this.OffsetSegmentDuration.Name = "OffsetSegmentDuration";
            // 
            // SegmentDuration
            // 
            this.SegmentDuration.HeaderText = "SegDur";
            this.SegmentDuration.Name = "SegmentDuration";
            // 
            // ULPower10dBm
            // 
            this.ULPower10dBm.HeaderText = "ULPower10dBm";
            this.ULPower10dBm.Name = "ULPower10dBm";
            // 
            // MaxTXPowerLimit
            // 
            this.MaxTXPowerLimit.HeaderText = "MaxTXPowerLimit";
            this.MaxTXPowerLimit.Name = "MaxTXPowerLimit";
            // 
            // ULLCID
            // 
            this.ULLCID.HeaderText = "ULLCID";
            this.ULLCID.Name = "ULLCID";
            // 
            // DLLCID
            // 
            this.DLLCID.HeaderText = "DLLCID";
            this.DLLCID.Name = "DLLCID";
            // 
            // TXChain
            // 
            this.TXChain.HeaderText = "TXChain";
            this.TXChain.Name = "TXChain";
            // 
            // RxChain
            // 
            this.RxChain.HeaderText = "RxChain";
            this.RxChain.Name = "RxChain";
            // 
            // TXPowerControlMode
            // 
            this.TXPowerControlMode.HeaderText = "TXPowerControlMode";
            this.TXPowerControlMode.Name = "TXPowerControlMode";
            // 
            // ConfigOverride
            // 
            this.ConfigOverride.HeaderText = "ConfigOverride";
            this.ConfigOverride.Name = "ConfigOverride";
            // 
            // MCS
            // 
            this.MCS.HeaderText = "MCS";
            this.MCS.Name = "MCS";
            // 
            // MeasureRxLevel
            // 
            this.MeasureRxLevel.HeaderText = "MeasureRxLevel";
            this.MeasureRxLevel.Name = "MeasureRxLevel";
            // 
            // MeasureErrorRate
            // 
            this.MeasureErrorRate.HeaderText = "MeasureErrorRate";
            this.MeasureErrorRate.Name = "MeasureErrorRate";
            // 
            // TestID
            // 
            this.TestID.HeaderText = "TestID";
            this.TestID.Name = "TestID";
            // 
            // SeqEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 458);
            this.Controls.Add(this.tabSeqTables);
            this.Controls.Add(this.cmbTaleFormat);
            this.Controls.Add(this.lblTableFormat);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menu);
            this.MainMenuStrip = this.menu;
            this.Name = "SeqEditForm";
            this.Text = "SeqEdit";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabSeqTables.ResumeLayout(false);
            this.pageTable1.ResumeLayout(false);
            this.pageTable1.PerformLayout();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTable1;
        private System.Windows.Forms.Label lblCableLoss;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtCableLoss;
        private System.Windows.Forms.Label lblTableFormat;
        private System.Windows.Forms.ComboBox cmbTaleFormat;
        private System.Windows.Forms.TabControl tabSeqTables;
        private System.Windows.Forms.TabPage pageTable1;
        private System.Windows.Forms.Label lblPackage1;
        private System.Windows.Forms.ComboBox cmbPackage1;
        private System.Windows.Forms.OpenFileDialog openSeqFileDlg;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.Label lblTestTypeID;
        private System.Windows.Forms.TextBox txtTestTypeID;
        private System.Windows.Forms.Label lblCommon;
        private System.Windows.Forms.TextBox txtCommon;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SegNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn UplinkPort;
        private System.Windows.Forms.DataGridViewTextBoxColumn DownlinkPort;
        private System.Windows.Forms.DataGridViewTextBoxColumn Band;
        private System.Windows.Forms.DataGridViewTextBoxColumn NetworkSignaling;
        private System.Windows.Forms.DataGridViewTextBoxColumn DLChannel;
        private System.Windows.Forms.DataGridViewTextBoxColumn UplinkLeveldBm;
        private System.Windows.Forms.DataGridViewTextBoxColumn DownlinkPowerdBm;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pattern;
        private System.Windows.Forms.DataGridViewTextBoxColumn CRNTI;
        private System.Windows.Forms.DataGridViewComboBoxColumn TrigSource;
        private System.Windows.Forms.DataGridViewComboBoxColumn TrigSlope;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrigLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrigDelay;
        private System.Windows.Forms.DataGridViewTextBoxColumn MeasStep;
        private System.Windows.Forms.DataGridViewComboBoxColumn Modulation;
        private System.Windows.Forms.DataGridViewTextBoxColumn BandWidth;
        private System.Windows.Forms.DataGridViewTextBoxColumn RBNumberOf;
        private System.Windows.Forms.DataGridViewTextBoxColumn RBStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn PowerMeasurement;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModulationAnalysis;
        private System.Windows.Forms.DataGridViewTextBoxColumn SpuriousEmissionMask;
        private System.Windows.Forms.DataGridViewTextBoxColumn OccupiedBandwidth;
        private System.Windows.Forms.DataGridViewTextBoxColumn AdjacentChannelLeakagePowerRatio;
        private System.Windows.Forms.DataGridViewTextBoxColumn OffsetSegmentDuration;
        private System.Windows.Forms.DataGridViewTextBoxColumn SegmentDuration;
        private System.Windows.Forms.DataGridViewTextBoxColumn ULPower10dBm;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxTXPowerLimit;
        private System.Windows.Forms.DataGridViewTextBoxColumn ULLCID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DLLCID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TXChain;
        private System.Windows.Forms.DataGridViewTextBoxColumn RxChain;
        private System.Windows.Forms.DataGridViewTextBoxColumn TXPowerControlMode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConfigOverride;
        private System.Windows.Forms.DataGridViewTextBoxColumn MCS;
        private System.Windows.Forms.DataGridViewTextBoxColumn MeasureRxLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn MeasureErrorRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestID;
    }
}