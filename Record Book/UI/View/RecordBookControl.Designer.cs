namespace RecordBook.UI.View
{
    partial class RecordBookControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecordBookControl));
            this.xlTabDrawer1 = new GrayIris.Utilities.UI.Controls.XlTabDrawer();
            this.tab_extremes = new GrayIris.Utilities.UI.Controls.YaTabPage();
            this.bnrExtremes = new ZoneFiveSoftware.Common.Visuals.ActionBanner();
            this.treeExtremes = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.tabcontrol = new GrayIris.Utilities.UI.Controls.YaTabControl();
            this.tab_distance = new GrayIris.Utilities.UI.Controls.YaTabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.treeDistance = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.btnZoomIn = new ZoneFiveSoftware.Common.Visuals.Button();
            this.bnrDistance = new ZoneFiveSoftware.Common.Visuals.ActionBanner();
            this.txtStartDate = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.txtEndDate = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.btnZoomOut = new ZoneFiveSoftware.Common.Visuals.Button();
            this.btnStripes = new ZoneFiveSoftware.Common.Visuals.Button();
            this.btnSave = new ZoneFiveSoftware.Common.Visuals.Button();
            this.distanceChart = new ZoneFiveSoftware.Common.Visuals.Chart.ChartBase();
            this.btnZoomFit = new ZoneFiveSoftware.Common.Visuals.Button();
            this.btnSelectCharts = new ZoneFiveSoftware.Common.Visuals.Button();
            this.tab_rank = new GrayIris.Utilities.UI.Controls.YaTabPage();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.treeAllActivities = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.bnrWhereDoesItRank = new ZoneFiveSoftware.Common.Visuals.ActionBanner();
            this.splitContainer6 = new System.Windows.Forms.SplitContainer();
            this.treeTime = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.bnrThen = new ZoneFiveSoftware.Common.Visuals.ActionBanner();
            this.treeNow = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.bnrNow = new ZoneFiveSoftware.Common.Visuals.ActionBanner();
            this.mnuTopRec = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.top10ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.top25ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.top50ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.top100ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tab_extremes.SuspendLayout();
            this.tabcontrol.SuspendLayout();
            this.tab_distance.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.bnrDistance.SuspendLayout();
            this.tab_rank.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.splitContainer6.Panel1.SuspendLayout();
            this.splitContainer6.Panel2.SuspendLayout();
            this.splitContainer6.SuspendLayout();
            this.mnuTopRec.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab_extremes
            // 
            this.tab_extremes.Controls.Add(this.bnrExtremes);
            this.tab_extremes.Controls.Add(this.treeExtremes);
            this.tab_extremes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab_extremes.ImageIndex = -1;
            this.tab_extremes.Location = new System.Drawing.Point(4, 4);
            this.tab_extremes.Name = "tab_extremes";
            this.tab_extremes.Size = new System.Drawing.Size(671, 616);
            this.tab_extremes.TabIndex = 0;
            this.tab_extremes.Tag = "tab_extremes";
            this.tab_extremes.Text = "My Extremes";
            // 
            // bnrExtremes
            // 
            this.bnrExtremes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bnrExtremes.BackColor = System.Drawing.Color.Transparent;
            this.bnrExtremes.HasMenuButton = true;
            this.bnrExtremes.Location = new System.Drawing.Point(0, 0);
            this.bnrExtremes.Name = "bnrExtremes";
            this.bnrExtremes.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bnrExtremes.Size = new System.Drawing.Size(671, 24);
            this.bnrExtremes.Style = ZoneFiveSoftware.Common.Visuals.ActionBanner.BannerStyle.Header2;
            this.bnrExtremes.TabIndex = 1;
            this.bnrExtremes.Text = "My Extremes";
            this.bnrExtremes.UseStyleFont = true;
            this.bnrExtremes.MenuClicked += new System.EventHandler(this.bnr_MenuClicked);
            // 
            // treeExtremes
            // 
            this.treeExtremes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeExtremes.BackColor = System.Drawing.Color.Transparent;
            this.treeExtremes.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.treeExtremes.CheckBoxes = false;
            this.treeExtremes.DefaultIndent = 15;
            this.treeExtremes.DefaultRowHeight = -1;
            this.treeExtremes.HeaderRowHeight = 21;
            this.treeExtremes.Location = new System.Drawing.Point(0, 21);
            this.treeExtremes.MultiSelect = false;
            this.treeExtremes.Name = "treeExtremes";
            this.treeExtremes.NumHeaderRows = ZoneFiveSoftware.Common.Visuals.TreeList.HeaderRows.Auto;
            this.treeExtremes.NumLockedColumns = 0;
            this.treeExtremes.RowAlternatingColors = true;
            this.treeExtremes.RowHotlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(147)))), ((int)(((byte)(160)))), ((int)(((byte)(112)))));
            this.treeExtremes.RowHotlightColorText = System.Drawing.SystemColors.HighlightText;
            this.treeExtremes.RowHotlightMouse = true;
            this.treeExtremes.RowSelectedColor = System.Drawing.SystemColors.Highlight;
            this.treeExtremes.RowSelectedColorText = System.Drawing.SystemColors.HighlightText;
            this.treeExtremes.RowSeparatorLines = true;
            this.treeExtremes.ShowLines = true;
            this.treeExtremes.ShowPlusMinus = true;
            this.treeExtremes.Size = new System.Drawing.Size(671, 595);
            this.treeExtremes.TabIndex = 2;
            this.treeExtremes.SelectedItemsChanged += new System.EventHandler(this.treeRecords_SelectedChanged);
            this.treeExtremes.ColumnResized += new ZoneFiveSoftware.Common.Visuals.TreeList.ColumnEventHandler(this.Extremes_ColumnResized);
            this.treeExtremes.ColumnClicked += new ZoneFiveSoftware.Common.Visuals.TreeList.ColumnEventHandler(this.tree_ColumnClicked);
            this.treeExtremes.Click += new System.EventHandler(this.treeExtremes_Click);
            this.treeExtremes.DoubleClick += new System.EventHandler(this.tree_DoubleClick);
            // 
            // tabcontrol
            // 
            this.tabcontrol.ActiveColor = System.Drawing.SystemColors.Control;
            this.tabcontrol.BackColor = System.Drawing.SystemColors.Control;
            this.tabcontrol.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.tabcontrol.Controls.Add(this.tab_extremes);
            this.tabcontrol.Controls.Add(this.tab_distance);
            this.tabcontrol.Controls.Add(this.tab_rank);
            this.tabcontrol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabcontrol.ImageIndex = -1;
            this.tabcontrol.ImageList = null;
            this.tabcontrol.InactiveColor = System.Drawing.SystemColors.Window;
            this.tabcontrol.Location = new System.Drawing.Point(0, 0);
            this.tabcontrol.Name = "tabcontrol";
            this.tabcontrol.ScrollButtonStyle = GrayIris.Utilities.UI.Controls.YaScrollButtonStyle.Always;
            this.tabcontrol.SelectedIndex = 1;
            this.tabcontrol.SelectedTab = this.tab_distance;
            this.tabcontrol.Size = new System.Drawing.Size(705, 624);
            this.tabcontrol.TabDock = System.Windows.Forms.DockStyle.Right;
            this.tabcontrol.TabDrawer = this.xlTabDrawer1;
            this.tabcontrol.TabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabcontrol.TabIndex = 0;
            this.tabcontrol.Text = "Next Tab";
            this.tabcontrol.TabChanged += new System.EventHandler(this.tabcontrol_TabChanged);
            // 
            // tab_distance
            // 
            this.tab_distance.Controls.Add(this.splitContainer3);
            this.tab_distance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab_distance.ImageIndex = -1;
            this.tab_distance.Location = new System.Drawing.Point(4, 4);
            this.tab_distance.Name = "tab_distance";
            this.tab_distance.Size = new System.Drawing.Size(671, 616);
            this.tab_distance.TabIndex = 1;
            this.tab_distance.Tag = "tab_distance";
            this.tab_distance.Text = "Distance";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.treeDistance);
            this.splitContainer3.Panel1.Controls.Add(this.bnrDistance);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.btnZoomOut);
            this.splitContainer3.Panel2.Controls.Add(this.btnStripes);
            this.splitContainer3.Panel2.Controls.Add(this.btnZoomIn);
            this.splitContainer3.Panel2.Controls.Add(this.btnSave);
            this.splitContainer3.Panel2.Controls.Add(this.distanceChart);
            this.splitContainer3.Panel2.Controls.Add(this.btnZoomFit);
            this.splitContainer3.Panel2.Controls.Add(this.btnSelectCharts);
            this.splitContainer3.Size = new System.Drawing.Size(671, 616);
            this.splitContainer3.SplitterDistance = 311;
            this.splitContainer3.TabIndex = 0;
            // 
            // treeDistance
            // 
            this.treeDistance.BackColor = System.Drawing.Color.Transparent;
            this.treeDistance.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.treeDistance.CheckBoxes = false;
            this.treeDistance.DefaultIndent = 15;
            this.treeDistance.DefaultRowHeight = -1;
            this.treeDistance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeDistance.HeaderRowHeight = 21;
            this.treeDistance.Location = new System.Drawing.Point(0, 24);
            this.treeDistance.MultiSelect = true;
            this.treeDistance.Name = "treeDistance";
            this.treeDistance.NumHeaderRows = ZoneFiveSoftware.Common.Visuals.TreeList.HeaderRows.Auto;
            this.treeDistance.NumLockedColumns = 0;
            this.treeDistance.RowAlternatingColors = true;
            this.treeDistance.RowHotlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(147)))), ((int)(((byte)(160)))), ((int)(((byte)(112)))));
            this.treeDistance.RowHotlightColorText = System.Drawing.SystemColors.HighlightText;
            this.treeDistance.RowHotlightMouse = true;
            this.treeDistance.RowSelectedColor = System.Drawing.SystemColors.Highlight;
            this.treeDistance.RowSelectedColorText = System.Drawing.SystemColors.HighlightText;
            this.treeDistance.RowSeparatorLines = true;
            this.treeDistance.ShowLines = true;
            this.treeDistance.ShowPlusMinus = true;
            this.treeDistance.Size = new System.Drawing.Size(671, 287);
            this.treeDistance.TabIndex = 3;
            this.treeDistance.SelectedItemsChanged += new System.EventHandler(this.treeRecords_SelectedChanged);
            this.treeDistance.ColumnResized += new ZoneFiveSoftware.Common.Visuals.TreeList.ColumnEventHandler(this.Distance_ColumnResized);
            this.treeDistance.ColumnClicked += new ZoneFiveSoftware.Common.Visuals.TreeList.ColumnEventHandler(this.tree_ColumnClicked);
            this.treeDistance.Click += new System.EventHandler(this.treeDistance_Click);
            this.treeDistance.DoubleClick += new System.EventHandler(this.tree_DoubleClick);
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnZoomIn.BackColor = System.Drawing.Color.Transparent;
            this.btnZoomIn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnZoomIn.BackgroundImage")));
            this.btnZoomIn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnZoomIn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnZoomIn.CenterImage = null;
            this.btnZoomIn.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnZoomIn.HyperlinkStyle = false;
            this.btnZoomIn.ImageMargin = 2;
            this.btnZoomIn.LeftImage = null;
            this.btnZoomIn.Location = new System.Drawing.Point(506, 2);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.PushStyle = true;
            this.btnZoomIn.RightImage = null;
            this.btnZoomIn.Size = new System.Drawing.Size(22, 21);
            this.btnZoomIn.TabIndex = 2;
            this.btnZoomIn.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnZoomIn.TextLeftMargin = 2;
            this.btnZoomIn.TextRightMargin = 2;
            this.btnZoomIn.Click += new System.EventHandler(this.zoomInButton_Click);
            // 
            // bnrDistance
            // 
            this.bnrDistance.BackColor = System.Drawing.Color.Transparent;
            this.bnrDistance.Controls.Add(this.txtStartDate);
            this.bnrDistance.Controls.Add(this.txtEndDate);
            this.bnrDistance.Dock = System.Windows.Forms.DockStyle.Top;
            this.bnrDistance.HasMenuButton = true;
            this.bnrDistance.Location = new System.Drawing.Point(0, 0);
            this.bnrDistance.Name = "bnrDistance";
            this.bnrDistance.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bnrDistance.Size = new System.Drawing.Size(671, 24);
            this.bnrDistance.Style = ZoneFiveSoftware.Common.Visuals.ActionBanner.BannerStyle.Header2;
            this.bnrDistance.TabIndex = 2;
            this.bnrDistance.Text = "My Distance Records";
            this.bnrDistance.UseStyleFont = true;
            this.bnrDistance.MenuClicked += new System.EventHandler(this.bnr_MenuClicked);
            // 
            // txtStartDate
            // 
            this.txtStartDate.AcceptsReturn = false;
            this.txtStartDate.AcceptsTab = false;
            this.txtStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStartDate.BackColor = System.Drawing.Color.White;
            this.txtStartDate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtStartDate.ButtonImage = ((System.Drawing.Image)(resources.GetObject("txtStartDate.ButtonImage")));
            this.txtStartDate.Location = new System.Drawing.Point(441, 2);
            this.txtStartDate.MaxLength = 32767;
            this.txtStartDate.Multiline = false;
            this.txtStartDate.Name = "txtStartDate";
            this.txtStartDate.ReadOnly = false;
            this.txtStartDate.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtStartDate.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtStartDate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtStartDate.Size = new System.Drawing.Size(87, 19);
            this.txtStartDate.TabIndex = 11;
            this.txtStartDate.Tag = "460";
            this.txtStartDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtStartDate.ButtonClick += new System.EventHandler(this.btnCalendar_Click);
            this.txtStartDate.Leave += new System.EventHandler(this.txtFilterDate_Leave);
            // 
            // txtEndDate
            // 
            this.txtEndDate.AcceptsReturn = false;
            this.txtEndDate.AcceptsTab = false;
            this.txtEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEndDate.BackColor = System.Drawing.Color.White;
            this.txtEndDate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtEndDate.ButtonImage = ((System.Drawing.Image)(resources.GetObject("txtEndDate.ButtonImage")));
            this.txtEndDate.Location = new System.Drawing.Point(554, 2);
            this.txtEndDate.MaxLength = 32767;
            this.txtEndDate.Multiline = false;
            this.txtEndDate.Name = "txtEndDate";
            this.txtEndDate.ReadOnly = false;
            this.txtEndDate.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtEndDate.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtEndDate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtEndDate.Size = new System.Drawing.Size(87, 19);
            this.txtEndDate.TabIndex = 11;
            this.txtEndDate.Tag = "553";
            this.txtEndDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtEndDate.ButtonClick += new System.EventHandler(this.btnCalendar_Click);
            this.txtEndDate.Leave += new System.EventHandler(this.txtFilterDate_Leave);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnZoomOut.BackColor = System.Drawing.Color.Transparent;
            this.btnZoomOut.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnZoomOut.BackgroundImage")));
            this.btnZoomOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnZoomOut.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnZoomOut.CenterImage = null;
            this.btnZoomOut.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnZoomOut.HyperlinkStyle = false;
            this.btnZoomOut.ImageMargin = 2;
            this.btnZoomOut.LeftImage = null;
            this.btnZoomOut.Location = new System.Drawing.Point(534, 2);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.PushStyle = true;
            this.btnZoomOut.RightImage = null;
            this.btnZoomOut.Size = new System.Drawing.Size(22, 21);
            this.btnZoomOut.TabIndex = 3;
            this.btnZoomOut.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnZoomOut.TextLeftMargin = 2;
            this.btnZoomOut.TextRightMargin = 2;
            this.btnZoomOut.Click += new System.EventHandler(this.zoomOutButton_Click);
            // 
            // btnStripes
            // 
            this.btnStripes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStripes.BackColor = System.Drawing.Color.Transparent;
            this.btnStripes.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnStripes.BackgroundImage")));
            this.btnStripes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnStripes.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnStripes.CenterImage = null;
            this.btnStripes.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnStripes.HyperlinkStyle = false;
            this.btnStripes.ImageMargin = 2;
            this.btnStripes.LeftImage = null;
            this.btnStripes.Location = new System.Drawing.Point(590, 2);
            this.btnStripes.Name = "btnStripes";
            this.btnStripes.PushStyle = true;
            this.btnStripes.RightImage = null;
            this.btnStripes.Size = new System.Drawing.Size(22, 21);
            this.btnStripes.TabIndex = 4;
            this.btnStripes.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnStripes.TextLeftMargin = 2;
            this.btnStripes.TextRightMargin = 2;
            this.btnStripes.Click += new System.EventHandler(this.stripesButton_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSave.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnSave.CenterImage = null;
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSave.HyperlinkStyle = false;
            this.btnSave.ImageMargin = 2;
            this.btnSave.LeftImage = null;
            this.btnSave.Location = new System.Drawing.Point(562, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.PushStyle = true;
            this.btnSave.RightImage = null;
            this.btnSave.Size = new System.Drawing.Size(22, 21);
            this.btnSave.TabIndex = 6;
            this.btnSave.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnSave.TextLeftMargin = 2;
            this.btnSave.TextRightMargin = 2;
            this.btnSave.Click += new System.EventHandler(this.savePicButton_Click);
            // 
            // distanceChart
            // 
            this.distanceChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.distanceChart.BackColor = System.Drawing.Color.Transparent;
            this.distanceChart.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.distanceChart.Location = new System.Drawing.Point(0, 24);
            this.distanceChart.Name = "distanceChart";
            this.distanceChart.Padding = new System.Windows.Forms.Padding(5);
            this.distanceChart.Size = new System.Drawing.Size(671, 277);
            this.distanceChart.TabIndex = 0;
            // 
            // btnZoomFit
            // 
            this.btnZoomFit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnZoomFit.BackColor = System.Drawing.Color.Transparent;
            this.btnZoomFit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnZoomFit.BackgroundImage")));
            this.btnZoomFit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnZoomFit.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnZoomFit.CenterImage = null;
            this.btnZoomFit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnZoomFit.HyperlinkStyle = false;
            this.btnZoomFit.ImageMargin = 2;
            this.btnZoomFit.LeftImage = null;
            this.btnZoomFit.Location = new System.Drawing.Point(646, 2);
            this.btnZoomFit.Name = "btnZoomFit";
            this.btnZoomFit.PushStyle = true;
            this.btnZoomFit.RightImage = null;
            this.btnZoomFit.Size = new System.Drawing.Size(22, 21);
            this.btnZoomFit.TabIndex = 1;
            this.btnZoomFit.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnZoomFit.TextLeftMargin = 2;
            this.btnZoomFit.TextRightMargin = 2;
            this.btnZoomFit.Click += new System.EventHandler(this.autoFitButton_Click);
            // 
            // btnSelectCharts
            // 
            this.btnSelectCharts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectCharts.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectCharts.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSelectCharts.BackgroundImage")));
            this.btnSelectCharts.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSelectCharts.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnSelectCharts.CenterImage = null;
            this.btnSelectCharts.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSelectCharts.HyperlinkStyle = false;
            this.btnSelectCharts.ImageMargin = 2;
            this.btnSelectCharts.LeftImage = null;
            this.btnSelectCharts.Location = new System.Drawing.Point(618, 2);
            this.btnSelectCharts.Name = "btnSelectCharts";
            this.btnSelectCharts.PushStyle = true;
            this.btnSelectCharts.RightImage = null;
            this.btnSelectCharts.Size = new System.Drawing.Size(22, 21);
            this.btnSelectCharts.TabIndex = 5;
            this.btnSelectCharts.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnSelectCharts.TextLeftMargin = 2;
            this.btnSelectCharts.TextRightMargin = 2;
            this.btnSelectCharts.Click += new System.EventHandler(this.selectFieldsButton_Click);
            // 
            // tab_rank
            // 
            this.tab_rank.Controls.Add(this.splitContainer4);
            this.tab_rank.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab_rank.ImageIndex = -1;
            this.tab_rank.Location = new System.Drawing.Point(4, 4);
            this.tab_rank.Name = "tab_rank";
            this.tab_rank.Size = new System.Drawing.Size(671, 616);
            this.tab_rank.TabIndex = 2;
            this.tab_rank.Tag = "tab_rank";
            this.tab_rank.Text = "Where does it rank";
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.treeAllActivities);
            this.splitContainer4.Panel1.Controls.Add(this.bnrWhereDoesItRank);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.splitContainer6);
            this.splitContainer4.Size = new System.Drawing.Size(671, 616);
            this.splitContainer4.SplitterDistance = 206;
            this.splitContainer4.TabIndex = 0;
            // 
            // treeAllActivities
            // 
            this.treeAllActivities.BackColor = System.Drawing.Color.Transparent;
            this.treeAllActivities.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.treeAllActivities.CheckBoxes = false;
            this.treeAllActivities.DefaultIndent = 15;
            this.treeAllActivities.DefaultRowHeight = -1;
            this.treeAllActivities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeAllActivities.HeaderRowHeight = 21;
            this.treeAllActivities.Location = new System.Drawing.Point(0, 24);
            this.treeAllActivities.MultiSelect = true;
            this.treeAllActivities.Name = "treeAllActivities";
            this.treeAllActivities.NumHeaderRows = ZoneFiveSoftware.Common.Visuals.TreeList.HeaderRows.Auto;
            this.treeAllActivities.NumLockedColumns = 0;
            this.treeAllActivities.RowAlternatingColors = true;
            this.treeAllActivities.RowHotlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.treeAllActivities.RowHotlightColorText = System.Drawing.SystemColors.HighlightText;
            this.treeAllActivities.RowHotlightMouse = true;
            this.treeAllActivities.RowSelectedColor = System.Drawing.SystemColors.Highlight;
            this.treeAllActivities.RowSelectedColorText = System.Drawing.SystemColors.HighlightText;
            this.treeAllActivities.RowSeparatorLines = true;
            this.treeAllActivities.ShowLines = false;
            this.treeAllActivities.ShowPlusMinus = false;
            this.treeAllActivities.Size = new System.Drawing.Size(671, 182);
            this.treeAllActivities.TabIndex = 1;
            this.treeAllActivities.SelectedItemsChanged += new System.EventHandler(this.AllActivities_SelectedChanged);
            this.treeAllActivities.ColumnResized += new ZoneFiveSoftware.Common.Visuals.TreeList.ColumnEventHandler(this.AllActivities_ColumnResized);
            this.treeAllActivities.ColumnClicked += new ZoneFiveSoftware.Common.Visuals.TreeList.ColumnEventHandler(this.tree_ColumnClicked);
            this.treeAllActivities.Click += new System.EventHandler(this.treeAllActivities_Click);
            this.treeAllActivities.DoubleClick += new System.EventHandler(this.tree_DoubleClick);
            // 
            // bnrWhereDoesItRank
            // 
            this.bnrWhereDoesItRank.BackColor = System.Drawing.Color.Transparent;
            this.bnrWhereDoesItRank.Dock = System.Windows.Forms.DockStyle.Top;
            this.bnrWhereDoesItRank.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnrWhereDoesItRank.HasMenuButton = false;
            this.bnrWhereDoesItRank.Location = new System.Drawing.Point(0, 0);
            this.bnrWhereDoesItRank.Name = "bnrWhereDoesItRank";
            this.bnrWhereDoesItRank.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bnrWhereDoesItRank.Size = new System.Drawing.Size(671, 24);
            this.bnrWhereDoesItRank.Style = ZoneFiveSoftware.Common.Visuals.ActionBanner.BannerStyle.Header2;
            this.bnrWhereDoesItRank.TabIndex = 0;
            this.bnrWhereDoesItRank.Text = "Where does it rank?";
            this.bnrWhereDoesItRank.UseStyleFont = true;
            // 
            // splitContainer6
            // 
            this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer6.Location = new System.Drawing.Point(0, 0);
            this.splitContainer6.Name = "splitContainer6";
            // 
            // splitContainer6.Panel1
            // 
            this.splitContainer6.Panel1.Controls.Add(this.treeTime);
            this.splitContainer6.Panel1.Controls.Add(this.bnrThen);
            // 
            // splitContainer6.Panel2
            // 
            this.splitContainer6.Panel2.Controls.Add(this.treeNow);
            this.splitContainer6.Panel2.Controls.Add(this.bnrNow);
            this.splitContainer6.Size = new System.Drawing.Size(671, 406);
            this.splitContainer6.SplitterDistance = 341;
            this.splitContainer6.TabIndex = 0;
            // 
            // treeTime
            // 
            this.treeTime.BackColor = System.Drawing.Color.Transparent;
            this.treeTime.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.treeTime.CheckBoxes = false;
            this.treeTime.DefaultIndent = 15;
            this.treeTime.DefaultRowHeight = -1;
            this.treeTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeTime.HeaderRowHeight = 21;
            this.treeTime.Location = new System.Drawing.Point(0, 24);
            this.treeTime.MultiSelect = false;
            this.treeTime.Name = "treeTime";
            this.treeTime.NumHeaderRows = ZoneFiveSoftware.Common.Visuals.TreeList.HeaderRows.Auto;
            this.treeTime.NumLockedColumns = 0;
            this.treeTime.RowAlternatingColors = true;
            this.treeTime.RowHotlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.treeTime.RowHotlightColorText = System.Drawing.SystemColors.HighlightText;
            this.treeTime.RowHotlightMouse = true;
            this.treeTime.RowSelectedColor = System.Drawing.SystemColors.Highlight;
            this.treeTime.RowSelectedColorText = System.Drawing.SystemColors.HighlightText;
            this.treeTime.RowSeparatorLines = true;
            this.treeTime.ShowLines = false;
            this.treeTime.ShowPlusMinus = false;
            this.treeTime.Size = new System.Drawing.Size(341, 382);
            this.treeTime.TabIndex = 1;
            // 
            // bnrThen
            // 
            this.bnrThen.BackColor = System.Drawing.Color.Transparent;
            this.bnrThen.Dock = System.Windows.Forms.DockStyle.Top;
            this.bnrThen.HasMenuButton = false;
            this.bnrThen.Location = new System.Drawing.Point(0, 0);
            this.bnrThen.Name = "bnrThen";
            this.bnrThen.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bnrThen.Size = new System.Drawing.Size(341, 24);
            this.bnrThen.Style = ZoneFiveSoftware.Common.Visuals.ActionBanner.BannerStyle.Header2;
            this.bnrThen.TabIndex = 0;
            this.bnrThen.Text = "At the time";
            this.bnrThen.UseStyleFont = true;
            // 
            // treeNow
            // 
            this.treeNow.BackColor = System.Drawing.Color.Transparent;
            this.treeNow.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.treeNow.CheckBoxes = false;
            this.treeNow.DefaultIndent = 15;
            this.treeNow.DefaultRowHeight = -1;
            this.treeNow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeNow.HeaderRowHeight = 21;
            this.treeNow.Location = new System.Drawing.Point(0, 24);
            this.treeNow.MultiSelect = false;
            this.treeNow.Name = "treeNow";
            this.treeNow.NumHeaderRows = ZoneFiveSoftware.Common.Visuals.TreeList.HeaderRows.Auto;
            this.treeNow.NumLockedColumns = 0;
            this.treeNow.RowAlternatingColors = true;
            this.treeNow.RowHotlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.treeNow.RowHotlightColorText = System.Drawing.SystemColors.HighlightText;
            this.treeNow.RowHotlightMouse = true;
            this.treeNow.RowSelectedColor = System.Drawing.SystemColors.Highlight;
            this.treeNow.RowSelectedColorText = System.Drawing.SystemColors.HighlightText;
            this.treeNow.RowSeparatorLines = true;
            this.treeNow.ShowLines = false;
            this.treeNow.ShowPlusMinus = false;
            this.treeNow.Size = new System.Drawing.Size(326, 382);
            this.treeNow.TabIndex = 2;
            // 
            // bnrNow
            // 
            this.bnrNow.BackColor = System.Drawing.Color.Transparent;
            this.bnrNow.Dock = System.Windows.Forms.DockStyle.Top;
            this.bnrNow.HasMenuButton = false;
            this.bnrNow.Location = new System.Drawing.Point(0, 0);
            this.bnrNow.Name = "bnrNow";
            this.bnrNow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bnrNow.Size = new System.Drawing.Size(326, 24);
            this.bnrNow.Style = ZoneFiveSoftware.Common.Visuals.ActionBanner.BannerStyle.Header2;
            this.bnrNow.TabIndex = 0;
            this.bnrNow.Text = "Now";
            this.bnrNow.UseStyleFont = true;
            // 
            // mnuTopRec
            // 
            this.mnuTopRec.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.top10ToolStripMenuItem,
            this.top25ToolStripMenuItem,
            this.top50ToolStripMenuItem,
            this.top100ToolStripMenuItem,
            this.allToolStripMenuItem});
            this.mnuTopRec.Name = "mnuTopRec";
            this.mnuTopRec.Size = new System.Drawing.Size(115, 114);
            // 
            // top10ToolStripMenuItem
            // 
            this.top10ToolStripMenuItem.CheckOnClick = true;
            this.top10ToolStripMenuItem.Name = "top10ToolStripMenuItem";
            this.top10ToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.top10ToolStripMenuItem.Tag = "10";
            this.top10ToolStripMenuItem.Text = "Top 10";
            this.top10ToolStripMenuItem.Click += new System.EventHandler(this.topMenuItem_Click);
            // 
            // top25ToolStripMenuItem
            // 
            this.top25ToolStripMenuItem.CheckOnClick = true;
            this.top25ToolStripMenuItem.Name = "top25ToolStripMenuItem";
            this.top25ToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.top25ToolStripMenuItem.Tag = "25";
            this.top25ToolStripMenuItem.Text = "Top 25";
            this.top25ToolStripMenuItem.Click += new System.EventHandler(this.topMenuItem_Click);
            // 
            // top50ToolStripMenuItem
            // 
            this.top50ToolStripMenuItem.CheckOnClick = true;
            this.top50ToolStripMenuItem.Name = "top50ToolStripMenuItem";
            this.top50ToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.top50ToolStripMenuItem.Tag = "50";
            this.top50ToolStripMenuItem.Text = "Top 50";
            this.top50ToolStripMenuItem.Click += new System.EventHandler(this.topMenuItem_Click);
            // 
            // top100ToolStripMenuItem
            // 
            this.top100ToolStripMenuItem.CheckOnClick = true;
            this.top100ToolStripMenuItem.Name = "top100ToolStripMenuItem";
            this.top100ToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.top100ToolStripMenuItem.Tag = "100";
            this.top100ToolStripMenuItem.Text = "Top 100";
            this.top100ToolStripMenuItem.Click += new System.EventHandler(this.topMenuItem_Click);
            // 
            // allToolStripMenuItem
            // 
            this.allToolStripMenuItem.CheckOnClick = true;
            this.allToolStripMenuItem.Name = "allToolStripMenuItem";
            this.allToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.allToolStripMenuItem.Tag = "0";
            this.allToolStripMenuItem.Text = "All";
            this.allToolStripMenuItem.Click += new System.EventHandler(this.topMenuItem_Click);
            // 
            // RecordBookControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabcontrol);
            this.Name = "RecordBookControl";
            this.Size = new System.Drawing.Size(705, 624);
            this.tab_extremes.ResumeLayout(false);
            this.tabcontrol.ResumeLayout(false);
            this.tab_distance.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.bnrDistance.ResumeLayout(false);
            this.tab_rank.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer6.Panel1.ResumeLayout(false);
            this.splitContainer6.Panel2.ResumeLayout(false);
            this.splitContainer6.ResumeLayout(false);
            this.mnuTopRec.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GrayIris.Utilities.UI.Controls.XlTabDrawer xlTabDrawer1;
        private GrayIris.Utilities.UI.Controls.YaTabPage tab_extremes;
        private ZoneFiveSoftware.Common.Visuals.TreeList treeExtremes;
        private ZoneFiveSoftware.Common.Visuals.ActionBanner bnrExtremes;
        private GrayIris.Utilities.UI.Controls.YaTabControl tabcontrol;
        private GrayIris.Utilities.UI.Controls.YaTabPage tab_distance;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private ZoneFiveSoftware.Common.Visuals.TreeList treeDistance;
        private ZoneFiveSoftware.Common.Visuals.ActionBanner bnrDistance;
        private GrayIris.Utilities.UI.Controls.YaTabPage tab_rank;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private ZoneFiveSoftware.Common.Visuals.ActionBanner bnrWhereDoesItRank;
        private System.Windows.Forms.SplitContainer splitContainer6;
        private ZoneFiveSoftware.Common.Visuals.ActionBanner bnrThen;
        private ZoneFiveSoftware.Common.Visuals.TreeList treeAllActivities;
        private ZoneFiveSoftware.Common.Visuals.ActionBanner bnrNow;
        private ZoneFiveSoftware.Common.Visuals.TreeList treeTime;
        private ZoneFiveSoftware.Common.Visuals.TreeList treeNow;
        private ZoneFiveSoftware.Common.Visuals.Chart.ChartBase distanceChart;
        private ZoneFiveSoftware.Common.Visuals.Button btnZoomFit;
        private ZoneFiveSoftware.Common.Visuals.Button btnSave;
        private ZoneFiveSoftware.Common.Visuals.Button btnSelectCharts;
        private ZoneFiveSoftware.Common.Visuals.Button btnStripes;
        private ZoneFiveSoftware.Common.Visuals.Button btnZoomOut;
        private ZoneFiveSoftware.Common.Visuals.Button btnZoomIn;
        private System.Windows.Forms.ContextMenuStrip mnuTopRec;
        private System.Windows.Forms.ToolStripMenuItem top10ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem top25ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem top50ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem top100ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtStartDate;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtEndDate;
    }
}
