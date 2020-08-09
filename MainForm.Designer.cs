namespace LargeTextEditor
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "ファイル先頭",
            "1"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "ファイル末尾",
            "-1"}, -1);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReload = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuCut = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuFind = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFindNext = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFindPrev = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSelectLine = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSearchStop = new System.Windows.Forms.Button();
            this.btnSearchPrev = new System.Windows.Forms.Button();
            this.btnSearchNext = new System.Windows.Forms.Button();
            this.chkSearchWord = new System.Windows.Forms.CheckBox();
            this.chkSearchUpperLower = new System.Windows.Forms.CheckBox();
            this.chkSearchRegex = new System.Windows.Forms.CheckBox();
            this.txtSearchBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtMain = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.TabPageProperty = new System.Windows.Forms.TabPage();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.TabPageBookmark = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnBookmarkAdd = new System.Windows.Forms.Button();
            this.btnBookmarkDelete = new System.Windows.Forms.Button();
            this.listViewBookmark = new System.Windows.Forms.ListView();
            this.listViewBookmarkName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewBookmarkByteIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.TabPageProperty.SuspendLayout();
            this.TabPageBookmark.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuEdit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1272, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNew,
            this.menuOpen,
            this.menuReload,
            this.toolStripMenuItem1,
            this.menuExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.ShortcutKeyDisplayString = "";
            this.menuFile.Size = new System.Drawing.Size(82, 24);
            this.menuFile.Text = "ファイル(&F)";
            this.menuFile.Click += new System.EventHandler(this.menuFile_Click);
            // 
            // menuNew
            // 
            this.menuNew.Name = "menuNew";
            this.menuNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.menuNew.Size = new System.Drawing.Size(233, 26);
            this.menuNew.Text = "新規(&N)";
            this.menuNew.Click += new System.EventHandler(this.menuNew_Click);
            // 
            // menuOpen
            // 
            this.menuOpen.Name = "menuOpen";
            this.menuOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuOpen.Size = new System.Drawing.Size(233, 26);
            this.menuOpen.Text = "開く(&O)...";
            this.menuOpen.Click += new System.EventHandler(this.menuOpen_Click);
            // 
            // menuReload
            // 
            this.menuReload.Name = "menuReload";
            this.menuReload.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.menuReload.Size = new System.Drawing.Size(233, 26);
            this.menuReload.Text = "再読み込み(&R)";
            this.menuReload.Click += new System.EventHandler(this.menuReload_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(230, 6);
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new System.Drawing.Size(233, 26);
            this.menuExit.Text = "終了(&X)";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // menuEdit
            // 
            this.menuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuUndo,
            this.toolStripMenuItem2,
            this.menuCut,
            this.menuCopy,
            this.menuPaste,
            this.menuDelete,
            this.toolStripMenuItem3,
            this.menuFind,
            this.menuFindNext,
            this.menuFindPrev,
            this.menuSelectLine});
            this.menuEdit.Name = "menuEdit";
            this.menuEdit.Size = new System.Drawing.Size(71, 24);
            this.menuEdit.Text = "編集(&E)";
            this.menuEdit.Click += new System.EventHandler(this.menuEdit_Click);
            // 
            // menuUndo
            // 
            this.menuUndo.Name = "menuUndo";
            this.menuUndo.Size = new System.Drawing.Size(231, 26);
            this.menuUndo.Text = "元に戻す(&U)";
            this.menuUndo.Click += new System.EventHandler(this.menuUndo_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(228, 6);
            // 
            // menuCut
            // 
            this.menuCut.Name = "menuCut";
            this.menuCut.Size = new System.Drawing.Size(231, 26);
            this.menuCut.Text = "切り取り(&T)";
            this.menuCut.Click += new System.EventHandler(this.menuCut_Click);
            // 
            // menuCopy
            // 
            this.menuCopy.Name = "menuCopy";
            this.menuCopy.Size = new System.Drawing.Size(231, 26);
            this.menuCopy.Text = "コピー(&C)";
            this.menuCopy.Click += new System.EventHandler(this.menuCopy_Click);
            // 
            // menuPaste
            // 
            this.menuPaste.Name = "menuPaste";
            this.menuPaste.Size = new System.Drawing.Size(231, 26);
            this.menuPaste.Text = "貼り付け(&P)";
            this.menuPaste.Click += new System.EventHandler(this.menuPaste_Click);
            // 
            // menuDelete
            // 
            this.menuDelete.Name = "menuDelete";
            this.menuDelete.Size = new System.Drawing.Size(231, 26);
            this.menuDelete.Text = "削除(&L)";
            this.menuDelete.Click += new System.EventHandler(this.menuDelete_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(228, 6);
            // 
            // menuFind
            // 
            this.menuFind.Name = "menuFind";
            this.menuFind.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.menuFind.Size = new System.Drawing.Size(231, 26);
            this.menuFind.Text = "検索(&F)...";
            this.menuFind.Click += new System.EventHandler(this.menuFind_Click);
            // 
            // menuFindNext
            // 
            this.menuFindNext.Name = "menuFindNext";
            this.menuFindNext.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.menuFindNext.Size = new System.Drawing.Size(231, 26);
            this.menuFindNext.Text = "次を検索(&N)";
            this.menuFindNext.Click += new System.EventHandler(this.menuFindNext_Click);
            // 
            // menuFindPrev
            // 
            this.menuFindPrev.Name = "menuFindPrev";
            this.menuFindPrev.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F3)));
            this.menuFindPrev.Size = new System.Drawing.Size(231, 26);
            this.menuFindPrev.Text = "前を検索(&V)";
            this.menuFindPrev.Click += new System.EventHandler(this.menuFindPrev_Click);
            // 
            // menuSelectLine
            // 
            this.menuSelectLine.Name = "menuSelectLine";
            this.menuSelectLine.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.menuSelectLine.Size = new System.Drawing.Size(231, 26);
            this.menuSelectLine.Text = "行を選択(&A)";
            this.menuSelectLine.Click += new System.EventHandler(this.menuSelectLine_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 699);
            this.statusStrip1.MinimumSize = new System.Drawing.Size(0, 26);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1272, 26);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 20);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "全て(*.*)|*.*|テキストファイル(*.txt)|*.txt";
            this.openFileDialog1.ReadOnlyChecked = true;
            this.openFileDialog1.ShowReadOnly = true;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "テキストファイル(*.txt)|*.txt|全て(*.*)|*.*";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSearchStop);
            this.panel2.Controls.Add(this.btnSearchPrev);
            this.panel2.Controls.Add(this.btnSearchNext);
            this.panel2.Controls.Add(this.chkSearchWord);
            this.panel2.Controls.Add(this.chkSearchUpperLower);
            this.panel2.Controls.Add(this.chkSearchRegex);
            this.panel2.Controls.Add(this.txtSearchBox);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 636);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1272, 63);
            this.panel2.TabIndex = 3;
            // 
            // btnSearchStop
            // 
            this.btnSearchStop.Enabled = false;
            this.btnSearchStop.Location = new System.Drawing.Point(723, 8);
            this.btnSearchStop.Name = "btnSearchStop";
            this.btnSearchStop.Size = new System.Drawing.Size(75, 23);
            this.btnSearchStop.TabIndex = 13;
            this.btnSearchStop.Text = "検索中止";
            this.btnSearchStop.UseVisualStyleBackColor = true;
            // 
            // btnSearchPrev
            // 
            this.btnSearchPrev.Location = new System.Drawing.Point(642, 8);
            this.btnSearchPrev.Name = "btnSearchPrev";
            this.btnSearchPrev.Size = new System.Drawing.Size(75, 23);
            this.btnSearchPrev.TabIndex = 3;
            this.btnSearchPrev.Text = "前を検索";
            this.btnSearchPrev.UseVisualStyleBackColor = true;
            this.btnSearchPrev.Click += new System.EventHandler(this.btnSearchPrev_Click);
            // 
            // btnSearchNext
            // 
            this.btnSearchNext.Location = new System.Drawing.Point(561, 8);
            this.btnSearchNext.Name = "btnSearchNext";
            this.btnSearchNext.Size = new System.Drawing.Size(75, 23);
            this.btnSearchNext.TabIndex = 2;
            this.btnSearchNext.Text = "次を検索";
            this.btnSearchNext.UseVisualStyleBackColor = true;
            this.btnSearchNext.Click += new System.EventHandler(this.btnSearchNext_Click);
            // 
            // chkSearchWord
            // 
            this.chkSearchWord.AutoSize = true;
            this.chkSearchWord.Location = new System.Drawing.Point(316, 37);
            this.chkSearchWord.Name = "chkSearchWord";
            this.chkSearchWord.Size = new System.Drawing.Size(89, 19);
            this.chkSearchWord.TabIndex = 7;
            this.chkSearchWord.Text = "単語単位";
            this.chkSearchWord.UseVisualStyleBackColor = true;
            // 
            // chkSearchUpperLower
            // 
            this.chkSearchUpperLower.AutoSize = true;
            this.chkSearchUpperLower.Location = new System.Drawing.Point(150, 37);
            this.chkSearchUpperLower.Name = "chkSearchUpperLower";
            this.chkSearchUpperLower.Size = new System.Drawing.Size(160, 19);
            this.chkSearchUpperLower.TabIndex = 6;
            this.chkSearchUpperLower.Text = "大文字小文字を区別";
            this.chkSearchUpperLower.UseVisualStyleBackColor = true;
            // 
            // chkSearchRegex
            // 
            this.chkSearchRegex.AutoSize = true;
            this.chkSearchRegex.Checked = true;
            this.chkSearchRegex.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSearchRegex.Location = new System.Drawing.Point(55, 37);
            this.chkSearchRegex.Name = "chkSearchRegex";
            this.chkSearchRegex.Size = new System.Drawing.Size(89, 19);
            this.chkSearchRegex.TabIndex = 5;
            this.chkSearchRegex.Text = "正規表現";
            this.chkSearchRegex.UseVisualStyleBackColor = true;
            // 
            // txtSearchBox
            // 
            this.txtSearchBox.Location = new System.Drawing.Point(55, 9);
            this.txtSearchBox.Name = "txtSearchBox";
            this.txtSearchBox.Size = new System.Drawing.Size(500, 22);
            this.txtSearchBox.TabIndex = 1;
            this.txtSearchBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchBox_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "検索";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.splitContainer1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 28);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1272, 608);
            this.panel3.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1272, 608);
            this.splitContainer1.SplitterDistance = 962;
            this.splitContainer1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtMain);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(962, 608);
            this.panel1.TabIndex = 5;
            // 
            // txtMain
            // 
            this.txtMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMain.Font = new System.Drawing.Font("ＭＳ ゴシック", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtMain.HideSelection = false;
            this.txtMain.Location = new System.Drawing.Point(0, 0);
            this.txtMain.Multiline = true;
            this.txtMain.Name = "txtMain";
            this.txtMain.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMain.Size = new System.Drawing.Size(941, 608);
            this.txtMain.TabIndex = 0;
            this.txtMain.WordWrap = false;
            this.txtMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMain_KeyDown);
            this.txtMain.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtMain_KeyUp);
            this.txtMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtMain_MouseDown);
            this.txtMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.txtMain_MouseUp);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.vScrollBar1);
            this.panel4.Controls.Add(this.btnUp);
            this.panel4.Controls.Add(this.btnDown);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(941, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(21, 608);
            this.panel4.TabIndex = 1;
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar1.Location = new System.Drawing.Point(0, 0);
            this.vScrollBar1.Maximum = 109;
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(21, 562);
            this.vScrollBar1.TabIndex = 0;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // btnUp
            // 
            this.btnUp.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnUp.Location = new System.Drawing.Point(0, 562);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(21, 23);
            this.btnUp.TabIndex = 0;
            this.btnUp.Text = "⇧";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            this.btnUp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnUp_MouseClick);
            this.btnUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnUp_MouseDown);
            this.btnUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnUp_MouseUp);
            // 
            // btnDown
            // 
            this.btnDown.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnDown.Location = new System.Drawing.Point(0, 585);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(21, 23);
            this.btnDown.TabIndex = 0;
            this.btnDown.Text = "⇩";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            this.btnDown.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnDown_MouseClick);
            this.btnDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnDown_MouseDown);
            this.btnDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnDown_MouseUp);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.TabPageProperty);
            this.tabControl1.Controls.Add(this.TabPageBookmark);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(306, 608);
            this.tabControl1.TabIndex = 0;
            // 
            // TabPageProperty
            // 
            this.TabPageProperty.Controls.Add(this.propertyGrid1);
            this.TabPageProperty.Location = new System.Drawing.Point(4, 25);
            this.TabPageProperty.Name = "TabPageProperty";
            this.TabPageProperty.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageProperty.Size = new System.Drawing.Size(298, 579);
            this.TabPageProperty.TabIndex = 0;
            this.TabPageProperty.Text = "プロパティ";
            this.TabPageProperty.UseVisualStyleBackColor = true;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(3, 3);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(292, 573);
            this.propertyGrid1.TabIndex = 0;
            // 
            // TabPageBookmark
            // 
            this.TabPageBookmark.Controls.Add(this.flowLayoutPanel1);
            this.TabPageBookmark.Controls.Add(this.listViewBookmark);
            this.TabPageBookmark.Location = new System.Drawing.Point(4, 25);
            this.TabPageBookmark.Name = "TabPageBookmark";
            this.TabPageBookmark.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageBookmark.Size = new System.Drawing.Size(298, 542);
            this.TabPageBookmark.TabIndex = 1;
            this.TabPageBookmark.Text = "ブックマーク";
            this.TabPageBookmark.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnBookmarkAdd);
            this.flowLayoutPanel1.Controls.Add(this.btnBookmarkDelete);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(292, 29);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // btnBookmarkAdd
            // 
            this.btnBookmarkAdd.Location = new System.Drawing.Point(3, 3);
            this.btnBookmarkAdd.Name = "btnBookmarkAdd";
            this.btnBookmarkAdd.Size = new System.Drawing.Size(75, 23);
            this.btnBookmarkAdd.TabIndex = 0;
            this.btnBookmarkAdd.Text = "追加";
            this.btnBookmarkAdd.UseVisualStyleBackColor = true;
            this.btnBookmarkAdd.Click += new System.EventHandler(this.btnBookmarkAdd_Click);
            // 
            // btnBookmarkDelete
            // 
            this.btnBookmarkDelete.Location = new System.Drawing.Point(84, 3);
            this.btnBookmarkDelete.Name = "btnBookmarkDelete";
            this.btnBookmarkDelete.Size = new System.Drawing.Size(75, 23);
            this.btnBookmarkDelete.TabIndex = 1;
            this.btnBookmarkDelete.Text = "削除";
            this.btnBookmarkDelete.UseVisualStyleBackColor = true;
            this.btnBookmarkDelete.Click += new System.EventHandler(this.btnBookmarkDelete_Click);
            // 
            // listViewBookmark
            // 
            this.listViewBookmark.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewBookmark.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.listViewBookmarkName,
            this.listViewBookmarkByteIndex});
            this.listViewBookmark.HideSelection = false;
            this.listViewBookmark.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.listViewBookmark.LabelEdit = true;
            this.listViewBookmark.Location = new System.Drawing.Point(3, 35);
            this.listViewBookmark.Name = "listViewBookmark";
            this.listViewBookmark.Size = new System.Drawing.Size(285, 754);
            this.listViewBookmark.TabIndex = 0;
            this.listViewBookmark.UseCompatibleStateImageBehavior = false;
            this.listViewBookmark.View = System.Windows.Forms.View.Details;
            this.listViewBookmark.DoubleClick += new System.EventHandler(this.listViewBookmark_DoubleClick);
            // 
            // listViewBookmarkName
            // 
            this.listViewBookmarkName.Text = "名称";
            this.listViewBookmarkName.Width = 153;
            // 
            // listViewBookmarkByteIndex
            // 
            this.listViewBookmarkByteIndex.Text = "位置";
            this.listViewBookmarkByteIndex.Width = 89;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1272, 725);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "LargeTextViewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.TabPageProperty.ResumeLayout(false);
            this.TabPageBookmark.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuNew;
        private System.Windows.Forms.ToolStripMenuItem menuOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.ToolStripMenuItem menuEdit;
        private System.Windows.Forms.ToolStripMenuItem menuUndo;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem menuCut;
        private System.Windows.Forms.ToolStripMenuItem menuCopy;
        private System.Windows.Forms.ToolStripMenuItem menuPaste;
        private System.Windows.Forms.ToolStripMenuItem menuDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem menuFind;
        private System.Windows.Forms.ToolStripMenuItem menuFindNext;
        private System.Windows.Forms.ToolStripMenuItem menuFindPrev;
        private System.Windows.Forms.ToolStripMenuItem menuSelectLine;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtMain;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage TabPageProperty;
        private System.Windows.Forms.TabPage TabPageBookmark;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnBookmarkAdd;
        private System.Windows.Forms.Button btnBookmarkDelete;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.ListView listViewBookmark;
        private System.Windows.Forms.ColumnHeader listViewBookmarkName;
        private System.Windows.Forms.ColumnHeader listViewBookmarkByteIndex;
        private System.Windows.Forms.Button btnSearchPrev;
        private System.Windows.Forms.Button btnSearchNext;
        private System.Windows.Forms.CheckBox chkSearchWord;
        private System.Windows.Forms.CheckBox chkSearchUpperLower;
        private System.Windows.Forms.CheckBox chkSearchRegex;
        private System.Windows.Forms.TextBox txtSearchBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.ToolStripMenuItem menuReload;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button btnSearchStop;
    }
}

