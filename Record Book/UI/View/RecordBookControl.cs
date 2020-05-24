namespace RecordBook.UI.View
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;
    using RecordBook.Data;
    using RecordBook.Settings;
    using ZoneFiveSoftware.Common.Data;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Visuals;
    using ZoneFiveSoftware.Common.Visuals.Util;

    public partial class RecordBookControl : UserControl
    {
        #region Fields

        private static Popup_Progress prog;
        private string CurrentSortedColumnId = string.Empty;
        private TreeList currentTree;
        // HACK: Couldn't find another way to pass this data through the context menu
        // These go with the Context menu: ListSettings and CopyActivities
        private List<TreeList.Column> currentColumns;

        #endregion

        #region Constructors

        public RecordBookControl()
        {
            InitializeComponent();

            InitializeChartData();

            // Subscribe to new/removed activities
            PluginMain.GetLogbook().Activities.CollectionChanged += new NotifyCollectionChangedEventHandler<IActivity>(Activities_CollectionChanged);
            PluginMain.GetApplication().SystemPreferences.PropertyChanged += new PropertyChangedEventHandler(SystemPreferences_PropertyChanged);
            UserData.PropertyChanged += new PropertyChangedEventHandler(UserData_PropertyChanged);

            // Reset stubborn controls that continue to move automatically
            txtEndDate.Location = new Point(bnrDistance.Width - 118, 2);
            txtStartDate.Location = new Point(txtEndDate.Location.X - 93, 2);

            InitializeTree(treeDistance, TreeType.Distance);
            InitializeTree(treeExtremes, TreeType.Extremes);
            InitializeTree(treeAllActivities, TreeType.WhereDoesItRank);

            topMenuItem_Click(null, null);
        }

        #endregion

        #region Properties

        #endregion

        #region Enumerations

        private enum TreeType
        {
            WhereDoesItRank,
            Extremes,
            Distance
        }

        #endregion

        #region Methods

        private void InitializeTree(TreeList tree, TreeType type)
        {
            IList<TreeList.Column> columns;

            switch (type)
            {
                case TreeType.Distance:
                    columns = BuildDisplayColumns(GetDistanceColumns(), UserData.UserColumns_Distance);
                    break;

                case TreeType.Extremes:
                    columns = BuildDisplayColumns(GetExtremesColumns(), UserData.UserColumns_Extreme);
                    break;

                default:
                case TreeType.WhereDoesItRank:
                    columns = BuildDisplayColumns(GetActivitiesColumns(), UserData.UserColumns_AllActivites);
                    break;
            }

            tree.Columns.Clear();
            foreach (TreeList.Column column in columns)
            {
                tree.Columns.Add(column);
            }
        }

        private void InitializeChartData()
        {
            prog = new Popup_Progress();
            prog.UICultureChanged(PluginMain.GetApplication().SystemPreferences.UICulture);
            prog.UpdateProgress(0, string.Empty);
            prog.Show();
            prog.BringToFront();

            ChartData.ProgressTick += new ProgressChangedEventHandler(ChartData_ProgressTick);
            ChartData.Initialize();
            ChartData.ProgressTick -= ChartData_ProgressTick;

            prog.Dispose();
        }

        public void RefreshPage()
        {
            if (ChartData.IsSuperDirty)
                InitializeChartData();

            ChartData.ProcessUpdateQueue();
            ChartData.ApplyFilter(); // Record building completed here, Records ranked properly

            RefreshTree(ChartData.DistanceRecords, treeDistance, UserData.NumRecords, false, TreeType.Distance);
            RefreshTree(ChartData.ExtremeRecords, treeExtremes, UserData.NumRecords, false, TreeType.Extremes);
            RefreshTree(ChartData.RankedRecords, treeAllActivities, UserData.NumRecords, true, TreeType.WhereDoesItRank);

            BuildNowThenTree(treeTime);
            BuildNowThenTree(treeNow);

            // Refresh Now and Then records
            Calendar_SelectedChanged(null, new EventArgs());

            RefreshCalendar();
        }

        public void ThemeChanged(ITheme visualTheme)
        {
            bnrExtremes.ThemeChanged(visualTheme);
            bnrDistance.ThemeChanged(visualTheme);
            treeExtremes.ThemeChanged(visualTheme);
            treeDistance.ThemeChanged(visualTheme);
            tabcontrol.BackColor = visualTheme.Control;
            tabcontrol.BorderColor = visualTheme.Control;
            tabcontrol.ActiveColor = visualTheme.Control;
            tabcontrol.InactiveColor = visualTheme.Window;
            tab_extremes.BackColor = visualTheme.Control;
            bnrWhereDoesItRank.ThemeChanged(visualTheme);
            bnrThen.ThemeChanged(visualTheme);
            bnrNow.ThemeChanged(visualTheme);
            treeAllActivities.ThemeChanged(visualTheme);
            treeTime.ThemeChanged(visualTheme);
            treeNow.ThemeChanged(visualTheme);
            distanceChart.ThemeChanged(visualTheme);
        }

        public void UICultureChanged(CultureInfo culture)
        {
            bnrDistance.Text = CommonResources.Text.LabelDistance;
            bnrExtremes.Text = Resources.Strings.Label_Extremes;
            bnrNow.Text = Resources.Strings.Label_Now;
            bnrThen.Text = Resources.Strings.Label_Then;
            bnrWhereDoesItRank.Text = Resources.Strings.Label_WhereDoesItRank;

            tab_distance.Text = CommonResources.Text.LabelDistance;
            tab_extremes.Text = Resources.Strings.Label_Extremes;
            tab_rank.Text = Resources.Strings.Label_WhereDoesItRank;

            //TODO: (LOW) Localize tree columns
            // TODO: (LOW) Localize top 25 menu
        }

        #endregion

        #region Generic Methods

        private void RefreshTree(List<RecordSet> rs, TreeList tree, int recordCount, bool expandTree, TreeType type)
        {
            // Add columns to Activity treeList
            IList<RecordCategory> expanded = new List<RecordCategory>();
            // TODO: (MED) Set selected
            RecordNode selected = CollectionUtils.GetFirstItemOfType<RecordNode>(tree.SelectedItems);
            foreach (RecordNode node in CollectionUtils.GetAllContainedItemsOfType<RecordNode>(tree.Expanded))
            {
                if (node.IsRecordSet)
                    expanded.Add(node.RecSet.Category);
            }

            // Remove handler and assign data (assigning would cause handler to run)
            tree.SelectedItemsChanged -= treeRecords_SelectedChanged;
            tree.ShowPlusMinus = true;

            IList<RecordNode> categories = tree.RowData as IList<RecordNode>;
            if (categories == null)
                categories = new List<RecordNode>();
            else
                categories.Clear();

            // Loop through each record category
            for (int i = 0; i < rs.Count; i++)
            {
                if (rs[i].Count != 0)
                {
                    RecordNode cat = new RecordNode(null, rs[i]);

                    for (int j = 0; j < rs[i].Count; j++)
                    {
                        RecordNode rec = new RecordNode(cat, rs[i][j]);
                        if (!ChartData.IsFiltered(rec.Record, recordCount))
                        {
                            cat.Children.Add(rec);
                            if (type == TreeType.WhereDoesItRank)
                                categories.Add(rec);
                        }
                    }

                    if (expanded.Contains(cat.RecSet.Category))
                        cat.Expanded = true;
                    else
                        cat.Expanded = false;

                    if (type != TreeType.WhereDoesItRank)
                        categories.Add(cat);
                }
            }

            tree.LabelProvider = new RecordBookLabelProvider();
            tree.RowDataRenderer = new RecordBookRenderer(tree);

            // Assign data set to tree
            tree.RowData = categories;

            // Expand the tree if told to
            if (expandTree)
            {
                tree.SetExpanded(categories, true, true);
            }
            else if (0 < expanded.Count)
            {
                foreach (RecordNode node in categories)
                {
                    if (node.Expanded)
                        tree.SetExpanded(node, true);
                    else
                        tree.SetExpanded(node, false);
                }

                // TODO: (MED) Set selected
                //if (selected != null)
                //tree.SelectedItems = new List<object> { selected };
            }

            // Re-add handler
            tree.SelectedItemsChanged += treeRecords_SelectedChanged;
        }

        /// <summary>
        /// Populate the highlighted calendar dates
        /// </summary>
        /// <param name="activities">The dates of these activities will be highlighted on the calendar</param>
        private void RefreshCalendar()
        {
            IList<RecordSet> recordSets;

            switch (tabcontrol.SelectedIndex)
            {
                case 0:
                    // Extreme
                    recordSets = ChartData.ExtremeRecords;
                    break;
                case 1:
                    // Distance
                    recordSets = ChartData.DistanceRecords;
                    break;
                case 2:
                    // Where does it rank
                    recordSets = ChartData.RankedRecords;
                    break;
                default:
                    return;
            }

            IList<DateTime> activityDates = new List<DateTime>();

            foreach (RecordSet recordSet in recordSets)
            {
                foreach (Record record in recordSet)
                {
                    // Highlight activity dates on calender
                    if (!activityDates.Contains(record.StartTime.ToLocalTime().Date))
                    {
                        activityDates.Add(record.StartTime.ToLocalTime().Date);
                    }
                }
            }

            PluginMain.GetApplication().Calendar.SetHighlightedDates(activityDates);
        }

        /// <summary>
        /// Tree context menu.  Shows 'List Settings' and 'Copy'.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="mouse"></param>
        /// <param name="tType"></param>
        private void TreeContextMenu(Control control, MouseEventArgs mouse, TreeType tType)
        {
            // Right Click on header: Context menu
            ITheme visualTheme = PluginMain.GetApplication().VisualTheme;
            ContextMenuStrip menuStrip = new ContextMenuStrip();
            menuStrip.Items.Add(Resources.Strings.Label_ListSettings + "...", CommonResources.Images.Table16);
            menuStrip.Items.Add(CommonResources.Text.ActionCopy, CommonResources.Images.DocumentCopy16);
            menuStrip.Items[1].Tag = string.Empty;
            switch (tType)
            {
                case TreeType.WhereDoesItRank:
                    menuStrip.ItemClicked += new ToolStripItemClickedEventHandler(treeWhereDoesItRank_menuStrip_ItemClicked);
                    break;
                case TreeType.Distance:
                    menuStrip.ItemClicked += new ToolStripItemClickedEventHandler(treeDistance_menuStrip_ItemClicked);
                    break;
                case TreeType.Extremes:
                    menuStrip.ItemClicked += new ToolStripItemClickedEventHandler(treeExtremes_menuStrip_ItemClicked);
                    break;
            }

            // Display the Context Menu
            Point point = mouse.Location;
            point = control.PointToScreen(mouse.Location);
            menuStrip.Show(point);
        }

        /// <summary>
        /// Ensures that only digits are allowed to be entered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void digitValidator(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true; // input is not passed on to the control(TextBox)`
            }
        }

        /// <summary>
        /// Select the record that matches the selected date.
        /// </summary>
        /// <param name="tree">Tree containing records.</param>
        /// <param name="node">This will select a record within this RecordSet.</param>
        /// <param name="date">Date to find matching activity.</param>
        /// <returns>True if selected activity date found, false if no record could be selected.</returns>
        private bool SelectRecord(TreeList tree, TreeList.TreeListNode node, DateTime date)
        {
            List<RecordNode> selected = new List<RecordNode>(); // New activity to select based on calendar date
            DateTime actDate;
            RecordSet recSet;
            int i = 0;

            if (node == null) return false;

            // 'node' should describe parent category.  Select parent category if not already selected.
            if (node.Parent != null)
            {
                node = node.Parent as TreeList.TreeListNode;
            }

            // read RecordSet under selected record category
            recSet = node.Element as RecordSet;
            if (recSet == null) return false;

            // Find record with date matching calendar and select it.
            while (i < node.Children.Count)
            {
                // Compare Dates
                actDate = recSet[i].StartTime.ToLocalTime().Date;
                if (actDate == date)
                {
                    // Set selected record
                    RecordNode recordW = node.Children[i] as RecordNode;
                    selected.Add(recordW);
                    tree.SelectedItems = selected;
                    tree.SelectedItemsChanged += treeRecords_SelectedChanged;
                    return true;
                }

                i++;
            }

            // No matching date found
            return false;
        }

        /// <summary>
        /// Get list of columns to be displayed
        /// </summary>
        /// <param name="columns">Full set of columns</param>
        /// <param name="displayColumns">Column IDs to select in the format "columnid1;columnid2;..."</param>
        /// <returns>List of columns to be displayed</returns>
        private static List<TreeList.Column> BuildDisplayColumns(List<TreeList.Column> columns, string displayColumns)
        {
            // TODO: (LOW) Cleanup - I think that BuildDisplayColumns may be eliminated by moving functions to GetXXXXColumns()
            // Display and organize saved columns
            List<TreeList.Column> display_columns = new List<TreeList.Column>();
            string[] userColumns = displayColumns.Split(";".ToCharArray());

            int userColumnIndex = 0;
            foreach (string column in userColumns)
            {
                // Rearrange Columns
                string[] column_data = column.Split("|".ToCharArray());
                int currentIndex = columns.FindIndex(delegate(TreeList.Column c) { return c.Id == column_data[0]; });
                if (currentIndex >= 0)
                {
                    // HACK: ST was being lame and wouldn't let me reset the width of the existing column so I made a new one
                    TreeList.Column newCol;
                    if (column_data.Length > 1)
                    {
                        newCol = new TreeList.Column(columns[currentIndex].Id, columns[currentIndex].Text, Convert.ToInt32(column_data[1]), columns[currentIndex].TextAlign);
                    }
                    else
                    {
                        newCol = new TreeList.Column(columns[currentIndex].Id, columns[currentIndex].Text, columns[currentIndex].Width, columns[currentIndex].TextAlign);
                    }
                    newCol.CanSelect = columns[currentIndex].CanSelect;
                    display_columns.Insert(userColumnIndex, newCol);
                    userColumnIndex++;
                }
            }

            return display_columns;
        }

        /// <summary>
        /// Gets the RecordSet list out of the tree's RowData
        /// </summary>
        /// <param name="tree">TreeList containing a list of RecordSet</param>
        /// <returns>List of RecordSet contained in tree</returns>
        private static List<RecordSet> GetRecordSet(TreeList tree)
        {
            List<RecordSet> recSetList = new List<RecordSet>();
            List<RecordNode> nodes = tree.RowData as List<RecordNode>;

            if (nodes != null && nodes.Count > 0 && nodes[0].IsRecordSet)
            {
                // Grouped tabs
                foreach (RecordNode recSet in nodes)
                {
                    if (recSet != null)
                    {
                        recSetList.Add(recSet.RecSet);
                    }
                }
            }
            else if (nodes != null && nodes.Count > 0 && nodes[0].IsRecord)
            {
                // Where does it rank tab
                RecordCategory category = new RecordCategory();
                category.Type = RecordCategory.RecordType.AllActivities;
                category.Name = Resources.Strings.Label_AllActivities;
                RecordSet set = new RecordSet(category);
                set.AddRecords();
                recSetList.Add(set);
            }

            return recSetList;
        }

        /// <summary>
        /// Get an icon from an image
        /// </summary>
        /// <param name="image">Icon image (bitmap)</param>
        public static Icon GetIcon(Image image)
        {
            Bitmap bitmap = image as Bitmap;

            if (bitmap != null)
            {
                return Icon.FromHandle(bitmap.GetHicon());
            }

            return null;
        }

        #endregion

        private void lblEval_Click(object sender, EventArgs e)
        {

        }
    }
}
