using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using GrayIris.Utilities.UI.Controls;
using RecordBook.Data;
using RecordBook.Settings;
using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Forms;
using System.Globalization;

namespace RecordBook.UI.View
{
    partial class RecordBookControl
    {
        private void treeRecords_SelectedChanged(object sender, EventArgs e)
        {
            TreeList tree = sender as TreeList;
            Record record;
            TreeList.TreeListNode selected;
            if (tree != null)
            {
                if (tree.SelectedItems.Count > 0)
                {
                    switch (tabcontrol.SelectedIndex)
                    {
                        case 2:
                            // 'Where Does it Rank' (simply a list of records)
                            selected = tree.SelectedItems[0] as TreeList.TreeListNode;
                            record = selected.Element as Record;
                            break;
                        case 1:
                            // Distance (categorized records)
                            selected = tree.SelectedItems[0] as TreeList.TreeListNode;
                            record = selected.Element as Record;

                            // Update Distance chart
                            if (tree.SelectedItems.Count != 0)
                            {
                                RecordNode node = tree.SelectedItems[0] as RecordNode;
                                if (node.IsRecordSet)
                                {
                                    DrawActivityRankChart(node.RecSet);
                                }
                                else if (node.IsRecord)
                                {
                                    DrawChartSelectedRecords((ArrayList)tree.SelectedItems);
                                }
                            }

                            break;

                        default:
                            // All other tabs (categorized records)
                            selected = tree.SelectedItems[0] as TreeList.TreeListNode;
                            record = selected.Element as Record;
                            break;
                    }

                    if (record != null)
                    {
                        // Select the calender date
                        DateTime activityDate = record.StartTime.Date;
                        PluginMain.GetApplication().Calendar.Selected = activityDate;
                    }
                }
            }
        }

        private void AllActivities_SelectedChanged(object sender, EventArgs e)
        {
            TreeList tree = sender as TreeList;
            if (tree != null)
            {
                if (tree.SelectedItems.Count > 0)
                {
                    RecordNode record = tree.SelectedItems[0] as RecordNode;
                    if (record != null)
                    {
                        // Move the data to the bottom 2 tables
                        PluginMain.GetApplication().Calendar.Selected = record.Record.StartTime.ToLocalTime().Date;

                        AddRecordToNowThenTree(record, DisplayRecord.recordTime.Then);
                        AddRecordToNowThenTree(record, DisplayRecord.recordTime.Now);
                    }
                }
            }
        }

        /// <summary>
        /// Column header clicked as if to sort by the selected colummn.  This is used generically for all tables.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tree_ColumnClicked(object sender, TreeList.ColumnEventArgs e)
        {
            // Sort only the data currently in the treeList
            List<RecordSet> recSet = GetRecordSet(treeAllActivities);

            // Used to tell if they clicked on a column that isn't sortable
            bool noSort = false;

            if (recSet[0] != null)
            {
                // Create a comparer instance
                Record.RecordComparer comparer = new Record.RecordComparer();

                // TODO: (LOW) Make columns sortable
                switch (e.Column.Id)
                {
                    case "RecValue":
                        noSort = true;
                        break;

                    case "MetersPerSecond":
                        comparer.ComparisonMethod = Record.RecordComparer.ComparisonType.AvgSpeed;
                        break;

                    default:
                        comparer.ComparisonMethod = (Record.RecordComparer.ComparisonType)Enum.Parse(typeof(Record.RecordComparer.ComparisonType), e.Column.Id);
                        break;
                }

                if (!noSort)
                {
                    if (CurrentSortedColumnId != e.Column.Id)
                    {
                        // Sort ascending
                        CurrentSortedColumnId = e.Column.Id;
                        comparer.SortOrder = Record.RecordComparer.Order.Ascending;
                        treeAllActivities.SetSortIndicator(e.Column.Id, true);
                    }
                    else
                    {
                        // Sort descending
                        CurrentSortedColumnId = string.Empty;
                        comparer.SortOrder = Record.RecordComparer.Order.Descending;
                        treeAllActivities.SetSortIndicator(e.Column.Id, false);
                    }

                    // Sort and display data
                    recSet[0].Sort(comparer);
                    List<RecordSet> rsAll = new List<RecordSet>();
                    rsAll.Add(recSet[0]);

                    // TODO: (LOW) tree refresh on column click (sort) is not right
                    RefreshTree(rsAll, treeAllActivities, UserData.NumRecords, true, TreeType.WhereDoesItRank);
                }
            }
        }

        private void AllActivities_ColumnResized(object sender, TreeList.ColumnEventArgs e)
        {
            TreeList t = sender as TreeList;
            string colString = string.Empty;
            foreach (TreeList.Column column in t.Columns)
            {
                if (column.Id == e.Column.Id)
                {
                    string[] settingsColumns = Settings.UserData.UserColumns_AllActivites.Split(";".ToCharArray());
                    foreach (string settingColumn in settingsColumns)
                    {
                        if (!string.IsNullOrEmpty(settingColumn))
                        {
                            string[] column_data = settingColumn.Split("|".ToCharArray());
                            if (column_data[0] == column.Id)
                            {
                                column_data[1] = e.Column.Width.ToString();
                            }

                            colString += column_data[0] + "|" + column_data[1] + ";";
                        }
                    }

                    break;
                }
            }

            Settings.UserData.UserColumns_AllActivites = colString;
        }

        private void Distance_ColumnResized(object sender, TreeList.ColumnEventArgs e)
        {
            TreeList t = sender as TreeList;
            string colString = string.Empty;
            foreach (TreeList.Column column in t.Columns)
            {
                if (column.Id == e.Column.Id)
                {
                    string[] settingsColumns = Settings.UserData.UserColumns_Distance.Split(";".ToCharArray());
                    foreach (string settingColumn in settingsColumns)
                    {
                        if (!string.IsNullOrEmpty(settingColumn))
                        {
                            string[] column_data = settingColumn.Split("|".ToCharArray());
                            if (column_data[0] == column.Id)
                            {
                                column_data[1] = e.Column.Width.ToString();
                            }

                            colString += column_data[0] + "|" + column_data[1] + ";";
                        }
                    }

                    break;
                }
            }

            Settings.UserData.UserColumns_Distance = colString;
        }

        private void Extremes_ColumnResized(object sender, TreeList.ColumnEventArgs e)
        {
            TreeList t = sender as TreeList;
            string colString = string.Empty;
            foreach (TreeList.Column column in t.Columns)
            {
                if (column.Id == e.Column.Id)
                {
                    string[] settingsColumns = Settings.UserData.UserColumns_Extreme.Split(";".ToCharArray());
                    foreach (string settingColumn in settingsColumns)
                    {
                        if (!string.IsNullOrEmpty(settingColumn))
                        {
                            string[] column_data = settingColumn.Split("|".ToCharArray());
                            if (column_data[0] == column.Id)
                            {
                                column_data[1] = e.Column.Width.ToString();
                            }

                            colString += column_data[0] + "|" + column_data[1] + ";";
                        }
                    }

                    break;
                }
            }

            Settings.UserData.UserColumns_Extreme = colString;
        }

        private void btnCalendar_Click(object sender, EventArgs e)
        {
            CalendarPopup calendarPopup = new CalendarPopup();

            ZoneFiveSoftware.Common.Visuals.TextBox textbox = sender as ZoneFiveSoftware.Common.Visuals.TextBox;
            calendarPopup.Tag = textbox;

            DateTime date;
            if (!DateTime.TryParse(textbox.Text, out date))
            {
                date = DateTime.Now.Date;
            }

            calendarPopup.Calendar.SetSelected(date, date);
            calendarPopup.ItemSelected += new CalendarPopup.ItemSelectedEventHandler(popupCalendar_ItemSelected);
            calendarPopup.ThemeChanged(PluginMain.GetApplication().VisualTheme);
            calendarPopup.Calendar.StartOfWeek = PluginMain.GetApplication().SystemPreferences.StartOfWeek;
            calendarPopup.Popup(textbox.RectangleToScreen(textbox.ClientRectangle));
        }

        /// <summary>
        /// Supports Popup Calendar buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popupCalendar_ItemSelected(object sender, CalendarPopup.ItemSelectedEventArgs e)
        {
            // Get objects of interest
            CalendarPopup calendarPopup = sender as CalendarPopup;
            ZoneFiveSoftware.Common.Visuals.TextBox textbox = calendarPopup.Tag as ZoneFiveSoftware.Common.Visuals.TextBox;

            // Store selected value in textbox and save to workout
            textbox.Text = e.Item.ToString("d", CultureInfo.CurrentCulture);
            textbox.SelectNextControl(textbox, true, true, true, true);

            // Dispose of popup
            calendarPopup.Hide();
            calendarPopup.Dispose();
        }

        private void txtFilterDate_Leave(object sender, EventArgs e)
        {
            ZoneFiveSoftware.Common.Visuals.TextBox text = sender as ZoneFiveSoftware.Common.Visuals.TextBox;
            DateTime date;

            // Handle Start Date
            if (text.Name.Contains("Start"))
            {
                if (DateTime.TryParse(text.Text, out date))
                    UserData.StartDateFilter = date;
                else
                    text.Text = UserData.StartDateFilter.ToString(CultureInfo.CurrentCulture);
            }
            // Handle End Date
            else if (text.Name.Contains("End"))
            {
                if (DateTime.TryParse(text.Text, out date))
                    UserData.EndDateFilter = date;
                else
                    text.Text = UserData.EndDateFilter.ToString(CultureInfo.CurrentCulture);
            }
        }

        private void treeAllActivities_Click(object sender, EventArgs e)
        {
            MouseEventArgs mouse = (MouseEventArgs)e;
            TreeList tree = (TreeList)sender;

            if (mouse.Button == MouseButtons.Right && (mouse.Y < tree.HeaderRowHeight || Cursor.Current == Cursors.Hand))
            {
                currentTree = treeAllActivities;
                currentColumns = GetActivitiesColumns();
                TreeContextMenu(tree, mouse, TreeType.WhereDoesItRank);
            }
        }

        private void treeExtremes_Click(object sender, EventArgs e)
        {
            MouseEventArgs mouse = (MouseEventArgs)e;
            TreeList tree = (TreeList)sender;

            if (mouse.Button == MouseButtons.Right && (mouse.Y < tree.HeaderRowHeight || Cursor.Current == Cursors.Hand))
            {
                currentTree = treeExtremes;
                currentColumns = GetExtremesColumns();
                TreeContextMenu(tree, mouse, TreeType.Extremes);
            }
        }

        private void treeDistance_Click(object sender, EventArgs e)
        {
            MouseEventArgs mouse = e as MouseEventArgs;
            TreeList tree = sender as TreeList;
            if (mouse != null && tree != null)
            {
                if (mouse.Button == MouseButtons.Right && (mouse.Y < tree.HeaderRowHeight || Cursor.Current == Cursors.Hand))
                {
                    currentTree = treeDistance;
                    currentColumns = GetDistanceColumns();
                    TreeContextMenu(tree, mouse, TreeType.Distance);
                }
            }
        }

        private void treeWhereDoesItRank_menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ContextMenuStrip strip = sender as ContextMenuStrip;
            if (e.ClickedItem.Text == Resources.Strings.Label_ListSettings + "...")
            {
                ListSettings(TreeType.WhereDoesItRank);
            }
            else if (e.ClickedItem.Text == CommonResources.Text.ActionCopy)
            {
                CopyActivities(currentTree);
            }

            strip.Dispose();
        }

        private void treeExtremes_menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ContextMenuStrip strip = sender as ContextMenuStrip;
            if (e.ClickedItem.Text == Resources.Strings.Label_ListSettings + "...")
            {
                ListSettings(TreeType.Extremes);
            }
            else if (e.ClickedItem.Text == CommonResources.Text.ActionCopy)
            {
                CopyActivities(currentTree);
            }

            strip.Dispose();
        }

        private void treeDistance_menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ContextMenuStrip strip = sender as ContextMenuStrip;
            if (e.ClickedItem.Text == Resources.Strings.Label_ListSettings + "...")
            {
                ListSettings(TreeType.Distance);
            }
            else if (e.ClickedItem.Text == CommonResources.Text.ActionCopy)
            {
                CopyActivities(currentTree);
            }

            strip.Dispose();
        }

        private void zoomInButton_Click(object sender, EventArgs e)
        {
            distanceChart.ZoomIn();
        }

        private void zoomOutButton_Click(object sender, EventArgs e)
        {
            distanceChart.ZoomOut();
        }

        private void autoFitButton_Click(object sender, EventArgs e)
        {
            distanceChart.AutozoomToData(true);
        }

        private void savePicButton_Click(object sender, EventArgs e)
        {
            ITheme theme = PluginMain.GetApplication().VisualTheme;

            SaveImageDialog save = new SaveImageDialog();
            save.ThemeChanged(theme);

            // TODO: (LOW) Set default file location for picture save
            save.FileName = Resources.Strings.Label_RecordBook + " " + DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);

            if (save.ShowDialog() == DialogResult.OK)
            {
                // Image Saved (save occurs in SaveDialog)
                string filename = save.FileName;

                if (System.IO.File.Exists(filename))
                {
                    if (MessageDialog.Show("File exists.  Overwrite?", "File Save", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    {
                        return;
                    }
                }

                // TODO: (LOW) Save chart image
                //distanceChart.SaveImage(save.ImageSize, save.FileName, save.ImageFormat);
            }

            save.Dispose();
        }

        private void stripesButton_Click(object sender, EventArgs e)
        {
            if (distanceChart.YAxis.Stripes.Count == 0)
            {
                DrawStripes();
            }
            else
            {
                // If the button was pressed to clear the stripes, clear them
                distanceChart.YAxis.Stripes.Clear();
                Refresh();
            }
        }

        /// <summary>
        /// Updates record selection and views when selected date on calender is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Calendar_SelectedChanged(object sender, EventArgs e)
        {
            TreeList tree;
            DateTime calDate, actDate;
            TreeList.TreeListNode categoryNode;
            int i = 0;

            calDate = PluginMain.GetApplication().Calendar.Selected;

            switch (tabcontrol.SelectedIndex)
            {
                case 1:
                    // TreeList to interact with
                    tree = treeDistance;

                    // Get selected category (categoryNode)
                    if (tree.SelectedItems.Count < 1) break;
                    categoryNode = tree.SelectedItems[0] as TreeList.TreeListNode;

                    // Select record on tree, within category node, and on calendar date
                    SelectRecord(tree, categoryNode, calDate);

                    break;

                case 0:
                    // TreeList to interact with
                    tree = treeExtremes;

                    // Get selected category (categoryNode)
                    if (tree.SelectedItems.Count < 1) break;
                    categoryNode = tree.SelectedItems[0] as TreeList.TreeListNode;

                    // Select record on tree, within category node, and on calendar date
                    SelectRecord(tree, categoryNode, calDate);

                    break;

                case 2:
                    List<RecordNode> recordsW;
                    List<RecordNode> selectedW = new List<RecordNode>();
                    tree = treeAllActivities;
                    tree.SelectedItems.Clear();

                    recordsW = tree.RowData as List<RecordNode>;

                    if (recordsW != null)
                    {
                        while (i < recordsW.Count)
                        {
                            actDate = recordsW[i].Record.StartTime.ToLocalTime().Date;
                            if (actDate == calDate)
                            {
                                // Select activity based on calendar date
                                tree.SelectedItemsChanged -= treeRecords_SelectedChanged;
                                selectedW.Add(recordsW[i]);
                                tree.SelectedItems = selectedW;
                                tree.SelectedItemsChanged += treeRecords_SelectedChanged;
                                break;
                            }

                            i++;
                        }
                    }
                    break;
            }
        }

        private static void CopyActivities(TreeList treeList)
        {
            treeList.CopyTextToClipboard(true, ",");
        }

        private void ListSettings(TreeType tType)
        {
            List<string> selected = new List<string>();
            List<string> unselected = new List<string>();

            // Collect selected and unselected columns
            List<TreeList.Column> columns = currentColumns;

            string[] settingsColumns = null;
            switch (tType)
            {
                case TreeType.WhereDoesItRank:
                    settingsColumns = Settings.UserData.UserColumns_AllActivites.Split(";".ToCharArray());
                    break;
                case TreeType.Distance:
                    settingsColumns = Settings.UserData.UserColumns_Distance.Split(";".ToCharArray());
                    break;
                case TreeType.Extremes:
                    settingsColumns = Settings.UserData.UserColumns_Extreme.Split(";".ToCharArray());
                    break;
            }

            foreach (string settingColumn in settingsColumns)
            {
                if (!string.IsNullOrEmpty(settingColumn))
                {
                    string[] column_data = settingColumn.Split("|".ToCharArray());
                    int currentIndex = columns.FindIndex(delegate(TreeList.Column c) { return c.Id == column_data[0]; });

                    if (currentIndex != -1)
                        selected.Add(columns[currentIndex].Text);
                }
            }

            foreach (TreeList.Column column in columns)
            {
                if (!selected.Contains(column.Text))
                {
                    unselected.Add(column.Text);
                }
            }

            // Sort unselected items to find items easily
            unselected.Sort();

            // Occurs when an item in the popup is selected
            //// ***************************************
            // Show Data Select Form

            // TODO: (LOW) Move this to the ST list picker form over RBs custom form.
            //ZoneFiveSoftware.Common.Visuals.Forms.ListSettingsDialog listDlg = new ZoneFiveSoftware.Common.Visuals.Forms.ListSettingsDialog();
            //listDlg.ThemeChanged(PluginMain.GetApplication().VisualTheme);
            //listDlg.AvailableColumns = unselected;
            //listDlg.SelectedColumns = selected;

            DataSelectForm data = new DataSelectForm(PluginMain.GetApplication().VisualTheme, selected, unselected);
            data.SetLabelSelected(Resources.Strings.Label_SelectedColumns + ":");
            data.Text = Resources.Strings.Label_ListSettings;
            data.SetIcon(CommonResources.Images.Table16);
            data.RefreshLabels();
            //// ***************************************

            if (data.ShowDialog() == DialogResult.OK)
            {
                foreach (TreeList.Column column in columns)
                {
                    // Show/Hide columns
                    if (data.Items.Contains(column.Text))
                    {
                        // Show column
                        column.CanSelect = true;
                    }
                    else
                    {
                        // Hide column
                        column.CanSelect = false;
                    }
                }

                string userColumns = string.Empty;

                // NOTE: This is the way to arrange items in a list.  Try to do this for the charts.
                foreach (string item in data.Items)
                {
                    bool added = false;

                    // Rearrange Columns
                    int currentIndex = columns.FindIndex(delegate(TreeList.Column c) { return c.Text == item; });
                    if (currentIndex != data.Items.IndexOf(item))
                    {
                        columns.Insert(data.Items.IndexOf(item), columns[currentIndex]);
                        columns.RemoveAt(currentIndex + 1);
                        currentIndex = columns.FindIndex(delegate(TreeList.Column c) { return c.Text == item; });
                    }

                    switch (tType)
                    {
                        case TreeType.WhereDoesItRank:
                            settingsColumns = Settings.UserData.UserColumns_AllActivites.Split(";".ToCharArray());
                            break;
                        case TreeType.Distance:
                            settingsColumns = Settings.UserData.UserColumns_Distance.Split(";".ToCharArray());
                            break;
                        case TreeType.Extremes:
                            settingsColumns = Settings.UserData.UserColumns_Extreme.Split(";".ToCharArray());
                            break;
                    }

                    foreach (string settingColumn in settingsColumns)
                    {
                        if (!string.IsNullOrEmpty(settingColumn))
                        {
                            string[] column_data = settingColumn.Split("|".ToCharArray());
                            if (column_data[0] == columns[currentIndex].Id)
                            {
                                userColumns += ";" + column_data[0] + "|" + column_data[1];
                                added = true;
                                break;
                            }
                        }
                    }
                    if (!added)
                    {
                        userColumns += ";" + columns[currentIndex].Id + "|" + columns[currentIndex].Width;
                    }
                }

                List<RecordSet> rs_all;

                switch (tType)
                {
                    case TreeType.WhereDoesItRank:
                        UserData.UserColumns_AllActivites = userColumns;
                        rs_all = ChartData.RankedRecords;
                        break;

                    case TreeType.Distance:
                        UserData.UserColumns_Distance = userColumns;
                        rs_all = GetRecordSet(currentTree);
                        break;

                    case TreeType.Extremes:
                        UserData.UserColumns_Extreme = userColumns;
                        rs_all = GetRecordSet(currentTree);
                        break;

                    default:
                        rs_all = new List<RecordSet>();
                        break;
                }

                InitializeTree(currentTree, tType);
            }

            data.Dispose();
        }

        private void tabcontrol_TabChanged(object sender, EventArgs e)
        {
            YaTabControl page = sender as GrayIris.Utilities.UI.Controls.YaTabControl;
            List<RecordNode> selected = new List<RecordNode>();
            List<RecordNode> records;
            DateTime calDate;
            TreeList tree;

            switch (page.SelectedTab.Tag as string)
            {
                case "tab_distance":
                    // TODO: (LOW) Set 'Distance' activity based on calender date

                    break;
                case "tab_extremes":
                    // TODO: (LOW) Set 'Extremes' activity based on calender date
                    break;

                case "tab_rank":
                    tree = treeAllActivities;
                    tree.SelectedItems.Clear();

                    records = tree.RowData as List<RecordNode>;
                    calDate = PluginMain.GetApplication().Calendar.Selected;

                    // Select appropriate record based on calendar
                    if (records != null)
                        SelectRecord(tree, records[0], calDate);
                    break;
            }

            RefreshCalendar();
        }

        private void selectFieldsButton_Click(object sender, EventArgs e)
        {
            ListSettingsDialog listDialog = new ListSettingsDialog();
            ICollection<IListColumnDefinition> available = new List<IListColumnDefinition>();

            available.Add(new ColumnDefinition(ColumnDefinition.cadenceID, 100));
            available.Add(new ColumnDefinition(ColumnDefinition.elevationID, 100));
            available.Add(new ColumnDefinition(ColumnDefinition.gradeID, 100));
            available.Add(new ColumnDefinition(ColumnDefinition.hrID, 100));
            available.Add(new ColumnDefinition(ColumnDefinition.powerID, 100));
            available.Add(new ColumnDefinition(ColumnDefinition.speedID, 100));

            List<string> selected = new List<string>();

            foreach (string s in UserData.DistanceChartItems)
            {
                foreach (ColumnDefinition column in available)
                {
                    if (s == column.Id)
                    {
                        selected.Add(column.Id);
                        break;
                    }
                }
            }

            listDialog.AvailableColumns = available;
            listDialog.SelectedColumns = selected;
            listDialog.Text = CommonResources.Text.LabelCharts;
            // TODO: (LOW) Localize "Selected Charts" label
            listDialog.SelectedItemListLabel = "Selected Charts";
            listDialog.AddButtonLabel = CommonResources.Text.ActionAdd;
            listDialog.AllowFixedColumnSelect = false;
            listDialog.AllowZeroSelected = false;
            listDialog.Icon = GetIcon(Resources.Images.select_more);

            listDialog.ThemeChanged(PluginMain.GetApplication().VisualTheme);

            if (listDialog.ShowDialog() == DialogResult.OK)
            {
                UserData.DistanceChartItems = listDialog.SelectedColumns;

                if (treeDistance.SelectedItems.Count > 0)
                {
                    RecordNode node = treeDistance.SelectedItems[0] as RecordNode;
                    if (node != null && node.IsRecordSet)
                    {
                        DrawActivityRankChart(((RecordNode)treeDistance.SelectedItems[0]).RecSet);
                    }
                    else if (node != null && node.IsRecord)
                    {
                        DrawChartSelectedRecords((ArrayList)treeDistance.SelectedItems);
                    }
                }
            }

            listDialog.Close();
            listDialog.Dispose();

            return;
        }

        internal void UserData_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "StartDateFilter" || e.PropertyName == "EndDateFilter")
            {
                ChartData.IsDirty = true;
                RefreshPage();
            }
        }

        private void tree_DoubleClick(object sender, EventArgs e)
        {
            MouseEventArgs mouse = (MouseEventArgs)e;
            TreeList tree = (TreeList)sender;

            if (mouse.Button == MouseButtons.Left && (mouse.Y > tree.HeaderRowHeight))
            {
                // Exit if nothing selected
                if (tree.SelectedItems.Count > 0)
                {
                    // Open the selected activity in the detail pane view
                    RecordNode node = tree.SelectedItems[0] as RecordNode;
                    if (node != null && node.IsRecord)
                    {
                        // TODO: ReferenceId is confusing... looks to be a unique Id, however it represents the CATEGORY Id
                        string bookmark = "id=" + node.Record.ActivityId;
                        PluginMain.GetApplication().ShowView(GUIDs.DailyActivities, bookmark);
                    }
                }
            }
        }

        private void bnr_MenuClicked(object sender, EventArgs e)
        {
            ActionBanner banner = sender as ActionBanner;
            mnuTopRec.Show(banner, new Point(banner.Right - 2, banner.Bottom), ToolStripDropDownDirection.BelowLeft);
        }

        private void topMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem selected = sender as ToolStripMenuItem;
            if (selected != null)
            {
                int qty = int.Parse((string)selected.Tag);
                UserData.NumRecords = qty;

                RefreshTree(ChartData.DistanceRecords, treeDistance, UserData.NumRecords, false, TreeType.Distance);
                RefreshTree(ChartData.ExtremeRecords, treeExtremes, UserData.NumRecords, false, TreeType.Extremes);
            }

            foreach (ToolStripMenuItem item in mnuTopRec.Items)
            {
                // Uncheck all other items.
                item.Checked = UserData.NumRecords == int.Parse((string)item.Tag);
            }
        }

        private void SystemPreferences_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Cause re-calculation when specific settings are changed.  Some of these may not be entirely necessary
            if (e.PropertyName == "DistanceUnits" || e.PropertyName == "ElevationUnits" ||
                e.PropertyName == "EnergyUnits" || e.PropertyName == "TemperatureUnits" ||
                e.PropertyName == "AnalysisSettings.IncludeStopped" || e.PropertyName == "AnalysisSettings.IncludePaused" ||
                e.PropertyName == "AnalysisSettings.CadenceCutoff" || e.PropertyName == "AnalysisSettings.PowerCutoff" ||
                e.PropertyName == "AnalysisSettings.SpeedSmoothingSeconds" || e.PropertyName == "AnalysisSettings.ElevationSmoothingSeconds" ||
                e.PropertyName == "AnalysisSettings.CadenceSmoothingSeconds" || e.PropertyName == "AnalysisSettings.HeartRateSmoothingSeconds" ||
                e.PropertyName == "AnalysisSettings.PowerSmoothingSeconds")
            {
                ChartData.IsSuperDirty = true;
                Units.LoadUnits();
            }
        }

        private void ChartData_ProgressTick(object sender, ProgressChangedEventArgs e)
        {
            if (prog != null && !prog.IsDisposed)
                prog.UpdateProgress(e.ProgressPercentage / 100f, e.UserState.ToString());
        }

        private void Activities_CollectionChanged(object sender, NotifyCollectionChangedEventArgs<IActivity> e)
        {
            foreach (IActivity activity in e.NewItems)
                ChartData.AddActivity(activity);

            foreach (IActivity activity in e.OldItems)
                ChartData.RemoveActivity(activity);
        }

    }
}
