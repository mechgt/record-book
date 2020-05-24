using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Visuals;
using RecordBook.Data;
using ZoneFiveSoftware.Common.Data.Fitness;
using System.Drawing;
using RecordBook.Settings;
using System.Windows.Forms;

namespace RecordBook.UI.View
{
    partial class RecordBookControl
    {

        /// <summary>
        /// BuildActivitiesColumns will setup the column structure for the 
        /// main/upper Where Does it Rank treelist
        /// </summary>
        /// <returns></returns>
        private static List<TreeList.Column> GetActivitiesColumns()
        {
            // Setup the Distance columns
            List<TreeList.Column> columns = new List<TreeList.Column>();

            columns.Add(new TreeList.Column("StartTime", CommonResources.Text.LabelStartTime, 80, StringAlignment.Near));
            columns.Add(new TreeList.Column("Location", CommonResources.Text.LabelLocation, 80, StringAlignment.Near));
            columns.Add(new TreeList.Column("Name", CommonResources.Text.LabelName, 80, StringAlignment.Near));
            columns.Add(new TreeList.Column("ActivityCategory", CommonResources.Text.LabelCategory, 80, StringAlignment.Near));
            columns.Add(new TreeList.Column("TotalDistanceMeters", CommonResources.Text.LabelDistance + " (" + Units.Distance + ")", 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("TotalTime", CommonResources.Text.LabelTime, 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("AvgPace", CommonResources.Text.LabelAvgPace + " (" + Units.Pace + ")", 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("AvgHR", CommonResources.Text.LabelAvgHR, 80, StringAlignment.Far));

            if (CustomDataFields.TRIMPexists)
                columns.Add(new TreeList.Column("TRIMP", Resources.Strings.Label_MaxTRIMP, 80, StringAlignment.Far));

            columns.Add(new TreeList.Column("EndTime", Resources.Strings.Label_End, 80, StringAlignment.Near));
            columns.Add(new TreeList.Column("Temp", CommonResources.Text.LabelTemperature + " (" + Units.Temp + ")", 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("ElevationChange", CommonResources.Text.LabelElevationChange + " (" + Units.Elevation + ")", 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("MaxHR", CommonResources.Text.LabelMaxHR, 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("MetersPerSecond", CommonResources.Text.LabelSpeed + " (" + Units.Speed + ")", 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("TotalAscend", CommonResources.Text.LabelAscending + " (" + Units.Elevation + ")", 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("TotalDescend", CommonResources.Text.LabelDescending + " (" + Units.Elevation + ")", 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("MaxSpeed", CommonResources.Text.LabelFastestSpeed + " (" + Units.Speed + ")", 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("MaxElevation", Resources.Strings.Label_MaxElevation + " (" + Units.Elevation + ")", 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("MinElevation", Resources.Strings.Label_MinElevation + " (" + Units.Elevation + ")", 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("MaxCadence", CommonResources.Text.LabelMaxCadence, 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("AvgCadence", CommonResources.Text.LabelAvgCadence, 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("MaxPower", CommonResources.Text.LabelMaxPower, 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("AvgPower", CommonResources.Text.LabelAvgPower, 80, StringAlignment.Far));

            if (CustomDataFields.TSSexists)
                columns.Add(new TreeList.Column("TSS", Resources.Strings.Label_MaxTSS, 80, StringAlignment.Far));

            columns.Add(new TreeList.Column("MaxGrade", CommonResources.Text.LabelMaxGrade, 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("AvgGrade", CommonResources.Text.LabelAvgGrade, 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("MinGrade", Resources.Strings.Label_MinGrade, 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("TotalCalories", CommonResources.Text.LabelCalories, 80, StringAlignment.Far));

            string[] userColumns = UserData.UserColumns_AllActivites.Split(";".ToCharArray());

            foreach (TreeList.Column column in columns)
            {
                foreach (string columnID in userColumns)
                {
                    string[] column_data = columnID.Split("|".ToCharArray());
                    if (column.Id == column_data[0])
                    {
                        column.CanSelect = true;
                    }
                }
            }

            return columns;
        }

        /// <summary>
        /// BuildTimeTree will build the column structure for the 'Then' tree
        /// </summary>
        private static void BuildNowThenTree(TreeList tree)
        {
            List<TreeList.Column> columns = new List<TreeList.Column>();
            columns.Add(new TreeList.Column("Name", CommonResources.Text.LabelCategory, 150, StringAlignment.Far));
            columns.Add(new TreeList.Column("RecValue", CommonResources.Text.LabelValue, 150, StringAlignment.Far));
            columns.Add(new TreeList.Column("Rank", Resources.Strings.Label_Rank, 80, StringAlignment.Far));

            // Add columns to Activity treeList
            tree.Columns.Clear();
            foreach (TreeList.Column column in columns)
            {
                tree.Columns.Add(column);
            }
        }

        private void AddRecordToNowThenTree(RecordNode record, DisplayRecord.recordTime type)
        {
            this.Cursor = Cursors.WaitCursor;
            TreeList tree;
            if (type == DisplayRecord.recordTime.Now)
            {
                tree = treeNow;
            }
            else
            {
                tree = treeTime;
            }

            tree.SelectedItemsChanged -= treeRecords_SelectedChanged;

            List<DisplayRecord> drList = new List<DisplayRecord>();
            List<RecordNode> nowThenCategories = new List<RecordNode>();

            // If there are saved settings, display from them
            if (Settings.UserData.NowThenRecordCategories.Count > 0)
            {
                // Parse through each category setting
                foreach (RecordCategory cat in Settings.UserData.NowThenRecordCategories)
                {
                    // Check to see if the current record is in the list
                    // TODO: ReferenceId is confusing... looks to be a unique Id, however it represents the CATEGORY Id
                    if (cat.Categories.Contains(record.Record.Activity.Category.ReferenceId))
                    {
                        DisplayRecord dr = new DisplayRecord(record, cat.Type, ChartData.RankedRecords[0], type, string.Empty, cat.Categories, cat.Name);
                        drList.Add(dr);
                    }
                }
            }

            // Add the distance records
            for (int i = 0; i < ChartData.DistanceRecords.Count; i++)
            {
                // Only add it if the event created a record for that distance
                DisplayRecord dr = new DisplayRecord(record, type, ChartData.DistanceRecords, i, string.Empty);

                if (dr.RecValue != "-")
                {
                    drList.Add(dr);
                }
            }

            tree.RowData = drList;
            tree.SelectedItemsChanged += treeRecords_SelectedChanged;
            this.Cursor = Cursors.Default;
        }
    }
}
