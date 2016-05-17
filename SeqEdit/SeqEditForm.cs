using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SeqEdit
{
    public partial class SeqEditForm : Form
    {
        [System.Runtime.InteropServices.DllImport("KERNEL32.DLL")]
        public static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

        [System.Runtime.InteropServices.DllImport("KERNEL32.DLL")]
        public static extern uint GetPrivateProfileInt(string lpAppName, string lpKeyName, int nDefault, string lpFileName);

        [System.Runtime.InteropServices.DllImport("KERNEL32.DLL")]
        public static extern uint WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

        public static string GetPrivateProfileString(string sPathName, string sAppName, string sKeyName, string sDefault)
        {
            string sGetValue = sDefault;
            try
            {
                int iSize = 1024;
                StringBuilder sValue = new StringBuilder(iSize);

                GetPrivateProfileString(sAppName, sKeyName, sDefault, sValue, iSize, sPathName);
                sGetValue = sValue.ToString();
                int p;
                if ((p = sGetValue.IndexOf(";")) != (-1)) sGetValue = sGetValue.Substring(0, p);
                if ((p = sGetValue.IndexOf("#")) != (-1)) sGetValue = sGetValue.Substring(0, p);
            }
            catch { }

            return sGetValue;
        }

        public SeqEditForm()
        {
            InitializeComponent();
            System.Environment.CurrentDirectory = Application.StartupPath;
            openSeqFileDlg.InitialDirectory = System.Environment.CurrentDirectory;
        }

        #region GUIEventHandler
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openSeqFileDlg.FileName != "")
            {
                openSeqFileDlg.InitialDirectory = System.IO.Path.GetDirectoryName(openSeqFileDlg.FileName);
            }
            if (openSeqFileDlg.ShowDialog() == DialogResult.OK)
            {
                LoadSeqFile(openSeqFileDlg.FileName);
            }
        }

        private void dgvTable_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            //指定されたセルの値を文字列として取得する
            string str1 = (e.CellValue1 == null ? "" : e.CellValue1.ToString());
            string str2 = (e.CellValue2 == null ? "" : e.CellValue2.ToString());

            try
            {
                // まずDouble値として比較
                double dbl1 = Convert.ToDouble(str1);
                double dbl2 = Convert.ToDouble(str2);

                e.SortResult = (int)((dbl1 - dbl2) * 1000.0);
            }
            catch
            {
                // どちらかのセルがDouble値に変換できない場合、文字列として比較
                e.SortResult = str1.CompareTo(str2);
            }

            // 比較結果が同じ場合、セグメント番号で比較する
            if (e.SortResult == 0 && e.Column.Name != "SegNum")
            {
                DataGridView dgvTable = (DataGridView)sender;
                str1 = dgvTable.Rows[e.RowIndex1].Cells["SegNum"].Value.ToString();
                str2 = dgvTable.Rows[e.RowIndex2].Cells["SegNum"].Value.ToString();

                int segNum1 = Convert.ToInt32(str1);
                int segNum2 = Convert.ToInt32(str2);

                e.SortResult = segNum1 - segNum2;
            }

            //処理したことを知らせる
            e.Handled = true;
        }

        private void tabTables_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region CommonGUI
        private void CreateTextBoxColumn(string name, string header, string toolTip, bool readOnly, out DataGridViewTextBoxColumn colmn)
        {
            colmn = new DataGridViewTextBoxColumn();
            if (readOnly)
            {
                DataGridViewCellStyle CellStyle = new DataGridViewCellStyle();
                CellStyle.BackColor = Color.LightGray;
                colmn.DefaultCellStyle = CellStyle;
            }
            colmn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            colmn.HeaderText = header;
            colmn.Name = name;
            colmn.ReadOnly = readOnly;
            colmn.Resizable = DataGridViewTriState.False;
            colmn.SortMode = DataGridViewColumnSortMode.Automatic;
            colmn.ToolTipText = toolTip;
        }
        private void CreateComboBoxColumn(string name, string header, string[] items, string toolTip, out DataGridViewComboBoxColumn colmn)
        {
            colmn = new DataGridViewComboBoxColumn();
            colmn.AutoComplete = true;
            colmn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            colmn.HeaderText = header;
            colmn.Items.AddRange(items);
            colmn.Name = name;
            colmn.ReadOnly = false;
            colmn.Resizable = DataGridViewTriState.False;
            colmn.SortMode = DataGridViewColumnSortMode.Automatic;
            colmn.ToolTipText = toolTip;
        }
        #endregion

        #region CommonSeqFile
        private int GetTableLength(string seqFile)
        {
            int tblIndex = 0;
            while (true)
            {
                // [Table??]セクションに、Package値とSegment1値が存在する場合、
                // その[Table??]セクションが存在すると判定する
                string package = GetPrivateProfileString(seqFile, "Table" + (tblIndex + 1), "Package", "");
                if (package == "") break;
                else
                {
                    string segment = GetPrivateProfileString(seqFile, "Table" + (tblIndex + 1), "Segment1", "");
                    if (segment == "") break;
                }
                tblIndex++;
            }
            return tblIndex;
        }
        private int GetSegmentLength(string seqFile, int tblIndex)
        {
            int segIndex = 0;
            while (true)
            {
                // [Table]セクションに、Segment??値が存在する場合、
                // その[Table]セクションにSegment??が存在すると判定する
                string segment = GetPrivateProfileString(seqFile, "Table" + tblIndex, "Segment" + (segIndex + 1), "");
                if (segment == "") break;
                segIndex++;
            }
            return segIndex;
        }

        private void LoadSeqFile(string seqFile)
        {
            this.Text = seqFile + " - SeqEdit";

            // Commonグループ
            txtTestTypeID.Text = GetPrivateProfileString(seqFile, "Common", "TestTypeID", "");
            txtCommon.Text = GetPrivateProfileString(seqFile, "Common", "Common", "");

            // CableLossグループ
            txtCableLoss.Text = GetPrivateProfileString(seqFile, "CableLoss", "CableLoss", "");

            // Segmentのパラメータ数からTableFormatを推測する
            string segment = GetPrivateProfileString(seqFile, "Table1", "Segment1", "");
            if (segment != "")
            {
                string[] parameters = segment.Split('\t');
                if (parameters.Length == 37)
                    cmbTaleFormat.Text = "LTE";
                else if (parameters.Length == 26)
                    cmbTaleFormat.Text = "WCDMA";
                else
                    cmbTaleFormat.Text = "";
            }
            else
            {
                cmbTaleFormat.Text = "";
            }

            // GUIデザインしやすくするために、GUIデザイナーでいくつかのTabPageを追加しているが、
            // テーブルロード前に、一旦全てのTanPageを消す
            tabSeqTables.Controls.Clear();

            // 全Tableをロードして表示する
            int tblLength = GetTableLength(seqFile);
            for (int i = 1; i <= tblLength; i++)
            {
                if (cmbTaleFormat.Text == "LTE")
                    LoadLTETable(seqFile, i, tabSeqTables);
                else if (cmbTaleFormat.Text == "WCDMA")
                    LoadWCDMATable(seqFile, i, tabSeqTables);
                else if (cmbTaleFormat.Text == "GSM")
                    LoadGSMTable(seqFile, i, tabSeqTables);
            }
        }
        #endregion

        #region LTE
        #region LTEChan
        private struct LTEChanInfo
        {
            public int Band;

            public struct Channel
            {
                public double FreqLowMHz;
                public int ChanOff;

                public struct Range
                {
                    public int Lower;
                    public int Upper;

                    public Range(int lower, int upper)
                    {
                        Lower = lower;
                        Upper = upper;
                    }
                };
                public Range ChanRange;

                public Channel(double freqLowMHz, int chanOff, int lower, int upper)
                {
                    FreqLowMHz = freqLowMHz;
                    ChanOff = chanOff;
                    ChanRange = new Range(lower, upper);
                }
            };
            public Channel Downlink;
            public Channel Uplink;

            public LTEChanInfo(int band,
                double down_freqLowMHz, int down_chanOff, int down_lower, int down_upper,
                double up_freqLowMHz, int up_chanOff, int up_lower, int up_upper
                )
            {
                Band = band;
                Downlink = new Channel(down_freqLowMHz, down_chanOff, down_lower, down_upper);
                Uplink = new Channel(up_freqLowMHz, up_chanOff, up_lower, up_upper);
            }
        };

        private LTEChanInfo[] LTEInfo = new LTEChanInfo[] {
            new LTEChanInfo( 1, 2110.0,     0,     0,   599, 1920.0, 18000, 18000, 18599),
            new LTEChanInfo( 2, 1930.0,   600,   600,  1199, 1850.0, 18600, 18600, 19199),
            new LTEChanInfo( 3, 1805.0,  1200,  1200,  1949, 1710.0, 19200, 19200, 19949),
            new LTEChanInfo( 4, 2110.0,  1950,  1950,  2399, 1710.0, 19950, 19950, 20399),
            new LTEChanInfo( 5,  869.0,  2400,  2400,  2649,  824.0, 20400, 20400, 20649),
            new LTEChanInfo( 6,  875.0,  2650,  2650,  2749,  830.0, 20650, 20650, 20749),
            new LTEChanInfo( 7, 2620.0,  2750,  2750,  3449, 2500.0, 20750, 20750, 21449),
            new LTEChanInfo( 8,  925.0,  3450,  3450,  3799,  880.0, 21450, 21450, 21799),
            new LTEChanInfo( 9, 1844.9,  3800,  3800,  4149, 1749.9, 21800, 21800, 22149),
            new LTEChanInfo(10, 2110.0,  4150,  4150,  4749, 1710.0, 22150, 22150, 22749),
            new LTEChanInfo(11, 1475.9,  4750,  4750,  4949, 1427.9, 22750, 22750, 22949),
            new LTEChanInfo(12,  729.0,  5010,  5010,  5179,  699.0, 23010, 23010, 23179),
            new LTEChanInfo(13,  746.0,  5180,  5180,  5279,  777.0, 23180, 23180, 23279),
            new LTEChanInfo(14,  758.0,  5280,  5280,  5379,  788.0, 23280, 23280, 23379),
            new LTEChanInfo(17,  734.0,  5730,  5730,  5849,  704.0, 23730, 23730, 23849),
            new LTEChanInfo(18,  860.0,  5850,  5850,  5999,  815.0, 23850, 23850, 23999),
            new LTEChanInfo(19,  875.0,  6000,  6000,  6149,  830.0, 24000, 24000, 24149),
            new LTEChanInfo(20,  791.0,  6150,  6150,  6449,  832.0, 24150, 24150, 24449),
            new LTEChanInfo(21, 1495.9,  6450,  6450,  6599, 1447.9, 24450, 24450, 24599),
            new LTEChanInfo(22, 3510.0,  6600,  6600,  7399, 3410.0, 24600, 24600, 25399),
            new LTEChanInfo(23, 2180.0,  7500,  7500,  7699, 2000.0, 25500, 25500, 25699),
            new LTEChanInfo(24, 1525.0,  7700,  7700,  8039, 1626.5, 25700, 25700, 26039),
            new LTEChanInfo(25, 1930.0,  8040,  8040,  8689, 1850.0, 26040, 26040, 26689),
            new LTEChanInfo(26,  859.0,  8690,  8690,  9039,  814.0, 26690, 26690, 27039),
            new LTEChanInfo(27,  852.0,  9040,  9040,  9209,  807.0, 27040, 27040, 27209),
            new LTEChanInfo(28,  758.0,  9210,  9210,  9659,  703.0, 27210, 27210, 27659),
            new LTEChanInfo(33, 1900.0, 36000, 36000, 36199, 1900.0, 36000, 36000, 36199),
            new LTEChanInfo(34, 2010.0, 36200, 36200, 36349, 2010.0, 36200, 36200, 36349),
            new LTEChanInfo(35, 1850.0, 36350, 36350, 36949, 1850.0, 36350, 36350, 36949),
            new LTEChanInfo(36, 1930.0, 36950, 36950, 37549, 1930.0, 36950, 36950, 37549),
            new LTEChanInfo(37, 1910.0, 37550, 37550, 37749, 1910.0, 37550, 37550, 37749),
            new LTEChanInfo(38, 2570.0, 37750, 37750, 38249, 2570.0, 37750, 37750, 38249),
            new LTEChanInfo(39, 1880.0, 38250, 38250, 38649, 1880.0, 38250, 38250, 38649),
            new LTEChanInfo(40, 2300.0, 38650, 38650, 39649, 2300.0, 38650, 38650, 39649),
            new LTEChanInfo(41, 2496.0, 39650, 39650, 41589, 2496.0, 39650, 39650, 41589),
            new LTEChanInfo(42, 3400.0, 41590, 41590, 43589, 3400.0, 41590, 41590, 43589),
            new LTEChanInfo(43, 3600.0, 43590, 43590, 45589, 3600.0, 43590, 43590, 45589),
            new LTEChanInfo(0,       0,     0,     0,     0,      0,     0,     0,     0)
        };

        private int LTEDLChan2ULChan(int DLChannel)
        {
            return DLChannel + 18000;
        }

        private int LTEULChan2BC(int ULChannel)
        {
            int ret = -1;
            for (int index = 0; LTEInfo[index].Band > 0; index++)
            {
                if ((LTEInfo[index].Uplink.ChanRange.Lower <= ULChannel) &&
                (LTEInfo[index].Uplink.ChanRange.Upper >= ULChannel))
                {
                    ret = index;
                    break;
                }
            }
            return ret;
        }

        private double LTEDLChan2FreqMHz(int DLChannel)
        {
            double ret = -1;
            int index = LTEULChan2BC(LTEDLChan2ULChan(DLChannel));
            if (index != -1)
            {
                ret = LTEInfo[index].Downlink.FreqLowMHz + 0.1 * (DLChannel - LTEInfo[index].Downlink.ChanOff);
            }
            return ret;
        }
        private double LTEULChan2FreqMHz(int ULChannel)
        {
            double ret = -1;
            int index = LTEULChan2BC(ULChannel);
            if (index != -1)
            {
                ret = LTEInfo[index].Uplink.FreqLowMHz + 0.1 * (ULChannel - LTEInfo[index].Uplink.ChanOff);
            }
            return ret;
        }
        #endregion

        private void CreateLTEPackage(string seqFile, int tblIndex, out Label lblPackage, out ComboBox cmbPackage)
        {
            lblPackage = new Label();
            lblPackage.AutoSize = true;
            lblPackage.Location = new System.Drawing.Point(6, 9);
            lblPackage.Name = "lblPackage" + tblIndex;
            lblPackage.Size = new System.Drawing.Size(48, 12);
            lblPackage.TabIndex = (tblIndex * 100) + 1;
            lblPackage.Text = "Package";

            cmbPackage = new ComboBox();
            cmbPackage.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPackage.FormattingEnabled = true;
            cmbPackage.Items.AddRange(new object[] {
                "MV887013A_LTEFDD_0002", "MV887013A_LTEFDD_0003", "MV887013A_LTEFDD_0004",
                "MV887013A_LTEFDD_0005", "MV887013A_LTEFDD_0007", "MV887013A_LTEFDD_0011",
                "MV887013A_LTEFDD_0013", "MV887013A_LTEFDD_0014"
            });
            cmbPackage.Location = new System.Drawing.Point(102, 6);
            cmbPackage.Name = "cmbPackage" + tblIndex;
            cmbPackage.Size = new System.Drawing.Size(218, 20);
            cmbPackage.TabIndex = (tblIndex * 100) + 2;

            cmbPackage.Text = GetPrivateProfileString(seqFile, "Table" + tblIndex, "Package", "");
        }

        private void CreateLTEDataGrid(string seqFile, int tblIndex, out DataGridView dgvTable)
        {
            // SegNum列作成
            DataGridViewTextBoxColumn SegNum;
            CreateTextBoxColumn("SegNum", "Seg#", "セグメント番号", true, out SegNum);

            // UplinkPort列作成 parameters[ 0 ]
            DataGridViewTextBoxColumn UplinkPort = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("UplinkPort", "ULPort", "Uplinkテスターポート番号(無効)", true, out UplinkPort);
            // DownlinkPort列作成 parameters[ 1 ]
            DataGridViewTextBoxColumn DownlinkPort = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("DownlinkPort", "DLPort", "Downlinkテスターポート番号(無効)", true, out DownlinkPort);

            // BandClass列作成 parameters[ 2 ]
            DataGridViewTextBoxColumn Band = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("Band", "BC", "バンドクラス(2,4,5,12,13)", false, out Band);

            // NetworkSignalingValue列作成 parameters[ 3 ]
            DataGridViewTextBoxColumn NetworkSignaling = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("NetworkSignaling", "NetSig", "ネットワークシグナリング値", false, out NetworkSignaling);

            // DLChannel列作成 parameters[ 4 ]
            DataGridViewTextBoxColumn DLChannel = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("DLChannel", "DLChan", "Downlinkチャネル番号", false, out DLChannel);
            DataGridViewTextBoxColumn DownlinkFrequencyMHz = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("DownlinkFrequencyMHz", "DLFreq", "Downlink周波数(DLChanから計算)", true, out DownlinkFrequencyMHz);
            DataGridViewTextBoxColumn ULChannel = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("ULChannel", "ULChan", "Upwnlinkチャネル番号(DLChanから計算)", true, out ULChannel);
            DataGridViewTextBoxColumn UplinkFrequencyMHz = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("UplinkFrequencyMHz", "ULFreq", "Uplink周波数(DLChanから計算)", true, out UplinkFrequencyMHz);

            // UplinkLeveldBm列作成 parameters[ 5 ]
            DataGridViewTextBoxColumn UplinkLeveldBm = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("UplinkLeveldBm", "ULPow", "テスターUplink入力レベル(dBm)", false, out UplinkLeveldBm);
            // DownlinkPowerdBm列作成 parameters[ 6 ]
            DataGridViewTextBoxColumn DownlinkPowerdBm = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("DownlinkPowerdBm", "DLPow", "テスターDownlink出力レベル(dBm)", false, out DownlinkPowerdBm);
            // Pattern列作成 parameters[ 7 ]
            DataGridViewComboBoxColumn Pattern = new DataGridViewComboBoxColumn();
            string[] patternItems = new string[] {
                "PAT1", "PAT2", "PAT3", "PAT4", "PAT5", "PAT6", "PAT7", "PAT8", "PAT9", "PAT10", "PAT11", "PAT12", "CW", "OFF", "NC"
            };
            CreateComboBoxColumn("Pattern", "DLPAT", patternItems, "テスターDownlink波形パターン", out Pattern);

            // CRNTI列作成 parameters[ 8 ]
            DataGridViewTextBoxColumn CRNTI = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("CRNTI", "CRNTI", "端末Downlink受信設定 C-RNTI(Cell Radio Network Temporary Identifier)", false, out CRNTI);

            // TrigSource列作成 parameters[ 9 ]
            DataGridViewComboBoxColumn TrigSource = new DataGridViewComboBoxColumn();
            string[] trigSrcItems = new string[] { "FREERUN", "PWR", "FRAME" };
            CreateComboBoxColumn("TrigSource", "TrigSrc", trigSrcItems, "トリガーソース(FREERUN:トリガーなし, PWR:入力パワー, FRAME:フレーム波形)", out TrigSource);
            // TrigSlope列作成 parameters[ 10 ]
            DataGridViewComboBoxColumn TrigSlope = new DataGridViewComboBoxColumn();
            string[] trigSlopeItems = new string[] { "RISE", "FALL" };
            CreateComboBoxColumn("TrigSlope", "TrigSlope", trigSlopeItems, "トリガースロープ(PWR時のみ有効, RISE:立ち上がり, FALL:立ち下り)", out TrigSlope);
            // TrigLevel列作成 parameters[ 11 ]
            DataGridViewTextBoxColumn TrigLevel = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("TrigLevel", "TrigLvl", "トリガーレベル(-40～0dB, PWR時のみ有効)", false, out TrigLevel);
            // TrigDelay列作成 parameters[ 12 ]
            DataGridViewTextBoxColumn TrigDelay = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("TrigDelay", "TrigDelay", "トリガーディレイ(0.000～10.000ms)", false, out TrigDelay);

            // MeasStep列作成 parameters[ 13 ]
            DataGridViewTextBoxColumn MeasStep = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("MeasStep", "MeasStep", "測定ステップ数(2～3000, 1step = 1ms)", false, out MeasStep);

            // Modulation列作成 parameters[ 14 ]
            DataGridViewComboBoxColumn Modulation = new DataGridViewComboBoxColumn();
            string[] modItems = new string[] { "QPSK", "16QAM", "64QAM" };
            CreateComboBoxColumn("Modulation", "MOD", modItems, "変調方式(使用されていない)", out Modulation);

            // BandWidth列作成 parameters[ 15 ]
            DataGridViewComboBoxColumn BandWidth = new DataGridViewComboBoxColumn();
            string[] bwItems = new string[] { "1.4MHZ", "3MHZ", "5MHZ", "10MHZ", "15MHZ", "20MHZ" };
            CreateComboBoxColumn("BandWidth", "BW", bwItems, "Uplinkチャネル帯域幅", out BandWidth);
            // RBNumberOf列作成 parameters[ 16 ]
            DataGridViewTextBoxColumn RBNumberOf = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("RBNumberOf", "NumRB", "Uplinkチャネルのリソースブロック数", false, out RBNumberOf);
            // RBStart列作成 parameters[ 17 ]
            DataGridViewTextBoxColumn RBStart = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("RBStart", "StrtRB", "Uplinkチャネルの開始リソースブロック番号", false, out RBStart);

            // PowerMeasurement列作成 parameters[ 18 ]
            DataGridViewTextBoxColumn PowerMeasurement = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("PowerMeasurement", "POW", "Uplink送信電力測定回数", false, out PowerMeasurement);
            // ModulationAnalysis列作成 parameters[ 19 ]
            DataGridViewTextBoxColumn ModulationAnalysis = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("ModulationAnalysis", "MOD", "Uplink変調解析測定回数", false, out ModulationAnalysis);
            // SpuriousEmissionMask列作成 parameters[ 20 ]
            DataGridViewTextBoxColumn SpuriousEmissionMask = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("SpuriousEmissionMask", "SEM", "スペクトラムエミッションマスク測定回数", false, out SpuriousEmissionMask);
            // OccupiedBandwidth列作成 parameters[ 21 ]
            DataGridViewTextBoxColumn OccupiedBandwidth = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("OccupiedBandwidth", "OBW", "占有帯域幅測定回数", false, out OccupiedBandwidth);
            // AdjacentChannelLeakagePowerRatio列作成 parameters[ 22 ]
            DataGridViewTextBoxColumn AdjacentChannelLeakagePowerRatio = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("AdjacentChannelLeakagePowerRatio", "ACLR", "隣接チャネル漏洩電力比測定回数", false, out AdjacentChannelLeakagePowerRatio);

            // OffsetSegmentDuration列作成 parameters[ 23 ]
            DataGridViewTextBoxColumn OffsetSegmentDuration = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("OffsetSegmentDuration", "Offset", "端末測定タイミング用セグメント開始からDownlink BLER測定開始までのオフセット時間(us)", false, out OffsetSegmentDuration);
            // SegmentDuration列作成 parameters[ 24 ]
            DataGridViewTextBoxColumn SegmentDuration = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("SegmentDuration", "SegDur", "端末測定タイミング用セグメント全体の時間(us)", false, out SegmentDuration);

            // ULPower10dBm列作成 parameters[ 25 ]
            DataGridViewTextBoxColumn ULPower10dBm;
            CreateTextBoxColumn("ULPower10dBm", "ULPow10", "端末Uplink送信電力レベル×10倍(dBm)", false, out ULPower10dBm);
            // MaxTXPowerLimit列作成 parameters[ 26 ]
            DataGridViewTextBoxColumn MaxTXPowerLimit;
            CreateTextBoxColumn("MaxTXPowerLimit", "MaxULPow", "端末最大Uplink送信電力レベル(dBm)", false, out MaxTXPowerLimit);

            // ULLCID列作成 parameters[ 27 ]
            DataGridViewTextBoxColumn ULLCID;
            CreateTextBoxColumn("ULLCID", "ULLCID", "端末Uplink LCID", false, out ULLCID);
            // DLLCID列作成 parameters[ 28 ]
            DataGridViewTextBoxColumn DLLCID;
            CreateTextBoxColumn("DLLCID", "DLLCID", "端末Downlink LCID", false, out DLLCID);
            // TXChain列作成 parameters[ 29 ]
            DataGridViewTextBoxColumn TXChain;
            CreateTextBoxColumn("TXChain", "TXChain", "端末送信アンテナ", false, out TXChain);
            // RXChain列作成 parameters[ 30 ]
            DataGridViewTextBoxColumn RXChain;
            CreateTextBoxColumn("RXChain", "RXChain", "端末受信アンテナ", false, out RXChain);

            // TXPowerControlMode列作成 parameters[ 31 ]
            DataGridViewTextBoxColumn TXPowerControlMode;
            CreateTextBoxColumn("TXPowerControlMode", "PowCtrl", "端末送信パワー制御(0:パワー制御bit有効, 1:無効)", false, out TXPowerControlMode);
            // ConfigOverride列作成 parameters[ 32 ]
            DataGridViewTextBoxColumn ConfigOverride;
            CreateTextBoxColumn("ConfigOverride", "CfgOvrd", "端末Uplink DCI上書(1:有効, 0:無効)", false, out ConfigOverride);
            // ModulationCodingScheme列作成 parameters[ 33 ]
            DataGridViewTextBoxColumn ModulationCodingScheme;
            CreateTextBoxColumn("ModulationCodingScheme", "MCS", "Uplink Modulation Coding Scheme", false, out ModulationCodingScheme);

            // MeasureRxLevel列作成 parameters[ 34 ]
            DataGridViewTextBoxColumn MeasureRxLevel;
            CreateTextBoxColumn("MeasureRxLevel", "MeasRX", "端末Downlink受信電力レベル測定", false, out MeasureRxLevel);
            // MeasureErrorRate列作成 parameters[ 35 ]
            DataGridViewTextBoxColumn MeasureErrorRate;
            CreateTextBoxColumn("MeasureErrorRate", "MeasERR", "端末Downlink受信エラー測定", false, out MeasureErrorRate);
            // TestID列作成 parameters[ 36 ]
            // Get_LteSequenceTest_Results関数で測定結果返すときに、TestIDも渡す
            // 検査プログラムはTestIDに含まれる測定項目を検査する
            //
            // public enum eMeasureType
            // {
            //     NoMeasured = -1,
            //     MaximumOutputPower = 0, //!< Maximum Output Power
            //     MPR,                    //!< Maximum Power Reduction
            //     ConfiguredUETransmittedOutputPower, //!< Configured UE Transmitted Output Power
            //     MinimumOutputPower,     //!< Minimum Output Power
            //     FrequencyError,         //!< Frequency Error
            //     EVM,                    //!< EVM
            //     CarrierLeakage,         //!< Carrier Leakage
            //     InbandEmissions,        //!< In-band Emissions for Non Allocated RB
            //     SpectrumFlatness,       //!< Spectrum Flatness
            //     OBW,                    //!< Occupied Bandwidth
            //     SEM,                    //!< Spectrum Emission Mask
            //     ACLR,                   //!< Adjacent Channel Leakage Power Ratio
            //     ReferenceSensitivityLevel, //!< Reference Sensitivity Level
            //     MaximumInputLevel,      //!< Maximum Input Level
            //     NoTest
            // };
            DataGridViewTextBoxColumn TestID;
            CreateTextBoxColumn("TestID", "TestID", "eMeasureType型参照", false, out TestID);

            // 列追加
            dgvTable = new DataGridView();
            dgvTable.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
            dgvTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTable.Location = new System.Drawing.Point(0, 32);
            dgvTable.Name = "dgvTable" + tblIndex;
            dgvTable.RowTemplate.Height = 21;
            dgvTable.Size = new System.Drawing.Size(198, 67);
            dgvTable.TabIndex = (tblIndex * 100) + 3;
            dgvTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                SegNum, UplinkPort, DownlinkPort, Band, NetworkSignaling,
                DLChannel, DownlinkFrequencyMHz, ULChannel, UplinkFrequencyMHz,
                UplinkLeveldBm, DownlinkPowerdBm, Pattern, CRNTI,
                TrigSource, TrigSlope, TrigLevel, TrigDelay,
                MeasStep, Modulation, BandWidth, RBNumberOf, RBStart,
                PowerMeasurement, ModulationAnalysis,
                SpuriousEmissionMask, OccupiedBandwidth,
                AdjacentChannelLeakagePowerRatio,
                OffsetSegmentDuration, SegmentDuration,
                ULPower10dBm, MaxTXPowerLimit,
                ULLCID, DLLCID, TXChain, RXChain,
                TXPowerControlMode, ConfigOverride, ModulationCodingScheme,
                MeasureRxLevel, MeasureErrorRate, TestID
            });
            dgvTable.SortCompare += new DataGridViewSortCompareEventHandler(this.dgvTable_SortCompare);

            int segLength = GetSegmentLength(seqFile, tblIndex);
            for (int i = 1; i <= segLength; i++)
            {
                string segment = GetPrivateProfileString(seqFile, "Table" + tblIndex, "Segment" + i, "");
                string[] parameters = segment.Split('\t');
                if (segment != "" && parameters.Length >= 37)
                {
                    // SegNum, DownlinkFrequencyMHz, ULChannel, UplinkFrequencyMHzを追加表示
                    string[] segment_rows = new string[37 + 4];
                    segment_rows[0] = i.ToString();     // SegNum
                    segment_rows[1] = parameters[0];    // UplinkPort
                    segment_rows[2] = parameters[1];    // DownlinkPort
                    segment_rows[3] = parameters[2];    // Band
                    segment_rows[4] = parameters[3];    // NetworkSignaling
                    int DLChan = int.Parse(parameters[4]);
                    segment_rows[5] = DLChan.ToString(); // DLChannel
                    double DLFreq = LTEDLChan2FreqMHz(DLChan);
                    segment_rows[6] = DLFreq.ToString(); // DownlinkFrequencyMHz
                    int ULChan = LTEDLChan2ULChan(DLChan);
                    segment_rows[7] = ULChan.ToString(); // ULChannel
                    double ULFreq = LTEULChan2FreqMHz(ULChan);
                    segment_rows[8] = ULFreq.ToString(); // UplinkFrequencyMHz
                    segment_rows[9] = parameters[5];     // UplinkLeveldBm
                    segment_rows[10] = parameters[6];    // DownlinkPowerdBm
                    segment_rows[11] = parameters[7];    // Pattern
                    segment_rows[12] = parameters[8];    // CRNTI
                    segment_rows[13] = parameters[9];    // TrigSource
                    segment_rows[14] = parameters[10];   // TrigSlope
                    segment_rows[15] = parameters[11];   // TrigLevel
                    segment_rows[16] = parameters[12];   // TrigDelay
                    segment_rows[17] = parameters[13];   // MeasStep
                    segment_rows[18] = parameters[14];   // Modulation
                    segment_rows[19] = parameters[15];   // BandWidth
                    segment_rows[20] = parameters[16];   // RBNumberOf
                    segment_rows[21] = parameters[17];   // RBStart
                    segment_rows[22] = parameters[18];   // PowerMeasurement
                    segment_rows[23] = parameters[19];   // ModulationAnalysis
                    segment_rows[24] = parameters[20];   // SpuriousEmissionMask
                    segment_rows[25] = parameters[21];   // OccupiedBandwidth
                    segment_rows[26] = parameters[22];   // AdjacentChannelLeakagePowerRatio
                    segment_rows[27] = parameters[23];   // OffsetSegmentDuration
                    segment_rows[28] = parameters[24];   // SegmentDuration
                    segment_rows[29] = parameters[25];   // ULPower10dBm
                    segment_rows[30] = parameters[26];   // MaxTXPowerLimit
                    segment_rows[31] = parameters[27];   // ULLCID
                    segment_rows[32] = parameters[28];   // DLLCID
                    segment_rows[33] = parameters[29];   // TXChain
                    segment_rows[34] = parameters[30];   // RXChain
                    segment_rows[35] = parameters[31];   // TXPowerControlMode
                    segment_rows[36] = parameters[32];   // ConfigOverride
                    segment_rows[37] = parameters[33];   // ModulationCodingScheme
                    // MeasureRxLevel
                    if(parameters[34] == "" || parameters[34] == "FALSE" || parameters[34] == "0")
                        segment_rows[38] = "0";
                    else
                        segment_rows[38] = "1";
                    // MeasureErrorRate
                    if (parameters[35] == "" || parameters[35] == "FALSE" || parameters[35] == "0")
                        segment_rows[39] = "0";
                    else
                        segment_rows[39] = "1";
                    segment_rows[40] = parameters[36];   // TestID

                    dgvTable.Rows.Add(segment_rows);
                }
            }
        }

        private void LoadLTETable(string seqFile, int tblIndex, TabControl tabTables)
        {
            Label label;
            ComboBox combo;
            CreateLTEPackage(seqFile, tblIndex, out label, out combo);

            DataGridView dataGrid;
            CreateLTEDataGrid(seqFile, tblIndex, out dataGrid);

            TabPage pageTable = new TabPage();
            pageTable.Controls.Add(label);
            pageTable.Controls.Add(combo);
            pageTable.Controls.Add(dataGrid);
            pageTable.Location = new System.Drawing.Point(4, 22);
            pageTable.Name = "pageTable" + tblIndex;
            pageTable.Padding = new System.Windows.Forms.Padding(3);
            pageTable.Size = new System.Drawing.Size(784, 264);
            pageTable.TabIndex = tblIndex * 100;
            pageTable.Text = "Table" + tblIndex;
            pageTable.UseVisualStyleBackColor = true;

            tabSeqTables.Controls.Add(pageTable);
        }
        #endregion

        #region WCDMA
        #region WCDMAChan
        private struct WCDMAChanInfo
        {
            public int Band;

            public struct Channel
            {
                public double FreqOff;
                public int ChanOff;
                public double AddFreqOff;

                public struct Range
                {
                    public int Lower;
                    public int Upper;

                    public Range(int lower, int upper)
                    {
                        Lower = lower;
                        Upper = upper;
                    }
                };
                public Range ChanRange;

                public Channel(double freqOff, int chanOff, double addFreqOff, int lower, int upper)
                {
                    FreqOff = freqOff;
                    ChanOff = chanOff;
                    AddFreqOff = addFreqOff;
                    ChanRange = new Range(lower, upper);
                }
            };
            public Channel Uplink;
            public Channel Downlink;

            public bool IsAdditional;

            public WCDMAChanInfo(int band,
                double up_freqOff, int up_chanOff, double up_addFreqOff, int up_lower, int up_upper,
                double down_freqOff, int down_chanOff, double down_addFreqOff, int down_lower, int down_upper,
                bool isAdditional
                )
            {
                Band = band;
                Uplink = new Channel(up_freqOff, up_chanOff, up_addFreqOff, up_lower, up_upper);
                Downlink = new Channel(down_freqOff, down_chanOff, down_addFreqOff, down_lower, down_upper);
                IsAdditional = isAdditional;
            }
        };

        private WCDMAChanInfo[] WCDMAInfo = new WCDMAChanInfo[] {
            new WCDMAChanInfo( 1,    0.0, 950,    0.0, 9612, 9888,    0.0, 950,    0.0, 10562, 10838, false),
            new WCDMAChanInfo( 2,    0.0, 400, 1850.1, 9262, 9538,    0.0, 400, 1850.1,  9662,  9938, true),
            new WCDMAChanInfo( 3, 1525.0, 225,    0.0,  937, 1288, 1575.0, 225,    0.0,  1162,  1513, false),
            new WCDMAChanInfo( 4, 1450.0, 225, 1380.1, 1312, 1513, 1805.0, 225, 1735.1,  1537,  1738, true),
            new WCDMAChanInfo( 5,    0.0, 225,  670.1, 4132, 4233,    0.0, 225,  670.1,  4357,  4458, true),
            new WCDMAChanInfo( 7, 2100.0, 225, 2030.1, 2012, 2338, 2175.0, 225, 2105.1,  2237,  2563, true),
            new WCDMAChanInfo( 8,  340.0, 225,    0.0, 2712, 2863,  340.0, 225,    0.0,  2937,  3088, false),
            new WCDMAChanInfo( 9,    0.0, 475,    0.0, 8762, 8912,    0.0, 475,    0.0,  9237,  9387, false),
            new WCDMAChanInfo(10, 1135.0, 225, 1075.1, 2887, 3613, 1490.0, 225, 1430.1,  3112,  3388, true),
            new WCDMAChanInfo(11,  733.0, 225,    0.0, 3487, 3587,  736.0, 225,    0.0,  3712,  3812, false),
            new WCDMAChanInfo(12,  -22.0, 225,  -39.9, 3612, 3678,  -37.0, 225,  -54.9,  3837,  3903, true),
            new WCDMAChanInfo(13,   21.0, 225,   11.0, 3792, 3818,  -55.0, 225,  -64.9,  4017,  4043, true),
            new WCDMAChanInfo(14,   12.0, 225,    2.1, 3892, 3918,  -63.0, 225,  -72.9,  4117,  4143, true),
            new WCDMAChanInfo(19,  770.0, 400,  775.1,  312,  363,  735.0, 400,  720.1,   712,   763, true),
            new WCDMAChanInfo(20,  -23.0, 225,    0.0, 4287, 4413, -109.0, 225,    0.0,  4512,  4683, false),
            new WCDMAChanInfo(21, 1358.0, 400,    0.0,  462,  512, 1326.0, 400,    0.0,   862,   912, false),
            new WCDMAChanInfo( 0,      0,   0,    0.0,    0,    0,      0,   0,    0.0,     0,     0, false)
        };
        private int[,] AdditionalBand2Channel = new int[,] {
            { 12,  37,  62,  87, 112, 137, 162, 187, 212, 237, 262, 287},
            {412, 437, 462, 487, 512, 537, 562, 587, 612, 637, 662, 687}
        };

        private int[,] AdditionalBand4Channel = new int[,] {
            {1662, 1687, 1712, 1737, 1762, 1787, 1812, 1837, 1862},
            {1887, 1912, 1937, 1962, 1987, 2012, 2037, 2062, 2087}
        };

        private int[,] AdditionalBand5Channel = new int[,] {
            { 782,  787,  807,  812,  837,  862},
            {1007, 1012, 1032, 1037, 1087, 2012}
        };

        private int[,] AdditionalBand7Channel = new int[,] {
            {2362, 2387, 2412, 2437, 2462, 2487, 2512, 2537, 2562, 2587, 2612, 2637, 2662, 2687},
            {2587, 2612, 2637, 2662, 2687, 2712, 2737, 2762, 2787, 2812, 2837, 2862, 2887, 2912}
        };

        private int[,] AdditionalBand10Channel = new int[,] {
            {3187, 3212, 3237, 3262, 3287, 3312, 3337, 3362, 3387, 3412, 3437, 3462},
            {3412, 3437, 3462, 3487, 3512, 3537, 3562, 3587, 3612, 3637, 3662, 3687}
        };

        private int[,] AdditionalBand12Channel = new int[,] {
            {3702, 3707, 3732, 3737, 3762, 3767},
            {3927, 3932, 3957, 3962, 3987, 3992}
        };

        private int[,] AdditionalBand13Channel = new int[,] {
            {3842, 3867},
            {4067, 4092}
        };

        private int[,] AdditionalBand14Channel = new int[,] {
            {3942, 3967},
            {4167, 4192}
        };

        private int[,] AdditionalBand19Channel = new int[,] {
            {387, 412, 437},
            {787, 812, 837}
        };

        int WCDMADLChan2BC(int DLChannel)
        {
            int ret = -1;

            for (int index = 0; WCDMAInfo[index].Band > 0; index++)
            {
                if (WCDMAInfo[index].IsAdditional)
                {
                    // Additionalを検索
                    //if (WCDMAMatchAdditionalChannel(WCDMAInfo[index].Band, DLChannel, Downlink))
                    {
                        ret = index;
                        break;
                    }
                }

                if ((WCDMAInfo[index].Downlink.ChanRange.Lower <= DLChannel) && (WCDMAInfo[index].Downlink.ChanRange.Upper >= DLChannel))
                {
                    ret = index;
                    break;
                }
            }

            return ret;
        }

        private int WCDMADLChan2ULChan(int DLChannel)
        {
            int offSet = 0;
            int index = WCDMADLChan2BC(DLChannel);

            if (index != -1)
            {
                offSet = WCDMAInfo[index].Downlink.ChanOff;
            }

            return DLChannel - offSet;
        }

#if false
        private int WCDMAULChan2BC(int ULChannel)
        {
            int ret = -1;
            for (int index = 0; LTEInfo[index].Band > 0; index++)
            {
                if ((LTEInfo[index].Uplink.ChanRange.Lower <= ULChannel) &&
                (LTEInfo[index].Uplink.ChanRange.Upper >= ULChannel))
                {
                    ret = index;
                    break;
                }
            }
            return ret;
        }

        private double WCDMADLChan2FreqMHz(int DLChannel)
        {
            double ret = -1;
            int index = WCDMAULChan2BC(LTEDLChan2ULChan(DLChannel));
            if (index != -1)
            {
                ret = LTEInfo[index].Downlink.FreqLowMHz + 0.1 * (DLChannel - LTEInfo[index].Downlink.ChanOff);
            }
            return ret;
        }
        private double WCDMAChan2FreqMHz(int ULChannel)
        {
            double ret = -1;
            int index = LTEULChan2BC(ULChannel);
            if (index != -1)
            {
                ret = LTEInfo[index].Uplink.FreqLowMHz + 0.1 * (ULChannel - LTEInfo[index].Uplink.ChanOff);
            }
            return ret;
        }
#endif
        #endregion

        private void CreateWCDMAPackage(string seqFile, int tblIndex, out Label lblPackage, out ComboBox cmbPackage)
        {
            lblPackage = new Label();
            lblPackage.AutoSize = true;
            lblPackage.Location = new System.Drawing.Point(6, 9);
            lblPackage.Name = "lblPackage" + tblIndex;
            lblPackage.Size = new System.Drawing.Size(48, 12);
            lblPackage.TabIndex = (tblIndex * 100) + 1;
            lblPackage.Text = "Package";

            cmbPackage = new ComboBox();
            cmbPackage.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPackage.FormattingEnabled = true;
            cmbPackage.Items.AddRange(new object[] {
                "MV887011A_WCDMA_0002", "MV887011A_WCDMA_0003", "MV887011A_WCDMA_0004",
                "MV887011A_WCDMA_0005", "MV887011A_WCDMA_0006", "MV887011A_WCDMA_0008",
                "MV887011A_WCDMA_0009", "MV887011A_WCDMA_0010", "MV887011A_WCDMA_0013"
            });
            cmbPackage.Location = new System.Drawing.Point(102, 6);
            cmbPackage.Name = "cmbPackage" + tblIndex;
            cmbPackage.Size = new System.Drawing.Size(218, 20);
            cmbPackage.TabIndex = (tblIndex * 100) + 2;

            cmbPackage.Text = GetPrivateProfileString(seqFile, "Table" + tblIndex, "Package", "");
        }

        private void CreateWCDMADataGrid(string seqFile, int tblIndex, out DataGridView dgvTable)
        {
            // SegNum列作成
            DataGridViewTextBoxColumn SegNum;
            CreateTextBoxColumn("SegNum", "Seg#", "セグメント番号", true, out SegNum);

            // UplinkPort列作成 parameters[ 0 ]
            DataGridViewTextBoxColumn UplinkPort = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("UplinkPort", "ULPort", "Uplinkテスターポート番号(無効)", true, out UplinkPort);
            // DownlinkPort列作成 parameters[ 1 ]
            DataGridViewTextBoxColumn DownlinkPort = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("DownlinkPort", "DLPort", "Downlinkテスターポート番号(無効)", true, out DownlinkPort);

            // BandClass列作成 parameters[ 2 ]
            DataGridViewTextBoxColumn Band = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("Band", "BC", "バンドクラス(2,5)", false, out Band);

            // DLChannel列作成 parameters[ 3 ]
            DataGridViewTextBoxColumn DLChannel = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("DLChannel", "DLChan", "Downlinkチャネル番号", false, out DLChannel);
            DataGridViewTextBoxColumn DownlinkFrequencyMHz = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("DownlinkFrequencyMHz", "DLFreq", "Downlink周波数(DLChanから計算)", true, out DownlinkFrequencyMHz);
            DataGridViewTextBoxColumn ULChannel = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("ULChannel", "ULChan", "Upwnlinkチャネル番号(DLChanから計算)", true, out ULChannel);
            DataGridViewTextBoxColumn UplinkFrequencyMHz = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("UplinkFrequencyMHz", "ULFreq", "Uplink周波数(DLChanから計算)", true, out UplinkFrequencyMHz);

            // UplinkLeveldBm列作成 parameters[ 4 ]
            DataGridViewTextBoxColumn UplinkLeveldBm = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("UplinkLeveldBm", "ULPow", "テスターUplink入力レベル(dBm)", false, out UplinkLeveldBm);
            // DownlinkPowerdBm列作成 parameters[ 5 ]
            DataGridViewTextBoxColumn DownlinkPowerdBm = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("DownlinkPowerdBm", "DLPow", "テスターDownlink出力レベル(dBm)", false, out DownlinkPowerdBm);
            // Pattern列作成 parameters[ 6 ]
            DataGridViewComboBoxColumn Pattern = new DataGridViewComboBoxColumn();
            string[] patternItems = new string[] {
                "PAT1",  "PAT2",  "PAT3",  "PAT4",  "PAT5",  "PAT6",  "PAT7",  "PAT8",  "PAT9",  "PAT10",
                "PAT11", "PAT12", "PAT13", "PAT14", "PAT15", "PAT16", "PAT17", "PAT18", "PAT19", "PAT20",
                   "CW",   "OFF", "NC"
            };
            CreateComboBoxColumn("Pattern", "DLPAT", patternItems, "テスターDownlink波形パターン", out Pattern);

            // ScramblingCode列作成 parameters[ 7 ]
            DataGridViewTextBoxColumn ScramblingCode = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("ScramblingCode", "SC", "スクランブルコード", false, out ScramblingCode);

            // TrigSource列作成 parameters[ 8 ]
            DataGridViewComboBoxColumn TrigSource = new DataGridViewComboBoxColumn();
            string[] trigSrcItems = new string[] { "FREERUN", "PWR", "FRAME" };
            CreateComboBoxColumn("TrigSource", "TrigSrc", trigSrcItems, "トリガーソース(FREERUN:トリガーなし, PWR:入力パワー, FRAME:フレーム波形)", out TrigSource);
            // TrigSlope列作成 parameters[ 9 ]
            DataGridViewComboBoxColumn TrigSlope = new DataGridViewComboBoxColumn();
            string[] trigSlopeItems = new string[] { "RISE", "FALL" };
            CreateComboBoxColumn("TrigSlope", "TrigSlope", trigSlopeItems, "トリガースロープ(PWR時のみ有効, RISE:立ち上がり, FALL:立ち下り)", out TrigSlope);
            // TrigLevel列作成 parameters[ 10 ]
            DataGridViewTextBoxColumn TrigLevel = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("TrigLevel", "TrigLvl", "トリガーレベル(-40～0dB, PWR時のみ有効)", false, out TrigLevel);
            // TrigDelay列作成 parameters[ 11 ]
            DataGridViewTextBoxColumn TrigDelay = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("TrigDelay", "TrigDelay", "トリガーディレイ(0.000～10.000ms)", false, out TrigDelay);

            // MeasStep列作成 parameters[ 12 ]
            DataGridViewTextBoxColumn MeasStep = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("MeasStep", "MeasStep", "測定ステップ数(2～3000, 1step = 0.67ms)", false, out MeasStep);

            // PowerMeasurement列作成 parameters[ 13 ]
            DataGridViewTextBoxColumn PowerMeasurement = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("PowerMeasurement", "POW", "Uplink送信電力測定回数", false, out PowerMeasurement);
            // ModulationAnalysis列作成 parameters[ 14 ]
            DataGridViewTextBoxColumn ModulationAnalysis = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("ModulationAnalysis", "MOD", "Uplink変調解析測定回数", false, out ModulationAnalysis);
            // SpuriousEmissionMask列作成 parameters[ 15 ]
            DataGridViewTextBoxColumn SpuriousEmissionMask = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("SpuriousEmissionMask", "SEM", "スペクトラムエミッションマスク測定回数", false, out SpuriousEmissionMask);
            // OccupiedBandwidth列作成 parameters[ 16 ]
            DataGridViewTextBoxColumn OccupiedBandwidth = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("OccupiedBandwidth", "OBW", "占有帯域幅測定回数", false, out OccupiedBandwidth);
            // AdjacentChannelLeakagePowerRatio列作成 parameters[ 17 ]
            DataGridViewTextBoxColumn AdjacentChannelLeakagePowerRatio = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("AdjacentChannelLeakagePowerRatio", "ACLR", "隣接チャネル漏洩電力比測定回数", false, out AdjacentChannelLeakagePowerRatio);
            // Inner Loop Power Control列作成 parameters[ 18 ]
            DataGridViewTextBoxColumn InnerLoopPowerControl = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("InnerLoopPowerControl", "ILPC", "内部ループパワー制御測定回数", false, out InnerLoopPowerControl);

            // OffsetSegmentDuration列作成 parameters[ 19 ]
            DataGridViewTextBoxColumn OffsetSegmentDuration = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("OffsetSegmentDuration", "Offset", "端末測定タイミング用セグメント開始からDownlink BLER測定開始までのオフセット時間(us)", false, out OffsetSegmentDuration);
            // SegmentDuration列作成 parameters[ 20 ]
            DataGridViewTextBoxColumn SegmentDuration = new DataGridViewTextBoxColumn();
            CreateTextBoxColumn("SegmentDuration", "SegDur", "端末測定タイミング用セグメント全体の時間(us)", false, out SegmentDuration);

            // ULPower10dBm列作成 parameters[ 21 ]
            DataGridViewTextBoxColumn ULPower10dBm;
            CreateTextBoxColumn("ULPower10dBm", "ULPow10", "端末Uplink送信電力レベル×10倍(dBm)", false, out ULPower10dBm);

            // TXPowerControlMode列作成 parameters[ 22 ]
            DataGridViewTextBoxColumn TXPowerControlMode;
            CreateTextBoxColumn("TXPowerControlMode", "PowCtrl", "端末送信パワー制御(0:パワー制御bit有効, 1:無効)", false, out TXPowerControlMode);

            // MeasureRxLevel列作成 parameters[ 23 ]
            DataGridViewTextBoxColumn MeasureRxLevel;
            CreateTextBoxColumn("MeasureRxLevel", "MeasRX", "端末Downlink受信電力レベル測定", false, out MeasureRxLevel);
            // MeasureErrorRate列作成 parameters[ 24 ]
            DataGridViewTextBoxColumn MeasureErrorRate;
            CreateTextBoxColumn("MeasureErrorRate", "MeasERR", "端末Downlink受信エラー測定", false, out MeasureErrorRate);
            // TestID列作成 parameters[ 25 ]
            // Get_LteSequenceTest_Results関数で測定結果返すときに、TestIDも渡す
            // 検査プログラムはTestIDに含まれる測定項目を検査する
            //
            // public enum eWcdmaMeasureType
            // {
            //     WcdmaNoMeasured = -1,
            //     WcdmaMaximumOutputPower = 0,	//!< Maximum Output Power
            //     WcdmaFrequencyError,			//!< Frequency Error
            //     WcdmaMinimumOutputPower,		//!< Minimum Output Power
            //     WcdmaOBW,						//!< Occupied Bandwidth
            //     WcdmaSEM,						//!< Spectrum Emission Mask
            //     WcdmaACLR,						//!< Adjacent Channel Leakage Power Ratio
            //     WcdmaEVM,						//!< EVM
            //     WcdmaReferenceSensitivityLevel,	//!< Reference Sensitivity Level
            //     WcdmaMaximumInputLevel,			//!< Maximum Input Level
            //     WcdmaILPC,						//!< Inner Loop Power Control in the Uplink
            //     WcdmaPCDE,						//!< Peak Code Domain Error
            //     WcdmaRCDE,						//!< Rerative Code Domain Error
            //     WcdmaPhaseDiscontinuity,		//!< Phase Discontinuity
            //     WcdmaNoTest
            // };
            DataGridViewTextBoxColumn TestID;
            CreateTextBoxColumn("TestID", "TestID", "eWcdmaMeasureType型参照", false, out TestID);

            // 列追加
            dgvTable = new DataGridView();
            dgvTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTable.Location = new System.Drawing.Point(6, 32);
            dgvTable.Name = "dgvTable" + tblIndex;
            dgvTable.RowTemplate.Height = 21;
            dgvTable.Size = new System.Drawing.Size(772, 226);
            dgvTable.TabIndex = (tblIndex * 100) + 3;
            dgvTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                SegNum, UplinkPort, DownlinkPort, Band,
                DLChannel, DownlinkFrequencyMHz, ULChannel, UplinkFrequencyMHz,
                UplinkLeveldBm, DownlinkPowerdBm, Pattern, ScramblingCode,
                TrigSource, TrigSlope, TrigLevel, TrigDelay,
                MeasStep, PowerMeasurement, ModulationAnalysis,
                SpuriousEmissionMask, OccupiedBandwidth,
                AdjacentChannelLeakagePowerRatio, InnerLoopPowerControl,
                OffsetSegmentDuration, SegmentDuration,
                ULPower10dBm, TXPowerControlMode,
                MeasureRxLevel, MeasureErrorRate, TestID
            });
            dgvTable.SortCompare += new DataGridViewSortCompareEventHandler(this.dgvTable_SortCompare);

            int segLength = GetSegmentLength(seqFile, tblIndex);
            for (int i = 1; i <= segLength; i++)
            {
                string segment = GetPrivateProfileString(seqFile, "Table" + tblIndex, "Segment" + i, "");
                string[] parameters = segment.Split('\t');
                if (segment != "" && parameters.Length >= 26)
                {
                    // SegNum, DownlinkFrequencyMHz, ULChannel, UplinkFrequencyMHzを追加表示
                    string[] segment_rows = new string[26 + 4];
                    segment_rows[0] = i.ToString();      // SegNum
                    segment_rows[1] = parameters[0];     // UplinkPort
                    segment_rows[2] = parameters[1];     // DownlinkPort
                    segment_rows[3] = parameters[2];     // Band
                    int DLChan = int.Parse(parameters[3]);
                    segment_rows[4] = DLChan.ToString(); // DLChannel
                    double DLFreq = LTEDLChan2FreqMHz(DLChan);
                    segment_rows[5] = ""; // DLFreq.ToString(); // DownlinkFrequencyMHz
                    int ULChan = WCDMADLChan2ULChan(DLChan);
                    segment_rows[6] = ""; // ULChan.ToString(); // ULChannel
                    double ULFreq = LTEULChan2FreqMHz(ULChan);
                    segment_rows[7] = ""; // ULFreq.ToString(); // UplinkFrequencyMHz
                    segment_rows[8] = parameters[4];     // UplinkLeveldBm
                    segment_rows[9] = parameters[5];     // DownlinkPowerdBm
                    segment_rows[10] = parameters[6];    // Pattern
                    segment_rows[11] = parameters[7];    // ScramblingCode
                    segment_rows[12] = parameters[8];    // TrigSource
                    segment_rows[13] = parameters[9];    // TrigSlope
                    segment_rows[14] = parameters[10];   // TrigLevel
                    segment_rows[15] = parameters[11];   // TrigDelay
                    segment_rows[16] = parameters[12];   // MeasStep
                    segment_rows[17] = parameters[13];   // PowerMeasurement
                    segment_rows[18] = parameters[14];   // ModulationAnalysis
                    segment_rows[19] = parameters[15];   // SpuriousEmissionMask
                    segment_rows[20] = parameters[16];   // OccupiedBandwidth
                    segment_rows[21] = parameters[17];   // AdjacentChannelLeakagePowerRatio
                    segment_rows[22] = parameters[18];   // InnerLoopPowerControl
                    segment_rows[23] = parameters[19];   // OffsetSegmentDuration
                    segment_rows[24] = parameters[20];   // SegmentDuration
                    segment_rows[25] = parameters[21];   // ULPower10dBm
                    segment_rows[26] = parameters[22];   // TXPowerControlMode
                    segment_rows[27] = parameters[23];   // MeasureRxLevel
                    segment_rows[28] = parameters[24];   // MeasureErrorRate
                    segment_rows[29] = parameters[25];   // TestID

                    dgvTable.Rows.Add(segment_rows);
                }
            }
        }

        private void LoadWCDMATable(string seqFile, int tblIndex, TabControl tabTables)
        {
            Label label;
            ComboBox combo;
            CreateWCDMAPackage(seqFile, tblIndex, out label, out combo);

            DataGridView dataGrid;
            CreateWCDMADataGrid(seqFile, tblIndex, out dataGrid);

            TabPage pageTable = new TabPage();
            pageTable.Controls.Add(label);
            pageTable.Controls.Add(combo);
            pageTable.Controls.Add(dataGrid);
            pageTable.Location = new System.Drawing.Point(4, 22);
            pageTable.Name = "pageTable" + tblIndex;
            pageTable.Padding = new System.Windows.Forms.Padding(3);
            pageTable.Size = new System.Drawing.Size(784, 264);
            pageTable.TabIndex = tblIndex * 100;
            pageTable.Text = "Table" + tblIndex;
            pageTable.UseVisualStyleBackColor = true;

            tabSeqTables.Controls.Add(pageTable);
        }
        #endregion

        private void LoadGSMTable(string seqFile, int tblIndex, TabControl tabTables)
        {
        }
    }
}
