namespace RecordBook.UI
{
    partial class RecordPaneControl
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
            this.treeActivity = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.actionBanner1 = new ZoneFiveSoftware.Common.Visuals.ActionBanner();
            this.SuspendLayout();
            // 
            // treeActivity
            // 
            this.treeActivity.BackColor = System.Drawing.Color.Transparent;
            this.treeActivity.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.treeActivity.CheckBoxes = false;
            this.treeActivity.DefaultIndent = 15;
            this.treeActivity.DefaultRowHeight = -1;
            this.treeActivity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeActivity.HeaderRowHeight = 21;
            this.treeActivity.Location = new System.Drawing.Point(0, 25);
            this.treeActivity.MultiSelect = false;
            this.treeActivity.Name = "treeActivity";
            this.treeActivity.NumHeaderRows = ZoneFiveSoftware.Common.Visuals.TreeList.HeaderRows.Auto;
            this.treeActivity.NumLockedColumns = 0;
            this.treeActivity.RowAlternatingColors = true;
            this.treeActivity.RowHotlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(10)))), ((int)(((byte)(36)))), ((int)(((byte)(106)))));
            this.treeActivity.RowHotlightColorText = System.Drawing.SystemColors.HighlightText;
            this.treeActivity.RowHotlightMouse = true;
            this.treeActivity.RowSelectedColor = System.Drawing.SystemColors.Highlight;
            this.treeActivity.RowSelectedColorText = System.Drawing.SystemColors.HighlightText;
            this.treeActivity.RowSeparatorLines = true;
            this.treeActivity.ShowLines = false;
            this.treeActivity.ShowPlusMinus = false;
            this.treeActivity.Size = new System.Drawing.Size(420, 464);
            this.treeActivity.TabIndex = 0;
            // 
            // actionBanner1
            // 
            this.actionBanner1.BackColor = System.Drawing.Color.Transparent;
            this.actionBanner1.Dock = System.Windows.Forms.DockStyle.Top;
            this.actionBanner1.HasMenuButton = false;
            this.actionBanner1.Location = new System.Drawing.Point(0, 0);
            this.actionBanner1.Name = "actionBanner1";
            this.actionBanner1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.actionBanner1.Size = new System.Drawing.Size(420, 25);
            this.actionBanner1.Style = ZoneFiveSoftware.Common.Visuals.ActionBanner.BannerStyle.Header2;
            this.actionBanner1.TabIndex = 1;
            this.actionBanner1.Text = "actionBanner1";
            this.actionBanner1.UseStyleFont = true;
            // 
            // RecordPaneControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeActivity);
            this.Controls.Add(this.actionBanner1);
            this.Name = "RecordPaneControl";
            this.Size = new System.Drawing.Size(420, 489);
            this.ResumeLayout(false);

        }

        #endregion

        private ZoneFiveSoftware.Common.Visuals.TreeList treeActivity;
        private ZoneFiveSoftware.Common.Visuals.ActionBanner actionBanner1;
    }
}
