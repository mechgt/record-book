// <copyright file="ImageSaveDialog.cs" company="N/A">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>mechgt</author>
// <email>mechgt@gmail.com</email>
// <date>2008-12-23</date>
namespace RecordBook.UI.View
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Windows.Forms;
    using ZoneFiveSoftware.Common.Visuals;
    using ZoneFiveSoftware.Common.Visuals.Chart;

    public partial class ImageSaveDialog : Form
    {
        #region Fields

        private DirectoryTreePopup dirTree = new DirectoryTreePopup();
        private ChartBase chart;
        private TreeListPopup popup = new TreeListPopup();
        private List<string> types = new List<string>();
        private List<string> sizes = new List<string>();
        private bool size;

        #endregion

        public ImageSaveDialog(ITheme visualTheme, ChartBase chart)
        {
            InitializeComponent();

            // Setup some cosmetic items
            this.Icon = Icon.FromHandle(((Bitmap)ZoneFiveSoftware.Common.Visuals.CommonResources.Images.Save16).GetHicon());
            btnDirTree.BackgroundImage = ZoneFiveSoftware.Common.Visuals.CommonResources.Images.Folder16;
            btnFolderUp.BackgroundImage = ZoneFiveSoftware.Common.Visuals.CommonResources.Images.FolderUp16;
            btnSizeOpen.CenterImage = ZoneFiveSoftware.Common.Visuals.CommonResources.Images.MoveDown16;
            // TODO: (LOW) Trying to draw the proper combobox button here.
            // ControlPaint.DrawComboButton(Graphics.FromImage(btnSizeOpen.CenterImage), btnSizeOpen.ClientRectangle, ButtonState.Normal);
            btnTypeOpen.CenterImage = ZoneFiveSoftware.Common.Visuals.CommonResources.Images.MoveDown16;
            ThemeChanged(visualTheme);

            // Default Save Location: My Pictures
            string mypics = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures).ToString();
            txtDirectory.Text = mypics;

            // Default Filename
            txtFilename.Text = "Record Book " + DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);

            // Default Size = Medium
            sizes.Add("Thumbnail (200 x 120)");
            sizes.Add("Small (400 x 240)");
            sizes.Add("Medium (800 x 480)");
            sizes.Add("Large (1400 x 840)");
            sizes.Add("Huge (2000 x 1200)");
            txtSize.Text = "Medium (800 x 480)";

            // Default Type = PNG
            types.Add("Bitmap (.bmp)");
            types.Add("Joint Photographic Experts Group (.jpg)");
            types.Add("Portable Network Graphics (.png)");
            types.Add("Tagged Image Format (.tif)");
            txtType.Text = "Portable Network Graphics (.png)";

            // Store chart
            this.chart = chart;
        }

        public void ThemeChanged(ITheme visualTheme)
        {
            backPanel.ThemeChanged(visualTheme);
            bottomPanel.ThemeChanged(visualTheme);
            txtDirectory.ThemeChanged(visualTheme);
            txtFilename.ThemeChanged(visualTheme);
            popup.ThemeChanged(visualTheme);
            dirTree.ThemeChanged(visualTheme);

            backPanel.BackColor = visualTheme.Control;
            //// unselectedList.BackColor = visualTheme.Window;
            //// splitContainer1.Panel1.BackColor = visualTheme.Control;
            //// splitContainer1.Panel2.BackColor = visualTheme.Control;
            bottomPanel.BackColor = visualTheme.Control;
            //// splitContainer1.BackColor = visualTheme.Control;
            bottomPanel.BackColor = visualTheme.Window;
            bottomPanel.RightGradientColor = Color.FromArgb(30, Color.Black);
            //// chartsLabel.ForeColor = visualTheme.ControlText;
            //// availableLabel.ForeColor = visualTheme.ControlText;
        }

        private void btnDirTree_Click(object sender, EventArgs e)
        {
            // Display a DirectoryTreePopup
            if (Directory.Exists(txtDirectory.Text))
            {
                dirTree.Tree.SelectedPath = txtDirectory.Text;
            }

            // Define size and location
            dirTree.Location = txtDirectory.Location;

            // Show the directory tree
            Rectangle rect = new Rectangle(txtDirectory.Location, txtDirectory.Size);
            rect.Offset(this.Location);
            rect.Offset(4, txtDirectory.Height + 4);

            // Add event handlers for selecting folder
            dirTree.ItemSelected += new DirectoryTreePopup.ItemSelectedEventHandler(dirTree_ItemSelected);

            dirTree.Popup(rect);
        }

        private void btnFolderUp_Click(object sender, EventArgs e)
        {
            txtDirectory.Text = txtDirectory.Text.Substring(0, txtDirectory.Text.LastIndexOf("\\", StringComparison.OrdinalIgnoreCase));
        }

        private void dirTree_ItemSelected(object sender, DirectoryTreePopup.ItemSelectedEventArgs e)
        {
            txtDirectory.Text = dirTree.Tree.SelectedPath;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Setup parameters used to specify how the image is saved
            int[] width = { 200, 400, 800, 1400, 2000 };
            int[] height = { 120, 220, 440, 840, 1200 };
            ImageFormat[] format = { ImageFormat.Bmp, ImageFormat.Jpeg, ImageFormat.Png, ImageFormat.Tiff };
            int typeIndex = 0;
            int sizeIndex = 0;

            string[] ext = { ".bmp", ".jpg", ".png", ".tif" };

            // Collect selected indices
            if (sizes.Contains(txtSize.Text))
            {
                sizeIndex = sizes.IndexOf(txtSize.Text);
            }
            else
            {
                return;
            }

            if (types.Contains(txtType.Text))
            {
                typeIndex = types.IndexOf(txtType.Text);
            }

            string filename = txtDirectory.Text.TrimEnd("\\".ToCharArray()) + "\\" + txtFilename.Text;
            filename = Path.ChangeExtension(filename, ext[typeIndex]);

            if (File.Exists(filename))
            {
                if (MessageDialog.Show("File exists.  Overwrite?", "File Save", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return;
                }
            }

            // Save chart image
            chart.SaveImage(new Size(width[sizeIndex], height[sizeIndex]), filename, format[typeIndex]);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Close Form
            this.Close();
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnSizeOpen_Click(object sender, EventArgs e)
        {
            // Create TreeListPopup
            size = true; // Indicate this popup is for the size control
            popup.Tree.Columns.Clear();
            popup.Tree.Columns.Add(new TreeList.Column());
            popup.Tree.RowData = sizes;
            popup.ItemSelected += new TreeListPopup.ItemSelectedEventHandler(popup_ItemSelected);
            Rectangle rect = new Rectangle(txtSize.Location, txtSize.Size);
            rect.Offset(this.Location);
            rect.Offset(4, txtSize.Height + 4);
            popup.Popup(rect);
        }

        private void popup_ItemSelected(object sender, TreeListPopup.ItemSelectedEventArgs e)
        {
            if (size)
            {
                // Poulate the Size
                txtSize.Text = e.Item.ToString();
            }
            else
            {
                // Populate the Type
                txtType.Text = e.Item.ToString();
            }
        }

        private void btnTypeOpen_Click(object sender, EventArgs e)
        {
            // Create TreeListPopup
            size = false; // Indicate this popup is for the type (not the size) control
            popup.Tree.Columns.Clear();
            popup.Tree.Columns.Add(new TreeList.Column());
            popup.Tree.RowData = types;
            popup.ItemSelected += new TreeListPopup.ItemSelectedEventHandler(popup_ItemSelected);
            Rectangle rect = new Rectangle(txtType.Location, txtType.Size);
            rect.Offset(this.Location);
            rect.Offset(4, txtType.Height + 4);
            popup.Popup(rect);
        }
    }
}
