namespace RecordBook.Settings
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;
    using RecordBook.Data;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Data.Measurement;
    using ZoneFiveSoftware.Common.Visuals;

    public partial class SettingsPageControl : UserControl
    {
        #region Fields

        private static UserData.CatType currentRecordType;
        private static RecordCategory currentRecCat;

        #endregion

        #region Constructor

        public SettingsPageControl()
        {
            InitializeComponent();

            InitializeStaticItems();
        }

        #endregion

        #region Properties

        public static bool IsActiveDisplay
        {
            get
            {
                return PluginMain.GetApplication().ActiveView.Id == GUIDs.RBSettingsPage;
            }
        }

        #endregion

        #region Theme & Culture

        public void ThemeChanged(ITheme visualTheme)
        {
            bnrDistance.ThemeChanged(visualTheme);
            bnrExtreme.ThemeChanged(visualTheme);
            bnrCategories.ThemeChanged(visualTheme);
            bnrNowThen.ThemeChanged(visualTheme);

            treeDistance.ThemeChanged(visualTheme);
            treeExt.ThemeChanged(visualTheme);
            treeNow.ThemeChanged(visualTheme);
            treeCategory.ThemeChanged(visualTheme);

            txtDistDistance.ThemeChanged(visualTheme);
            txtDistName.ThemeChanged(visualTheme);
            txtDistUnit.ThemeChanged(visualTheme);
            txtExtName.ThemeChanged(visualTheme);
            txtExtType.ThemeChanged(visualTheme);
            txtNowName.ThemeChanged(visualTheme);
            txtNowType.ThemeChanged(visualTheme);
        }

        public void UICultureChanged(CultureInfo culture)
        {
            bnrCategories.Text = CommonResources.Text.LabelCategory;
            bnrDistance.Text = CommonResources.Text.LabelDistance + " " + CommonResources.Text.LabelCategory;
            bnrExtreme.Text = Resources.Strings.Label_Extremes + " " + CommonResources.Text.LabelCategory;
            bnrNowThen.Text = Resources.Strings.Label_NowThen + " " + CommonResources.Text.LabelCategory;

            lblDistance.Text = CommonResources.Text.LabelDistance;
            lblName.Text = CommonResources.Text.LabelName;
            lblName2.Text = CommonResources.Text.LabelName;
            lblName3.Text = CommonResources.Text.LabelName;
            lblType2.Text = CommonResources.Text.LabelType;
            lblType3.Text = CommonResources.Text.LabelType;
            lblUnits.Text = Resources.Strings.Label_Units;

            treeDistance.Columns[0].Text = CommonResources.Text.LabelName;
            treeDistance.Columns[1].Text = CommonResources.Text.LabelDistance;
            treeDistance.Columns[2].Text = Resources.Strings.Label_Units;
            treeExt.Columns[0].Text = CommonResources.Text.LabelName;
            treeExt.Columns[1].Text = CommonResources.Text.LabelCategory;
            treeNow.Columns[0].Text = CommonResources.Text.LabelName;
            treeNow.Columns[1].Text = CommonResources.Text.LabelCategory;

            // TODO: (LOW) Localize unlock button
            //btnUpgrade.Text = Strings.
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        private void RefreshSavedDistances()
        {
            treeDistance.RowData = UserData.DistanceRecordCategories;
        }

        private void RefreshSavedExtremes()
        {
            treeExt.RowData = UserData.ExtremeRecordCategories;
        }

        /// <summary>
        /// A display refresh of some sort.
        /// </summary>
        private void RefreshSavedNowThen()
        {
            treeNow.RowData = UserData.NowThenRecordCategories;
        }

        private void RefreshSettingsPageControl(object sender, EventArgs e)
        {
            RefreshSavedDistances();
            RefreshSavedExtremes();
            RefreshSavedNowThen();
        }

        private void InitializeStaticItems()
        {
            // ******************
            // Load Category Tree
            // ******************
            // Parts from: http://www.zonefivesoftware.com/sporttracks/Forums/viewtopic.php?p=40402

            List<PopupCategory> categories = new List<PopupCategory>();
            ILogbook logbook = PluginMain.GetLogbook();
            treeCategory.Columns.Clear();

            // Collect all categories
            SearchSubcategory(logbook.ActivityCategories, ref categories, 0);

            int i = 0;
            string name;
            foreach (PopupCategory cat in categories)
            {
                name = string.Empty;
                for (int j = 0; j < cat.Depth; j++)
                {
                    name = name + "   ";
                }

                cat.DisplayName = name + cat.Category.Name;
                cat.DisplayIndex = i;
                i++;
            }

            // Setup Category tree
            treeCategory.Columns.Add(new TreeList.Column("DisplayName"));
            treeCategory.RowData = categories;
            treeCategory.CheckBoxes = true;

            // Setup Distance, Extreme, & NowThen tree
            treeDistance.Columns.Clear();
            treeDistance.Columns.Add(new TreeList.Column("Name", CommonResources.Text.LabelName, 150, StringAlignment.Near));
            treeDistance.Columns.Add(new TreeList.Column("Distance", CommonResources.Text.LabelDistance, 80, StringAlignment.Near));
            treeDistance.Columns.Add(new TreeList.Column("Unit", Resources.Strings.Label_Units, 80, StringAlignment.Near));

            treeExt.Columns.Clear();
            treeExt.Columns.Add(new TreeList.Column("Name", CommonResources.Text.LabelName, 150, StringAlignment.Near));
            treeExt.Columns.Add(new TreeList.Column("RecordType", CommonResources.Text.LabelType, 120, StringAlignment.Near));

            treeNow.Columns.Clear();
            treeNow.Columns.Add(new TreeList.Column("Name", CommonResources.Text.LabelName, 150, StringAlignment.Near));
            treeNow.Columns.Add(new TreeList.Column("RecordType", CommonResources.Text.LabelType, 120, StringAlignment.Near));

            // *******************
            // Load Units Combobox
            // *******************
            // Extremes Combo
            txtExtType.Tag = RecordCategory.RecordGroup.Extreme;
            txtNowType.Tag = RecordCategory.RecordGroup.NowThen;
            txtDistUnit.Tag = RecordCategory.RecordGroup.Distance;
        }

        /// <summary>
        /// Gets a list of the currently selected (checked) category refIds from the Settings page category list
        /// </summary>
        /// <returns></returns>
        private List<string> GetCheckedCategories()
        {
            List<string> categories = new List<string>();
            ArrayList items = treeCategory.CheckedElements as ArrayList;

            foreach (PopupCategory item in items)
            {
                categories.Add(item.ReferenceId);
            }

            return categories;
        }

        /// <summary>
        /// Get currently selected record from appropriate tree based on which type of record is requested
        /// </summary>
        /// <param name="type">type of record category requested</param>
        /// <returns>A single selected record category.</returns>
        private RecordCategory GetCurrentRecord(UserData.CatType type)
        {
            ArrayList currentList = null;

            switch (type)
            {
                case UserData.CatType.Distance:
                    currentList = treeDistance.SelectedItems as ArrayList;
                    break;

                case UserData.CatType.Extremes:
                    currentList = treeExt.SelectedItems as ArrayList;
                    break;

                case UserData.CatType.NowThen:
                    currentList = treeNow.SelectedItems as ArrayList;
                    break;
            }

            if (currentList == null || currentList.Count == 0 || currentList[0].GetType() != typeof(RecordCategory))
            {
                // Return and do nothing if bad data
                return null;
            }

            return currentList[0] as RecordCategory;
        }

        /// <summary>
        /// Traverses through all categories in order and records their depth within the tree
        /// </summary>
        /// <param name="subCategory">List of Categories to search</param>
        /// <param name="categories">List of PopupCategory (By Reference)</param>
        /// <param name="depth">Depth associated with subCategory</param>
        private void SearchSubcategory(IEnumerable<IActivityCategory> subCategory, ref List<PopupCategory> categories, int depth)
        {
            foreach (IActivityCategory activityCategory in subCategory)
            {
                PopupCategory catInfo = new PopupCategory();
                catInfo.Category = activityCategory;
                catInfo.Depth = depth;

                categories.Add(catInfo);
                if (activityCategory.SubCategories.Count > 0)
                {
                    SearchSubcategory(activityCategory.SubCategories, ref categories, depth + 1);
                }
            }
        }

        private void SaveDistance(Length.Units units)
        {
            if (currentRecCat == null) return;

            float distance;

            if (float.TryParse(txtDistDistance.Text, NumberStyles.Float, CultureInfo.CurrentCulture, out distance))
            {
                currentRecCat.Name = txtDistName.Text;
                currentRecCat.Categories = GetCheckedCategories();
                currentRecCat.Distance = distance;
                currentRecCat.LengthMeasure = units;

                if (string.IsNullOrEmpty(currentRecCat.Name) || string.IsNullOrEmpty(txtDistUnit.Text))
                {
                    // Bad data
                    return;
                }

                UserData.UpdateCategory(UserData.CatType.Distance, currentRecCat);
            }
            else
            {
                txtDistDistance.Text = currentRecCat.Distance.ToString(CultureInfo.CurrentCulture);
            }

            RefreshSavedDistances();
        }

        private void SaveExtreme(RecordCategory.RecordType type)
        {
            if (currentRecCat == null) return;

            currentRecCat.Categories = GetCheckedCategories();
            currentRecCat.Name = txtExtName.Text;
            currentRecCat.Type = type;

            if (string.IsNullOrEmpty(currentRecCat.Name))
            {
                // Bad data
                return;
            }

            UserData.UpdateCategory(UserData.CatType.NowThen, currentRecCat);

            RefreshSavedExtremes();
        }

        private void SaveNowThen(RecordCategory.RecordType type)
        {
            if (currentRecCat == null) return;

            currentRecCat.Categories = GetCheckedCategories();
            currentRecCat.Name = txtNowName.Text;
            currentRecCat.Type = type;

            if (string.IsNullOrEmpty(currentRecCat.Name) || string.IsNullOrEmpty(txtNowType.Text))
            {
                // Bad data
                return;
            }

            UserData.UpdateCategory(UserData.CatType.NowThen, currentRecCat);

            RefreshSavedNowThen();
        }

        #endregion

        #region Event Handlers

        private void btnSaveDist_Click(object sender, EventArgs e)
        {
            UserData.DistanceRecordCategories.Add(new RecordCategory(Resources.Strings.Label_New, RecordCategory.RecordType.Distance, GetCheckedCategories(), 1, Length.Units.Kilometer));
            ChartData.IsSuperDirty = true;
            RefreshSavedDistances();
        }

        private void btnSaveExt_Click(object sender, EventArgs e)
        {
            UserData.ExtremeRecordCategories.Add(new RecordCategory("New", RecordCategory.RecordType.AvgHR, GetCheckedCategories()));
            ChartData.IsSuperDirty = true;
            RefreshSavedExtremes();
        }

        private void btnSaveNowThen_Click(object sender, EventArgs e)
        {
            UserData.NowThenRecordCategories.Add(new RecordCategory("New", RecordCategory.RecordType.AvgHR, GetCheckedCategories()));
            ChartData.IsSuperDirty = true;
            RefreshSavedNowThen();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            ZoneFiveSoftware.Common.Visuals.Button button = sender as ZoneFiveSoftware.Common.Visuals.Button;
            TreeList tree;

            switch (button.Tag as string)
            {
                case "Extreme":
                    tree = treeExt;
                    break;
                case "Distance":
                    tree = treeDistance;
                    break;
                case "NowThen":
                    tree = treeNow;
                    break;
                default:
                    // Bad data
                    return;
            }

            // Manage list if items
            List<RecordCategory> items = tree.RowData as List<RecordCategory>;
            ArrayList selected = tree.SelectedItems as ArrayList;
            IList nextItem = new List<RecordCategory>();

            foreach (RecordCategory item in selected)
            {
                nextItem.Clear();
                nextItem.Add(tree.FindNextSelectedAfterDelete(item));
                items.Remove(item);
            }

            // Save to specific data
            switch (button.Tag as string)
            {
                case "Extreme":
                    UserData.ExtremeRecordCategories = items;
                    break;
                case "Distance":
                    UserData.DistanceRecordCategories = items;
                    break;
                case "NowThen":
                    UserData.NowThenRecordCategories = items;
                    break;
            }

            // Update tree
            tree.RowData = items;

            if (items.Count > 0)
            {
                tree.SelectedItems = nextItem;
            }
        }

        /// <summary>
        /// Remove the selected record category from the treelist.
        /// </summary>
        /// <param name="tree">Treelist containing data to remove record from</param>
        /// <returns>Returns new list of records with the selected record removed</returns>
        private static List<RecordCategory> RemoveRecordCat(TreeList tree)
        {
            if (tree.SelectedItems.Count == 0)
            {
                // Nothing selected
                return null;
            }

            RecordCategory rcRemove = (RecordCategory)tree.SelectedItems[0];
            List<RecordCategory> rcAll = (List<RecordCategory>)tree.RowData;

            List<RecordCategory> save = new List<RecordCategory>();

            foreach (RecordCategory cat in rcAll)
            {
                if (cat != rcRemove && cat != null)
                {
                    save.Add(cat);
                }
            }

            return save;
        }

        private void upDownButton_Click(object sender, EventArgs e)
        {
            ZoneFiveSoftware.Common.Visuals.Button button = sender as ZoneFiveSoftware.Common.Visuals.Button;

            string[] config = ((string)(button.Tag)).Split('|');
            TreeList tree;

            bool up = config[1] == "Up";

            if (config[0] == "Extreme")
            {
                tree = treeExt;
            }
            else if (config[0] == "NowThen")
            {
                tree = treeNow;
            }
            else if (config[0] == "Distance")
            {
                // TODO: Fill in tags for distance up/down buttons
                tree = treeDistance;
            }
            else
            {
                // Bad data
                return;
            }

            if (tree.SelectedItems.Count == 0)
            {
                // Nothing selected
                return;
            }

            // Find record to move, full rec. set, and index of record
            RecordCategory rcMove = (RecordCategory)tree.SelectedItems[0];
            List<RecordCategory> rcAll = tree.RowData as List<RecordCategory>;
            int index = rcAll.IndexOf(rcMove);

            if (up)
            {
                // move record up 1 slot
                index = Math.Max(0, index - 1);
            }
            else
            {
                // move record down 1 slot
                index = Math.Min(rcAll.Count - 1, index + 1);
            }

            // Do the move
            rcAll.Remove(rcMove);
            rcAll.Insert(index, rcMove);

            // Save the records
            if (config[0] == "Extreme")
            {
                Settings.UserData.ExtremeRecordCategories = rcAll;
            }
            else if (config[0] == "NowThen")
            {
                Settings.UserData.NowThenRecordCategories = rcAll;
            }
            else if (config[0] == "Distance")
            {
                Settings.UserData.DistanceRecordCategories = rcAll;
            }

            // Refresh the treelist and select the original selected row
            tree.RowData = rcAll;

            // Selected
            List<RecordCategory> selected = new List<RecordCategory>();
            selected.Add(rcMove);
            tree.SelectedItems = selected;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ZoneFiveSoftware.Common.Visuals.Button btn = sender as ZoneFiveSoftware.Common.Visuals.Button;
            string tag = btn.Tag as string;

            switch (tag)
            {
                case "Distance":
                    Settings.UserData.DistanceRecordCategories = new List<RecordCategory>();
                    RefreshSavedDistances();
                    break;

                case "Extreme":
                    Settings.UserData.ExtremeRecordCategories = new List<RecordCategory>();
                    RefreshSavedExtremes();
                    break;

                case "Rank":
                    Settings.UserData.NowThenRecordCategories = new List<RecordCategory>();
                    RefreshSavedNowThen();
                    break;
            }
        }

        /// <summary>
        /// Handles checking/unchecking a category checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeCategory_CheckedChanged(object sender, TreeList.ItemEventArgs e)
        {
            // Do nothing if not associated with a category
            if (currentRecCat == null)
            {
                return;
            }

            // Get item checked and list of all categories
            PopupCategory checkedCat = e.Item as PopupCategory;
            List<PopupCategory> categories = treeCategory.RowData as List<PopupCategory>;
            List<PopupCategory>.Enumerator catEnum = categories.GetEnumerator();

            // Check/uncheck all children
            // TODO: (LOW) Add Indeterminate check state for parent nodes
            while (catEnum.MoveNext())
            {
                // Find current category
                if (catEnum.Current == checkedCat)
                {
                    CheckState check = treeCategory.ElementCheckState(catEnum.Current);
                    int depth = catEnum.Current.Depth;
                    while (catEnum.MoveNext() && depth < catEnum.Current.Depth)
                    {
                        treeCategory.SetCheckedState(catEnum.Current, check);
                    }
                    break;
                }
            }

            // Create and save new record based on current record type (which list are we modifying)
            currentRecCat.Categories = GetCheckedCategories();

            switch (currentRecordType)
            {
                case UserData.CatType.Distance:
                    UserData.UpdateCategory(UserData.CatType.Distance, currentRecCat);
                    break;

                case UserData.CatType.Extremes:
                    UserData.UpdateCategory(UserData.CatType.Extremes, currentRecCat);
                    break;

                case UserData.CatType.NowThen:
                    UserData.UpdateCategory(UserData.CatType.NowThen, currentRecCat);
                    break;
            }

            RefreshSettingsPageControl(null, null);
        }

        private void treeDistance_SelectionChanged(object sender, EventArgs e)
        {
            TreeList tree = sender as TreeList;
            if (tree.SelectedItems.Count <= 0)
                return;

            currentRecordType = UserData.CatType.Distance;
            currentRecCat = GetCurrentRecord(currentRecordType);
            
            if (currentRecCat != null)
            {
                txtDistName.Text = currentRecCat.Name;
                txtDistDistance.Text = currentRecCat.Distance.ToString(CultureInfo.CurrentCulture);
                txtDistUnit.Text = Length.LabelPlural(currentRecCat.LengthMeasure);

                LoadCategories(currentRecCat.Categories);

                treeExt.SelectedItems = null;
                treeNow.SelectedItems = null;
            }
        }

        private void treeExt_SelectionChanged(object sender, EventArgs e)
        {
            TreeList tree = sender as TreeList;
            if (tree.SelectedItems.Count <= 0)
                return;

            currentRecordType = UserData.CatType.Extremes;
            currentRecCat = GetCurrentRecord(currentRecordType);

            if (currentRecCat != null)
            {
                txtExtName.Text = currentRecCat.Name;
                txtExtType.Text = RecordCategory.DisplayValue(currentRecCat.Type);

                LoadCategories(currentRecCat.Categories);

                treeDistance.SelectedItems = null;
                treeNow.SelectedItems = null;
            }
        }

        private void treeNow_SelectionChanged(object sender, EventArgs e)
        {
            TreeList tree = sender as TreeList;
            if (tree.SelectedItems.Count <= 0)
                return;

            currentRecordType = UserData.CatType.NowThen;
            currentRecCat = GetCurrentRecord(currentRecordType);

            if (currentRecCat != null)
            {
                txtNowName.Text = currentRecCat.Name;
                txtNowType.Text = RecordCategory.DisplayValue(currentRecCat.Type);

                LoadCategories(currentRecCat.Categories);

                treeExt.SelectedItems = null;
                treeDistance.SelectedItems = null;
            }
        }

        /// <summary>
        /// Load categoryList into Category tree by setting checkboxes for included 
        /// categories, and unchecking those that aren't included.
        /// </summary>
        /// <param name="categoryList">List of included/checked categories</param>
        private void LoadCategories(List<string> categoryList)
        {
            List<PopupCategory> categories = treeCategory.RowData as List<PopupCategory>;

            foreach (PopupCategory item in categories)
            {
                if (categoryList.Contains(item.ReferenceId))
                {
                    treeCategory.SetCheckedState(item, CheckState.Checked);
                }
                else
                {
                    treeCategory.SetCheckedState(item, CheckState.Unchecked);
                }
            }
        }

        private void txtDist_Leave(object sender, EventArgs e)
        {
            currentRecCat = GetCurrentRecord(UserData.CatType.Distance);
            if (currentRecCat != null)
            {
                SaveDistance(currentRecCat.LengthMeasure);
            }
        }

        private void txtExt_Leave(object sender, EventArgs e)
        {
            currentRecCat = GetCurrentRecord(UserData.CatType.Extremes);
            if (currentRecCat != null)
            {
                SaveExtreme(currentRecCat.Type);
            }
        }

        private void txtNow_Leave(object sender, EventArgs e)
        {
            currentRecCat = GetCurrentRecord(UserData.CatType.NowThen);
            if (currentRecCat != null)
            {
                SaveNowThen(currentRecCat.Type);
            }
        }

        private class PopupCategory
        {
            #region Fields

            IActivityCategory category;
            string textName;
            int depth;
            int displayIndex;

            #endregion

            #region Properties

            public IActivityCategory Category
            {
                get { return this.category; }
                set { this.category = value; }
            }

            public string DisplayName
            {
                get { return this.textName; }
                set { this.textName = value; }
            }

            public int Depth
            {
                get { return this.depth; }
                set { this.depth = value; }
            }

            public int DisplayIndex
            {
                get { return this.displayIndex; }
                set { this.displayIndex = value; }
            }

            public string ReferenceId
            {
                get { return this.category.ReferenceId; }
            }

            #endregion
        }

        private void txtType_ButtonClick(object sender, EventArgs e)
        {
            ZoneFiveSoftware.Common.Visuals.TextBox textbox = sender as ZoneFiveSoftware.Common.Visuals.TextBox;

            if (textbox.Tag.GetType() != typeof(RecordCategory.RecordGroup))
            {
                // Bad data!!
                return;
            }

            // Create TreeListPopup
            TreeListPopup popup = new TreeListPopup();
            popup.Tag = textbox.Tag;
            popup.Tree.Columns.Add(new TreeList.Column("Value"));
            popup.Tree.RowData = RecordCategory.GetSettingsComboItems((RecordCategory.RecordGroup)textbox.Tag);
            popup.ItemSelected += new TreeListPopup.ItemSelectedEventHandler(popup_ItemSelected);
            popup.ThemeChanged(PluginMain.GetApplication().VisualTheme);
            Rectangle rect = new Rectangle(textbox.Location, textbox.Size);
            rect.Offset(this.Location);
            rect.Offset(4, textbox.Height + 4);
            popup.Popup(textbox.RectangleToScreen(textbox.ClientRectangle));
        }

        private void popup_ItemSelected(object sender, TreeListPopup.ItemSelectedEventArgs e)
        {
            TreeListPopup popup = sender as TreeListPopup;

            // Collect selected data
            KeyValuePair<object, string> selected = (KeyValuePair<object, string>)e.Item;

            // Process selected item...
            if (selected.Key.GetType() == typeof(RecordCategory.RecordType))
            {
                RecordCategory.RecordType recordType = (RecordCategory.RecordType)selected.Key;

                if ((RecordCategory.RecordGroup)popup.Tag == RecordCategory.RecordGroup.NowThen)
                {
                    // Now Then type changed
                    txtNowType.Text = selected.Value;
                    SaveNowThen(recordType);
                }
                else if ((RecordCategory.RecordGroup)popup.Tag == RecordCategory.RecordGroup.Extreme)
                {
                    // Extreme type changed
                    txtExtType.Text = selected.Value;
                    SaveExtreme(recordType);
                }
            }
            else if (selected.Key.GetType() == typeof(Length.Units))
            {
                Length.Units units = (Length.Units)selected.Key;

                if ((RecordCategory.RecordGroup)popup.Tag == RecordCategory.RecordGroup.Distance)
                {
                    // Distance unit changed
                    txtDistUnit.Text = selected.Value;
                    SaveDistance(units);
                }
            }
        }

        private void txtDistUnit_ButtonClick(object sender, EventArgs e)
        {
            ZoneFiveSoftware.Common.Visuals.TextBox textbox = sender as ZoneFiveSoftware.Common.Visuals.TextBox;

            // Create TreeListPopup
            TreeListPopup popup = new TreeListPopup();
            popup.Tag = textbox.Tag;
            popup.Tree.Columns.Add(new TreeList.Column("Value"));
            popup.Tree.RowData = RecordCategory.GetSettingsComboItems((RecordCategory.RecordGroup)textbox.Tag);
            popup.ItemSelected += new TreeListPopup.ItemSelectedEventHandler(popup_ItemSelected);
            Rectangle rect = new Rectangle(textbox.Location, textbox.Size);
            rect.Offset(this.Location);
            rect.Offset(4, textbox.Height + 4);
            popup.Popup(textbox.RectangleToScreen(textbox.ClientRectangle));
        }

        #endregion
    }
}
