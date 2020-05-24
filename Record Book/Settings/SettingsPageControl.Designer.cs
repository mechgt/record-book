namespace RecordBook.Settings
{
    partial class SettingsPageControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsPageControl));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeDistance = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.pnlDistHeader = new ZoneFiveSoftware.Common.Visuals.Panel();
            this.txtDistUnit = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.btnResetDistance = new ZoneFiveSoftware.Common.Visuals.Button();
            this.btnSaveDist = new ZoneFiveSoftware.Common.Visuals.Button();
            this.btnDistDown = new ZoneFiveSoftware.Common.Visuals.Button();
            this.btnRemoveDist = new ZoneFiveSoftware.Common.Visuals.Button();
            this.txtDistDistance = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.btnDistUp = new ZoneFiveSoftware.Common.Visuals.Button();
            this.lblDistance = new System.Windows.Forms.Label();
            this.txtDistName = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.lblUnits = new System.Windows.Forms.Label();
            this.bnrDistance = new ZoneFiveSoftware.Common.Visuals.ActionBanner();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.treeExt = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.pnlExtHeader = new ZoneFiveSoftware.Common.Visuals.Panel();
            this.txtExtType = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.lblName2 = new System.Windows.Forms.Label();
            this.extremes_down_button = new ZoneFiveSoftware.Common.Visuals.Button();
            this.btnResetExtreme = new ZoneFiveSoftware.Common.Visuals.Button();
            this.btnSaveExt = new ZoneFiveSoftware.Common.Visuals.Button();
            this.extremes_up_button = new ZoneFiveSoftware.Common.Visuals.Button();
            this.lblType2 = new System.Windows.Forms.Label();
            this.txtExtName = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.remove_extreme_button = new ZoneFiveSoftware.Common.Visuals.Button();
            this.bnrExtreme = new ZoneFiveSoftware.Common.Visuals.ActionBanner();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.treeNow = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.pnlNowHeader = new ZoneFiveSoftware.Common.Visuals.Panel();
            this.txtNowType = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.lblName3 = new System.Windows.Forms.Label();
            this.btnResetRank = new ZoneFiveSoftware.Common.Visuals.Button();
            this.btnSaveNowThen = new ZoneFiveSoftware.Common.Visuals.Button();
            this.remove_nowthen_button = new ZoneFiveSoftware.Common.Visuals.Button();
            this.nowThen_down_button = new ZoneFiveSoftware.Common.Visuals.Button();
            this.lblType3 = new System.Windows.Forms.Label();
            this.nowThen_up_button = new ZoneFiveSoftware.Common.Visuals.Button();
            this.txtNowName = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.bnrNowThen = new ZoneFiveSoftware.Common.Visuals.ActionBanner();
            this.splitVertical = new System.Windows.Forms.SplitContainer();
            this.treeCategory = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.bnrCategories = new ZoneFiveSoftware.Common.Visuals.ActionBanner();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnlDistHeader.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.pnlExtHeader.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.pnlNowHeader.SuspendLayout();
            this.splitVertical.Panel1.SuspendLayout();
            this.splitVertical.Panel2.SuspendLayout();
            this.splitVertical.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeDistance);
            this.splitContainer1.Panel1.Controls.Add(this.pnlDistHeader);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(571, 771);
            this.splitContainer1.SplitterDistance = 206;
            this.splitContainer1.TabIndex = 10;
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
            this.treeDistance.Location = new System.Drawing.Point(0, 61);
            this.treeDistance.MultiSelect = false;
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
            this.treeDistance.ShowLines = false;
            this.treeDistance.ShowPlusMinus = false;
            this.treeDistance.Size = new System.Drawing.Size(571, 145);
            this.treeDistance.TabIndex = 0;
            this.treeDistance.SelectedItemsChanged += new System.EventHandler(this.treeDistance_SelectionChanged);
            // 
            // pnlDistHeader
            // 
            this.pnlDistHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlDistHeader.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.None;
            this.pnlDistHeader.BorderColor = System.Drawing.Color.Gray;
            this.pnlDistHeader.Controls.Add(this.txtDistUnit);
            this.pnlDistHeader.Controls.Add(this.btnResetDistance);
            this.pnlDistHeader.Controls.Add(this.btnSaveDist);
            this.pnlDistHeader.Controls.Add(this.btnDistDown);
            this.pnlDistHeader.Controls.Add(this.btnRemoveDist);
            this.pnlDistHeader.Controls.Add(this.txtDistDistance);
            this.pnlDistHeader.Controls.Add(this.lblName);
            this.pnlDistHeader.Controls.Add(this.btnDistUp);
            this.pnlDistHeader.Controls.Add(this.lblDistance);
            this.pnlDistHeader.Controls.Add(this.txtDistName);
            this.pnlDistHeader.Controls.Add(this.lblUnits);
            this.pnlDistHeader.Controls.Add(this.bnrDistance);
            this.pnlDistHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDistHeader.HeadingBackColor = System.Drawing.Color.LightBlue;
            this.pnlDistHeader.HeadingFont = null;
            this.pnlDistHeader.HeadingLeftMargin = 0;
            this.pnlDistHeader.HeadingText = null;
            this.pnlDistHeader.HeadingTextColor = System.Drawing.Color.Black;
            this.pnlDistHeader.HeadingTopMargin = 3;
            this.pnlDistHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlDistHeader.Name = "pnlDistHeader";
            this.pnlDistHeader.Size = new System.Drawing.Size(571, 61);
            this.pnlDistHeader.TabIndex = 22;
            // 
            // txtDistUnit
            // 
            this.txtDistUnit.AcceptsReturn = false;
            this.txtDistUnit.AcceptsTab = false;
            this.txtDistUnit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDistUnit.BackColor = System.Drawing.Color.White;
            this.txtDistUnit.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtDistUnit.ButtonImage = ((System.Drawing.Image)(resources.GetObject("txtDistUnit.ButtonImage")));
            this.txtDistUnit.Location = new System.Drawing.Point(480, 35);
            this.txtDistUnit.MaxLength = 32767;
            this.txtDistUnit.Multiline = false;
            this.txtDistUnit.Name = "txtDistUnit";
            this.txtDistUnit.ReadOnly = true;
            this.txtDistUnit.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtDistUnit.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtDistUnit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtDistUnit.Size = new System.Drawing.Size(88, 21);
            this.txtDistUnit.TabIndex = 6;
            this.txtDistUnit.Tag = "ExtType";
            this.txtDistUnit.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDistUnit.ButtonClick += new System.EventHandler(this.txtType_ButtonClick);
            // 
            // btnResetDistance
            // 
            this.btnResetDistance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetDistance.BackColor = System.Drawing.Color.Transparent;
            this.btnResetDistance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnResetDistance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnResetDistance.CenterImage = null;
            this.btnResetDistance.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnResetDistance.HyperlinkStyle = false;
            this.btnResetDistance.ImageMargin = 2;
            this.btnResetDistance.LeftImage = ((System.Drawing.Image)(resources.GetObject("btnResetDistance.LeftImage")));
            this.btnResetDistance.Location = new System.Drawing.Point(364, 2);
            this.btnResetDistance.Name = "btnResetDistance";
            this.btnResetDistance.PushStyle = true;
            this.btnResetDistance.RightImage = null;
            this.btnResetDistance.Size = new System.Drawing.Size(70, 27);
            this.btnResetDistance.TabIndex = 0;
            this.btnResetDistance.Tag = "Distance";
            this.btnResetDistance.Text = "Reset";
            this.btnResetDistance.TextAlign = System.Drawing.StringAlignment.Near;
            this.btnResetDistance.TextLeftMargin = 2;
            this.btnResetDistance.TextRightMargin = 2;
            this.btnResetDistance.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSaveDist
            // 
            this.btnSaveDist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveDist.BackColor = System.Drawing.Color.Transparent;
            this.btnSaveDist.BackgroundImage = global::RecordBook.Resources.Images.add;
            this.btnSaveDist.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSaveDist.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnSaveDist.CenterImage = null;
            this.btnSaveDist.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSaveDist.HyperlinkStyle = false;
            this.btnSaveDist.ImageMargin = 2;
            this.btnSaveDist.LeftImage = null;
            this.btnSaveDist.Location = new System.Drawing.Point(438, 2);
            this.btnSaveDist.Name = "btnSaveDist";
            this.btnSaveDist.PushStyle = true;
            this.btnSaveDist.RightImage = null;
            this.btnSaveDist.Size = new System.Drawing.Size(28, 27);
            this.btnSaveDist.TabIndex = 0;
            this.btnSaveDist.TextAlign = System.Drawing.StringAlignment.Far;
            this.btnSaveDist.TextLeftMargin = 2;
            this.btnSaveDist.TextRightMargin = 2;
            this.btnSaveDist.Click += new System.EventHandler(this.btnSaveDist_Click);
            // 
            // btnDistDown
            // 
            this.btnDistDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDistDown.BackColor = System.Drawing.Color.Transparent;
            this.btnDistDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDistDown.BackgroundImage")));
            this.btnDistDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDistDown.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnDistDown.CenterImage = null;
            this.btnDistDown.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnDistDown.HyperlinkStyle = false;
            this.btnDistDown.ImageMargin = 2;
            this.btnDistDown.LeftImage = null;
            this.btnDistDown.Location = new System.Drawing.Point(540, 2);
            this.btnDistDown.Name = "btnDistDown";
            this.btnDistDown.PushStyle = true;
            this.btnDistDown.RightImage = null;
            this.btnDistDown.Size = new System.Drawing.Size(28, 27);
            this.btnDistDown.TabIndex = 3;
            this.btnDistDown.Tag = "Distance|Down";
            this.btnDistDown.TextAlign = System.Drawing.StringAlignment.Far;
            this.btnDistDown.TextLeftMargin = 2;
            this.btnDistDown.TextRightMargin = 2;
            this.btnDistDown.Click += new System.EventHandler(this.upDownButton_Click);
            // 
            // btnRemoveDist
            // 
            this.btnRemoveDist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveDist.BackColor = System.Drawing.Color.Transparent;
            this.btnRemoveDist.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRemoveDist.BackgroundImage")));
            this.btnRemoveDist.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRemoveDist.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnRemoveDist.CenterImage = null;
            this.btnRemoveDist.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnRemoveDist.HyperlinkStyle = false;
            this.btnRemoveDist.ImageMargin = 2;
            this.btnRemoveDist.LeftImage = null;
            this.btnRemoveDist.Location = new System.Drawing.Point(472, 1);
            this.btnRemoveDist.Name = "btnRemoveDist";
            this.btnRemoveDist.PushStyle = true;
            this.btnRemoveDist.RightImage = null;
            this.btnRemoveDist.Size = new System.Drawing.Size(28, 28);
            this.btnRemoveDist.TabIndex = 1;
            this.btnRemoveDist.Tag = "Distance";
            this.btnRemoveDist.TextAlign = System.Drawing.StringAlignment.Far;
            this.btnRemoveDist.TextLeftMargin = 2;
            this.btnRemoveDist.TextRightMargin = 2;
            this.btnRemoveDist.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // txtDistDistance
            // 
            this.txtDistDistance.AcceptsReturn = false;
            this.txtDistDistance.AcceptsTab = false;
            this.txtDistDistance.BackColor = System.Drawing.Color.White;
            this.txtDistDistance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtDistDistance.ButtonImage = null;
            this.txtDistDistance.Location = new System.Drawing.Point(330, 36);
            this.txtDistDistance.MaxLength = 32767;
            this.txtDistDistance.Multiline = false;
            this.txtDistDistance.Name = "txtDistDistance";
            this.txtDistDistance.ReadOnly = false;
            this.txtDistDistance.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtDistDistance.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtDistDistance.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtDistDistance.Size = new System.Drawing.Size(67, 21);
            this.txtDistDistance.TabIndex = 5;
            this.txtDistDistance.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDistDistance.Leave += new System.EventHandler(this.txtDist_Leave);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(6, 39);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 19;
            this.lblName.Text = "Name";
            // 
            // btnDistUp
            // 
            this.btnDistUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDistUp.BackColor = System.Drawing.Color.Transparent;
            this.btnDistUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDistUp.BackgroundImage")));
            this.btnDistUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDistUp.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnDistUp.CenterImage = null;
            this.btnDistUp.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnDistUp.HyperlinkStyle = false;
            this.btnDistUp.ImageMargin = 2;
            this.btnDistUp.LeftImage = null;
            this.btnDistUp.Location = new System.Drawing.Point(506, 2);
            this.btnDistUp.Name = "btnDistUp";
            this.btnDistUp.PushStyle = true;
            this.btnDistUp.RightImage = null;
            this.btnDistUp.Size = new System.Drawing.Size(28, 27);
            this.btnDistUp.TabIndex = 2;
            this.btnDistUp.Tag = "Distance|Up";
            this.btnDistUp.TextAlign = System.Drawing.StringAlignment.Far;
            this.btnDistUp.TextLeftMargin = 2;
            this.btnDistUp.TextRightMargin = 2;
            this.btnDistUp.Click += new System.EventHandler(this.upDownButton_Click);
            // 
            // lblDistance
            // 
            this.lblDistance.Location = new System.Drawing.Point(250, 39);
            this.lblDistance.Name = "lblDistance";
            this.lblDistance.Size = new System.Drawing.Size(71, 13);
            this.lblDistance.TabIndex = 20;
            this.lblDistance.Text = "Distance";
            this.lblDistance.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtDistName
            // 
            this.txtDistName.AcceptsReturn = false;
            this.txtDistName.AcceptsTab = false;
            this.txtDistName.BackColor = System.Drawing.Color.White;
            this.txtDistName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtDistName.ButtonImage = null;
            this.txtDistName.Location = new System.Drawing.Point(47, 36);
            this.txtDistName.MaxLength = 32767;
            this.txtDistName.Multiline = false;
            this.txtDistName.Name = "txtDistName";
            this.txtDistName.ReadOnly = false;
            this.txtDistName.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtDistName.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtDistName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtDistName.Size = new System.Drawing.Size(200, 21);
            this.txtDistName.TabIndex = 4;
            this.txtDistName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDistName.Leave += new System.EventHandler(this.txtDist_Leave);
            // 
            // lblUnits
            // 
            this.lblUnits.Location = new System.Drawing.Point(403, 39);
            this.lblUnits.Name = "lblUnits";
            this.lblUnits.Size = new System.Drawing.Size(71, 13);
            this.lblUnits.TabIndex = 21;
            this.lblUnits.Text = "Unit";
            this.lblUnits.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // bnrDistance
            // 
            this.bnrDistance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bnrDistance.BackColor = System.Drawing.Color.Transparent;
            this.bnrDistance.HasMenuButton = false;
            this.bnrDistance.Location = new System.Drawing.Point(3, 3);
            this.bnrDistance.Name = "bnrDistance";
            this.bnrDistance.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bnrDistance.Size = new System.Drawing.Size(318, 24);
            this.bnrDistance.Style = ZoneFiveSoftware.Common.Visuals.ActionBanner.BannerStyle.Header2;
            this.bnrDistance.TabIndex = 9;
            this.bnrDistance.Text = "Distance Categories";
            this.bnrDistance.UseStyleFont = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.treeExt);
            this.splitContainer2.Panel1.Controls.Add(this.pnlExtHeader);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(571, 561);
            this.splitContainer2.SplitterDistance = 227;
            this.splitContainer2.TabIndex = 13;
            // 
            // treeExt
            // 
            this.treeExt.BackColor = System.Drawing.Color.Transparent;
            this.treeExt.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.treeExt.CheckBoxes = false;
            this.treeExt.DefaultIndent = 15;
            this.treeExt.DefaultRowHeight = -1;
            this.treeExt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeExt.HeaderRowHeight = 21;
            this.treeExt.Location = new System.Drawing.Point(0, 61);
            this.treeExt.MultiSelect = false;
            this.treeExt.Name = "treeExt";
            this.treeExt.NumHeaderRows = ZoneFiveSoftware.Common.Visuals.TreeList.HeaderRows.Auto;
            this.treeExt.NumLockedColumns = 0;
            this.treeExt.RowAlternatingColors = true;
            this.treeExt.RowHotlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(147)))), ((int)(((byte)(160)))), ((int)(((byte)(112)))));
            this.treeExt.RowHotlightColorText = System.Drawing.SystemColors.HighlightText;
            this.treeExt.RowHotlightMouse = true;
            this.treeExt.RowSelectedColor = System.Drawing.SystemColors.Highlight;
            this.treeExt.RowSelectedColorText = System.Drawing.SystemColors.HighlightText;
            this.treeExt.RowSeparatorLines = true;
            this.treeExt.ShowLines = false;
            this.treeExt.ShowPlusMinus = false;
            this.treeExt.Size = new System.Drawing.Size(571, 166);
            this.treeExt.TabIndex = 0;
            this.treeExt.SelectedItemsChanged += new System.EventHandler(this.treeExt_SelectionChanged);
            // 
            // pnlExtHeader
            // 
            this.pnlExtHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlExtHeader.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.None;
            this.pnlExtHeader.BorderColor = System.Drawing.Color.Gray;
            this.pnlExtHeader.Controls.Add(this.txtExtType);
            this.pnlExtHeader.Controls.Add(this.lblName2);
            this.pnlExtHeader.Controls.Add(this.extremes_down_button);
            this.pnlExtHeader.Controls.Add(this.btnResetExtreme);
            this.pnlExtHeader.Controls.Add(this.btnSaveExt);
            this.pnlExtHeader.Controls.Add(this.extremes_up_button);
            this.pnlExtHeader.Controls.Add(this.lblType2);
            this.pnlExtHeader.Controls.Add(this.txtExtName);
            this.pnlExtHeader.Controls.Add(this.remove_extreme_button);
            this.pnlExtHeader.Controls.Add(this.bnrExtreme);
            this.pnlExtHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlExtHeader.HeadingBackColor = System.Drawing.Color.LightBlue;
            this.pnlExtHeader.HeadingFont = null;
            this.pnlExtHeader.HeadingLeftMargin = 0;
            this.pnlExtHeader.HeadingText = null;
            this.pnlExtHeader.HeadingTextColor = System.Drawing.Color.Black;
            this.pnlExtHeader.HeadingTopMargin = 3;
            this.pnlExtHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlExtHeader.Name = "pnlExtHeader";
            this.pnlExtHeader.Size = new System.Drawing.Size(571, 61);
            this.pnlExtHeader.TabIndex = 22;
            // 
            // txtExtType
            // 
            this.txtExtType.AcceptsReturn = false;
            this.txtExtType.AcceptsTab = false;
            this.txtExtType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExtType.BackColor = System.Drawing.Color.White;
            this.txtExtType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtExtType.ButtonImage = ((System.Drawing.Image)(resources.GetObject("txtExtType.ButtonImage")));
            this.txtExtType.Location = new System.Drawing.Point(327, 36);
            this.txtExtType.MaxLength = 32767;
            this.txtExtType.Multiline = false;
            this.txtExtType.Name = "txtExtType";
            this.txtExtType.ReadOnly = true;
            this.txtExtType.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtExtType.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtExtType.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtExtType.Size = new System.Drawing.Size(241, 21);
            this.txtExtType.TabIndex = 7;
            this.txtExtType.Tag = "ExtType";
            this.txtExtType.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtExtType.ButtonClick += new System.EventHandler(this.txtType_ButtonClick);
            // 
            // lblName2
            // 
            this.lblName2.AutoSize = true;
            this.lblName2.Location = new System.Drawing.Point(3, 39);
            this.lblName2.Name = "lblName2";
            this.lblName2.Size = new System.Drawing.Size(35, 13);
            this.lblName2.TabIndex = 19;
            this.lblName2.Text = "Name";
            // 
            // extremes_down_button
            // 
            this.extremes_down_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.extremes_down_button.BackColor = System.Drawing.Color.Transparent;
            this.extremes_down_button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("extremes_down_button.BackgroundImage")));
            this.extremes_down_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.extremes_down_button.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.extremes_down_button.CenterImage = null;
            this.extremes_down_button.DialogResult = System.Windows.Forms.DialogResult.None;
            this.extremes_down_button.HyperlinkStyle = false;
            this.extremes_down_button.ImageMargin = 2;
            this.extremes_down_button.LeftImage = null;
            this.extremes_down_button.Location = new System.Drawing.Point(540, 2);
            this.extremes_down_button.Name = "extremes_down_button";
            this.extremes_down_button.PushStyle = true;
            this.extremes_down_button.RightImage = null;
            this.extremes_down_button.Size = new System.Drawing.Size(28, 27);
            this.extremes_down_button.TabIndex = 5;
            this.extremes_down_button.Tag = "Extreme|Down";
            this.extremes_down_button.TextAlign = System.Drawing.StringAlignment.Far;
            this.extremes_down_button.TextLeftMargin = 2;
            this.extremes_down_button.TextRightMargin = 2;
            this.extremes_down_button.Click += new System.EventHandler(this.upDownButton_Click);
            // 
            // btnResetExtreme
            // 
            this.btnResetExtreme.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetExtreme.BackColor = System.Drawing.Color.Transparent;
            this.btnResetExtreme.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnResetExtreme.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnResetExtreme.CenterImage = null;
            this.btnResetExtreme.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnResetExtreme.HyperlinkStyle = false;
            this.btnResetExtreme.ImageMargin = 2;
            this.btnResetExtreme.LeftImage = ((System.Drawing.Image)(resources.GetObject("btnResetExtreme.LeftImage")));
            this.btnResetExtreme.Location = new System.Drawing.Point(364, 2);
            this.btnResetExtreme.Name = "btnResetExtreme";
            this.btnResetExtreme.PushStyle = true;
            this.btnResetExtreme.RightImage = null;
            this.btnResetExtreme.Size = new System.Drawing.Size(70, 27);
            this.btnResetExtreme.TabIndex = 1;
            this.btnResetExtreme.Tag = "Extreme";
            this.btnResetExtreme.Text = "Reset";
            this.btnResetExtreme.TextAlign = System.Drawing.StringAlignment.Near;
            this.btnResetExtreme.TextLeftMargin = 2;
            this.btnResetExtreme.TextRightMargin = 2;
            this.btnResetExtreme.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSaveExt
            // 
            this.btnSaveExt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveExt.BackColor = System.Drawing.Color.Transparent;
            this.btnSaveExt.BackgroundImage = global::RecordBook.Resources.Images.add;
            this.btnSaveExt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSaveExt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnSaveExt.CenterImage = null;
            this.btnSaveExt.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSaveExt.HyperlinkStyle = false;
            this.btnSaveExt.ImageMargin = 2;
            this.btnSaveExt.LeftImage = null;
            this.btnSaveExt.Location = new System.Drawing.Point(438, 2);
            this.btnSaveExt.Name = "btnSaveExt";
            this.btnSaveExt.PushStyle = true;
            this.btnSaveExt.RightImage = null;
            this.btnSaveExt.Size = new System.Drawing.Size(28, 27);
            this.btnSaveExt.TabIndex = 1;
            this.btnSaveExt.TextAlign = System.Drawing.StringAlignment.Far;
            this.btnSaveExt.TextLeftMargin = 2;
            this.btnSaveExt.TextRightMargin = 2;
            this.btnSaveExt.Click += new System.EventHandler(this.btnSaveExt_Click);
            // 
            // extremes_up_button
            // 
            this.extremes_up_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.extremes_up_button.BackColor = System.Drawing.Color.Transparent;
            this.extremes_up_button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("extremes_up_button.BackgroundImage")));
            this.extremes_up_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.extremes_up_button.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.extremes_up_button.CenterImage = null;
            this.extremes_up_button.DialogResult = System.Windows.Forms.DialogResult.None;
            this.extremes_up_button.HyperlinkStyle = false;
            this.extremes_up_button.ImageMargin = 2;
            this.extremes_up_button.LeftImage = null;
            this.extremes_up_button.Location = new System.Drawing.Point(506, 2);
            this.extremes_up_button.Name = "extremes_up_button";
            this.extremes_up_button.PushStyle = true;
            this.extremes_up_button.RightImage = null;
            this.extremes_up_button.Size = new System.Drawing.Size(28, 27);
            this.extremes_up_button.TabIndex = 3;
            this.extremes_up_button.Tag = "Extreme|Up";
            this.extremes_up_button.TextAlign = System.Drawing.StringAlignment.Far;
            this.extremes_up_button.TextLeftMargin = 2;
            this.extremes_up_button.TextRightMargin = 2;
            this.extremes_up_button.Click += new System.EventHandler(this.upDownButton_Click);
            // 
            // lblType2
            // 
            this.lblType2.Location = new System.Drawing.Point(250, 39);
            this.lblType2.Name = "lblType2";
            this.lblType2.Size = new System.Drawing.Size(71, 13);
            this.lblType2.TabIndex = 21;
            this.lblType2.Text = "Type";
            this.lblType2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtExtName
            // 
            this.txtExtName.AcceptsReturn = false;
            this.txtExtName.AcceptsTab = false;
            this.txtExtName.BackColor = System.Drawing.Color.White;
            this.txtExtName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtExtName.ButtonImage = null;
            this.txtExtName.Location = new System.Drawing.Point(44, 36);
            this.txtExtName.MaxLength = 32767;
            this.txtExtName.Multiline = false;
            this.txtExtName.Name = "txtExtName";
            this.txtExtName.ReadOnly = false;
            this.txtExtName.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtExtName.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtExtName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtExtName.Size = new System.Drawing.Size(200, 21);
            this.txtExtName.TabIndex = 6;
            this.txtExtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtExtName.Leave += new System.EventHandler(this.txtExt_Leave);
            // 
            // remove_extreme_button
            // 
            this.remove_extreme_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.remove_extreme_button.BackColor = System.Drawing.Color.Transparent;
            this.remove_extreme_button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("remove_extreme_button.BackgroundImage")));
            this.remove_extreme_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.remove_extreme_button.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.remove_extreme_button.CenterImage = null;
            this.remove_extreme_button.DialogResult = System.Windows.Forms.DialogResult.None;
            this.remove_extreme_button.HyperlinkStyle = false;
            this.remove_extreme_button.ImageMargin = 2;
            this.remove_extreme_button.LeftImage = null;
            this.remove_extreme_button.Location = new System.Drawing.Point(472, 1);
            this.remove_extreme_button.Name = "remove_extreme_button";
            this.remove_extreme_button.PushStyle = true;
            this.remove_extreme_button.RightImage = null;
            this.remove_extreme_button.Size = new System.Drawing.Size(28, 28);
            this.remove_extreme_button.TabIndex = 2;
            this.remove_extreme_button.Tag = "Extreme";
            this.remove_extreme_button.TextAlign = System.Drawing.StringAlignment.Far;
            this.remove_extreme_button.TextLeftMargin = 2;
            this.remove_extreme_button.TextRightMargin = 2;
            this.remove_extreme_button.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // bnrExtreme
            // 
            this.bnrExtreme.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bnrExtreme.BackColor = System.Drawing.Color.Transparent;
            this.bnrExtreme.HasMenuButton = false;
            this.bnrExtreme.Location = new System.Drawing.Point(3, 3);
            this.bnrExtreme.Name = "bnrExtreme";
            this.bnrExtreme.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bnrExtreme.Size = new System.Drawing.Size(315, 24);
            this.bnrExtreme.Style = ZoneFiveSoftware.Common.Visuals.ActionBanner.BannerStyle.Header2;
            this.bnrExtreme.TabIndex = 14;
            this.bnrExtreme.Text = "My Extreme Categories";
            this.bnrExtreme.UseStyleFont = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.IsSplitterFixed = true;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.treeNow);
            this.splitContainer3.Panel1.Controls.Add(this.pnlNowHeader);
            this.splitContainer3.Panel2Collapsed = true;
            this.splitContainer3.Size = new System.Drawing.Size(571, 330);
            this.splitContainer3.SplitterDistance = 277;
            this.splitContainer3.TabIndex = 0;
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
            this.treeNow.Location = new System.Drawing.Point(0, 61);
            this.treeNow.MultiSelect = false;
            this.treeNow.Name = "treeNow";
            this.treeNow.NumHeaderRows = ZoneFiveSoftware.Common.Visuals.TreeList.HeaderRows.Auto;
            this.treeNow.NumLockedColumns = 0;
            this.treeNow.RowAlternatingColors = true;
            this.treeNow.RowHotlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(147)))), ((int)(((byte)(160)))), ((int)(((byte)(112)))));
            this.treeNow.RowHotlightColorText = System.Drawing.SystemColors.HighlightText;
            this.treeNow.RowHotlightMouse = true;
            this.treeNow.RowSelectedColor = System.Drawing.SystemColors.Highlight;
            this.treeNow.RowSelectedColorText = System.Drawing.SystemColors.HighlightText;
            this.treeNow.RowSeparatorLines = true;
            this.treeNow.ShowLines = false;
            this.treeNow.ShowPlusMinus = false;
            this.treeNow.Size = new System.Drawing.Size(571, 269);
            this.treeNow.TabIndex = 0;
            this.treeNow.SelectedItemsChanged += new System.EventHandler(this.treeNow_SelectionChanged);
            // 
            // pnlNowHeader
            // 
            this.pnlNowHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlNowHeader.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.None;
            this.pnlNowHeader.BorderColor = System.Drawing.Color.Gray;
            this.pnlNowHeader.Controls.Add(this.txtNowType);
            this.pnlNowHeader.Controls.Add(this.lblName3);
            this.pnlNowHeader.Controls.Add(this.btnResetRank);
            this.pnlNowHeader.Controls.Add(this.btnSaveNowThen);
            this.pnlNowHeader.Controls.Add(this.remove_nowthen_button);
            this.pnlNowHeader.Controls.Add(this.nowThen_down_button);
            this.pnlNowHeader.Controls.Add(this.lblType3);
            this.pnlNowHeader.Controls.Add(this.nowThen_up_button);
            this.pnlNowHeader.Controls.Add(this.txtNowName);
            this.pnlNowHeader.Controls.Add(this.bnrNowThen);
            this.pnlNowHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNowHeader.HeadingBackColor = System.Drawing.Color.LightBlue;
            this.pnlNowHeader.HeadingFont = null;
            this.pnlNowHeader.HeadingLeftMargin = 0;
            this.pnlNowHeader.HeadingText = null;
            this.pnlNowHeader.HeadingTextColor = System.Drawing.Color.Black;
            this.pnlNowHeader.HeadingTopMargin = 3;
            this.pnlNowHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlNowHeader.Name = "pnlNowHeader";
            this.pnlNowHeader.Size = new System.Drawing.Size(571, 61);
            this.pnlNowHeader.TabIndex = 22;
            // 
            // txtNowType
            // 
            this.txtNowType.AcceptsReturn = false;
            this.txtNowType.AcceptsTab = false;
            this.txtNowType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNowType.BackColor = System.Drawing.Color.White;
            this.txtNowType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtNowType.ButtonImage = global::RecordBook.Properties.Resources.comboDown;
            this.txtNowType.Location = new System.Drawing.Point(327, 36);
            this.txtNowType.MaxLength = 32767;
            this.txtNowType.Multiline = false;
            this.txtNowType.Name = "txtNowType";
            this.txtNowType.ReadOnly = true;
            this.txtNowType.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtNowType.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtNowType.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtNowType.Size = new System.Drawing.Size(241, 21);
            this.txtNowType.TabIndex = 5;
            this.txtNowType.Tag = "NowType";
            this.txtNowType.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtNowType.ButtonClick += new System.EventHandler(this.txtType_ButtonClick);
            // 
            // lblName3
            // 
            this.lblName3.AutoSize = true;
            this.lblName3.Location = new System.Drawing.Point(3, 39);
            this.lblName3.Name = "lblName3";
            this.lblName3.Size = new System.Drawing.Size(35, 13);
            this.lblName3.TabIndex = 19;
            this.lblName3.Text = "Name";
            // 
            // btnResetRank
            // 
            this.btnResetRank.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetRank.BackColor = System.Drawing.Color.Transparent;
            this.btnResetRank.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnResetRank.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnResetRank.CenterImage = null;
            this.btnResetRank.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnResetRank.HyperlinkStyle = false;
            this.btnResetRank.ImageMargin = 2;
            this.btnResetRank.LeftImage = global::RecordBook.Properties.Resources.reset;
            this.btnResetRank.Location = new System.Drawing.Point(364, 2);
            this.btnResetRank.Name = "btnResetRank";
            this.btnResetRank.PushStyle = true;
            this.btnResetRank.RightImage = null;
            this.btnResetRank.Size = new System.Drawing.Size(70, 27);
            this.btnResetRank.TabIndex = 0;
            this.btnResetRank.Tag = "Rank";
            this.btnResetRank.Text = "Reset";
            this.btnResetRank.TextAlign = System.Drawing.StringAlignment.Near;
            this.btnResetRank.TextLeftMargin = 2;
            this.btnResetRank.TextRightMargin = 2;
            this.btnResetRank.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSaveNowThen
            // 
            this.btnSaveNowThen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveNowThen.BackColor = System.Drawing.Color.Transparent;
            this.btnSaveNowThen.BackgroundImage = global::RecordBook.Resources.Images.add;
            this.btnSaveNowThen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSaveNowThen.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnSaveNowThen.CenterImage = null;
            this.btnSaveNowThen.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSaveNowThen.HyperlinkStyle = false;
            this.btnSaveNowThen.ImageMargin = 2;
            this.btnSaveNowThen.LeftImage = null;
            this.btnSaveNowThen.Location = new System.Drawing.Point(438, 2);
            this.btnSaveNowThen.Name = "btnSaveNowThen";
            this.btnSaveNowThen.PushStyle = true;
            this.btnSaveNowThen.RightImage = null;
            this.btnSaveNowThen.Size = new System.Drawing.Size(28, 27);
            this.btnSaveNowThen.TabIndex = 0;
            this.btnSaveNowThen.TextAlign = System.Drawing.StringAlignment.Far;
            this.btnSaveNowThen.TextLeftMargin = 2;
            this.btnSaveNowThen.TextRightMargin = 2;
            this.btnSaveNowThen.Click += new System.EventHandler(this.btnSaveNowThen_Click);
            // 
            // remove_nowthen_button
            // 
            this.remove_nowthen_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.remove_nowthen_button.BackColor = System.Drawing.Color.Transparent;
            this.remove_nowthen_button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("remove_nowthen_button.BackgroundImage")));
            this.remove_nowthen_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.remove_nowthen_button.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.remove_nowthen_button.CenterImage = null;
            this.remove_nowthen_button.DialogResult = System.Windows.Forms.DialogResult.None;
            this.remove_nowthen_button.HyperlinkStyle = false;
            this.remove_nowthen_button.ImageMargin = 2;
            this.remove_nowthen_button.LeftImage = null;
            this.remove_nowthen_button.Location = new System.Drawing.Point(472, 1);
            this.remove_nowthen_button.Name = "remove_nowthen_button";
            this.remove_nowthen_button.PushStyle = true;
            this.remove_nowthen_button.RightImage = null;
            this.remove_nowthen_button.Size = new System.Drawing.Size(28, 28);
            this.remove_nowthen_button.TabIndex = 1;
            this.remove_nowthen_button.Tag = "NowThen";
            this.remove_nowthen_button.TextAlign = System.Drawing.StringAlignment.Far;
            this.remove_nowthen_button.TextLeftMargin = 2;
            this.remove_nowthen_button.TextRightMargin = 2;
            this.remove_nowthen_button.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // nowThen_down_button
            // 
            this.nowThen_down_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nowThen_down_button.BackColor = System.Drawing.Color.Transparent;
            this.nowThen_down_button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("nowThen_down_button.BackgroundImage")));
            this.nowThen_down_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.nowThen_down_button.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.nowThen_down_button.CenterImage = null;
            this.nowThen_down_button.DialogResult = System.Windows.Forms.DialogResult.None;
            this.nowThen_down_button.HyperlinkStyle = false;
            this.nowThen_down_button.ImageMargin = 2;
            this.nowThen_down_button.LeftImage = null;
            this.nowThen_down_button.Location = new System.Drawing.Point(540, 2);
            this.nowThen_down_button.Name = "nowThen_down_button";
            this.nowThen_down_button.PushStyle = true;
            this.nowThen_down_button.RightImage = null;
            this.nowThen_down_button.Size = new System.Drawing.Size(28, 27);
            this.nowThen_down_button.TabIndex = 3;
            this.nowThen_down_button.Tag = "NowThen|Down";
            this.nowThen_down_button.TextAlign = System.Drawing.StringAlignment.Far;
            this.nowThen_down_button.TextLeftMargin = 2;
            this.nowThen_down_button.TextRightMargin = 2;
            this.nowThen_down_button.Click += new System.EventHandler(this.upDownButton_Click);
            // 
            // lblType3
            // 
            this.lblType3.Location = new System.Drawing.Point(250, 39);
            this.lblType3.Name = "lblType3";
            this.lblType3.Size = new System.Drawing.Size(71, 13);
            this.lblType3.TabIndex = 21;
            this.lblType3.Text = "Type";
            this.lblType3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // nowThen_up_button
            // 
            this.nowThen_up_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nowThen_up_button.BackColor = System.Drawing.Color.Transparent;
            this.nowThen_up_button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("nowThen_up_button.BackgroundImage")));
            this.nowThen_up_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.nowThen_up_button.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.nowThen_up_button.CenterImage = null;
            this.nowThen_up_button.DialogResult = System.Windows.Forms.DialogResult.None;
            this.nowThen_up_button.HyperlinkStyle = false;
            this.nowThen_up_button.ImageMargin = 2;
            this.nowThen_up_button.LeftImage = null;
            this.nowThen_up_button.Location = new System.Drawing.Point(506, 2);
            this.nowThen_up_button.Name = "nowThen_up_button";
            this.nowThen_up_button.PushStyle = true;
            this.nowThen_up_button.RightImage = null;
            this.nowThen_up_button.Size = new System.Drawing.Size(28, 27);
            this.nowThen_up_button.TabIndex = 2;
            this.nowThen_up_button.Tag = "NowThen|Up";
            this.nowThen_up_button.TextAlign = System.Drawing.StringAlignment.Far;
            this.nowThen_up_button.TextLeftMargin = 2;
            this.nowThen_up_button.TextRightMargin = 2;
            this.nowThen_up_button.Click += new System.EventHandler(this.upDownButton_Click);
            // 
            // txtNowName
            // 
            this.txtNowName.AcceptsReturn = false;
            this.txtNowName.AcceptsTab = false;
            this.txtNowName.BackColor = System.Drawing.Color.White;
            this.txtNowName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtNowName.ButtonImage = null;
            this.txtNowName.Location = new System.Drawing.Point(44, 36);
            this.txtNowName.MaxLength = 32767;
            this.txtNowName.Multiline = false;
            this.txtNowName.Name = "txtNowName";
            this.txtNowName.ReadOnly = false;
            this.txtNowName.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtNowName.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtNowName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtNowName.Size = new System.Drawing.Size(200, 21);
            this.txtNowName.TabIndex = 4;
            this.txtNowName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtNowName.Leave += new System.EventHandler(this.txtNow_Leave);
            // 
            // bnrNowThen
            // 
            this.bnrNowThen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bnrNowThen.BackColor = System.Drawing.Color.Transparent;
            this.bnrNowThen.HasMenuButton = false;
            this.bnrNowThen.Location = new System.Drawing.Point(3, 3);
            this.bnrNowThen.Name = "bnrNowThen";
            this.bnrNowThen.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bnrNowThen.Size = new System.Drawing.Size(315, 24);
            this.bnrNowThen.Style = ZoneFiveSoftware.Common.Visuals.ActionBanner.BannerStyle.Header2;
            this.bnrNowThen.TabIndex = 15;
            this.bnrNowThen.Text = "Now/Then Categories";
            this.bnrNowThen.UseStyleFont = true;
            // 
            // splitVertical
            // 
            this.splitVertical.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitVertical.Location = new System.Drawing.Point(0, 0);
            this.splitVertical.Name = "splitVertical";
            // 
            // splitVertical.Panel1
            // 
            this.splitVertical.Panel1.Controls.Add(this.treeCategory);
            this.splitVertical.Panel1.Controls.Add(this.bnrCategories);
            // 
            // splitVertical.Panel2
            // 
            this.splitVertical.Panel2.Controls.Add(this.splitContainer1);
            this.splitVertical.Size = new System.Drawing.Size(862, 771);
            this.splitVertical.SplitterDistance = 287;
            this.splitVertical.TabIndex = 22;
            // 
            // treeCategory
            // 
            this.treeCategory.BackColor = System.Drawing.Color.Transparent;
            this.treeCategory.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.treeCategory.CheckBoxes = true;
            this.treeCategory.DefaultIndent = 15;
            this.treeCategory.DefaultRowHeight = -1;
            this.treeCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeCategory.HeaderRowHeight = 21;
            this.treeCategory.Location = new System.Drawing.Point(0, 27);
            this.treeCategory.MultiSelect = false;
            this.treeCategory.Name = "treeCategory";
            this.treeCategory.NumHeaderRows = ZoneFiveSoftware.Common.Visuals.TreeList.HeaderRows.None;
            this.treeCategory.NumLockedColumns = 0;
            this.treeCategory.RowAlternatingColors = true;
            this.treeCategory.RowHotlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(147)))), ((int)(((byte)(160)))), ((int)(((byte)(112)))));
            this.treeCategory.RowHotlightColorText = System.Drawing.SystemColors.HighlightText;
            this.treeCategory.RowHotlightMouse = true;
            this.treeCategory.RowSelectedColor = System.Drawing.SystemColors.Highlight;
            this.treeCategory.RowSelectedColorText = System.Drawing.SystemColors.HighlightText;
            this.treeCategory.RowSeparatorLines = true;
            this.treeCategory.ShowLines = false;
            this.treeCategory.ShowPlusMinus = false;
            this.treeCategory.Size = new System.Drawing.Size(287, 744);
            this.treeCategory.TabIndex = 0;
            this.treeCategory.CheckedChanged += new ZoneFiveSoftware.Common.Visuals.TreeList.ItemEventHandler(this.treeCategory_CheckedChanged);
            // 
            // bnrCategories
            // 
            this.bnrCategories.BackColor = System.Drawing.Color.Transparent;
            this.bnrCategories.Dock = System.Windows.Forms.DockStyle.Top;
            this.bnrCategories.HasMenuButton = false;
            this.bnrCategories.Location = new System.Drawing.Point(0, 0);
            this.bnrCategories.Name = "bnrCategories";
            this.bnrCategories.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bnrCategories.Size = new System.Drawing.Size(287, 27);
            this.bnrCategories.Style = ZoneFiveSoftware.Common.Visuals.ActionBanner.BannerStyle.Header2;
            this.bnrCategories.TabIndex = 9;
            this.bnrCategories.Text = "Category";
            this.bnrCategories.UseStyleFont = true;
            // 
            // SettingsPageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitVertical);
            this.Name = "SettingsPageControl";
            this.Size = new System.Drawing.Size(862, 771);
            this.Load += new System.EventHandler(this.RefreshSettingsPageControl);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.pnlDistHeader.ResumeLayout(false);
            this.pnlDistHeader.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.pnlExtHeader.ResumeLayout(false);
            this.pnlExtHeader.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.pnlNowHeader.ResumeLayout(false);
            this.pnlNowHeader.PerformLayout();
            this.splitVertical.Panel1.ResumeLayout(false);
            this.splitVertical.Panel2.ResumeLayout(false);
            this.splitVertical.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private ZoneFiveSoftware.Common.Visuals.ActionBanner bnrDistance;
        private ZoneFiveSoftware.Common.Visuals.Button btnRemoveDist;
        private ZoneFiveSoftware.Common.Visuals.Button btnSaveDist;
        private ZoneFiveSoftware.Common.Visuals.TreeList treeDistance;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private ZoneFiveSoftware.Common.Visuals.Button remove_extreme_button;
        private ZoneFiveSoftware.Common.Visuals.Button btnSaveExt;
        private ZoneFiveSoftware.Common.Visuals.ActionBanner bnrExtreme;
        private ZoneFiveSoftware.Common.Visuals.TreeList treeExt;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private ZoneFiveSoftware.Common.Visuals.TreeList treeNow;
        private ZoneFiveSoftware.Common.Visuals.Button remove_nowthen_button;
        private ZoneFiveSoftware.Common.Visuals.Button btnSaveNowThen;
        private ZoneFiveSoftware.Common.Visuals.ActionBanner bnrNowThen;
        private ZoneFiveSoftware.Common.Visuals.Button nowThen_down_button;
        private ZoneFiveSoftware.Common.Visuals.Button nowThen_up_button;
        private ZoneFiveSoftware.Common.Visuals.Button extremes_down_button;
        private ZoneFiveSoftware.Common.Visuals.Button extremes_up_button;
        private System.Windows.Forms.SplitContainer splitVertical;
        private ZoneFiveSoftware.Common.Visuals.TreeList treeCategory;
        private ZoneFiveSoftware.Common.Visuals.ActionBanner bnrCategories;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtDistDistance;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtDistName;
        private System.Windows.Forms.Label lblUnits;
        private System.Windows.Forms.Label lblDistance;
        private System.Windows.Forms.Label lblName;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtExtName;
        private System.Windows.Forms.Label lblType2;
        private System.Windows.Forms.Label lblName2;
        private System.Windows.Forms.Label lblName3;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtNowName;
        private System.Windows.Forms.Label lblType3;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtNowType;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtExtType;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtDistUnit;
        private ZoneFiveSoftware.Common.Visuals.Button btnDistDown;
        private ZoneFiveSoftware.Common.Visuals.Button btnDistUp;
        private ZoneFiveSoftware.Common.Visuals.Panel pnlDistHeader;
        private ZoneFiveSoftware.Common.Visuals.Panel pnlExtHeader;
        private ZoneFiveSoftware.Common.Visuals.Panel pnlNowHeader;
        private ZoneFiveSoftware.Common.Visuals.Button btnResetDistance;
        private ZoneFiveSoftware.Common.Visuals.Button btnResetExtreme;
        private ZoneFiveSoftware.Common.Visuals.Button btnResetRank;
    }
}
