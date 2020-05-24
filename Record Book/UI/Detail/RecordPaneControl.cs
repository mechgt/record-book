namespace RecordBook.UI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Visuals;
    using ZoneFiveSoftware.Common.Visuals.Fitness;
    using RecordBook.Data;

    public partial class RecordPaneControl : UserControl
    {
        private IActivity activity;

        #region Constructor

        public RecordPaneControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        public IActivity Activity
        {
            get { return activity; }
            set { activity = value; }
        }

        #endregion

        public void RefreshPage()
        {
            // Setup Tree
            List<TreeList.Column> columns_now = new List<TreeList.Column>();
            columns_now.Add(new TreeList.Column("Name", "Criteria", 150, StringAlignment.Far));
            columns_now.Add(new TreeList.Column("RecValue", "Value", 150, StringAlignment.Far));
            //columns_now.Add(new TreeList.Column("Rank", "Rank", 80, StringAlignment.Far));

            treeActivity.Columns.Clear();
            foreach (TreeList.Column column in columns_now)
            {
                treeActivity.Columns.Add(column);
            }

            if (activity != null)
            {
                RecordNode record = new RecordNode(null, new Record(activity, new RecordCategory()));

                List<DisplayRecord> drList = new List<DisplayRecord>();

                // If there are saved settings, display from them
                List<RecordCategory> extremes = Settings.UserData.NowThenRecordCategories;

                treeActivity.RowData = drList;
            }
        }

        public void ThemeChanged(ITheme visualTheme)
        {
            actionBanner1.ThemeChanged(visualTheme);
            treeActivity.ThemeChanged(visualTheme);
        }

        public void UICultureChanged(System.Globalization.CultureInfo culture)
        {
            actionBanner1.Text = Resources.Strings.Label_RecordBook;
        }

        //private void AddRecordToTimeTree(RecordWrapper record)
        //{
        //    List<DisplayRecord> drList = new List<DisplayRecord>();

        //    // If there are saved settings, display from them
        //    if (!string.IsNullOrEmpty(Settings.UserData.NowThenRecordCategories))
        //    {
        //        string extFromLog = Settings.UserData.NowThenRecordCategories.Trim(';');
        //        string[] extremes = extFromLog.Split(";".ToCharArray());

        //        // Parse through each category setting
        //        foreach (string ext in extremes)
        //        {
        //            string[] recs = ext.Split("|".ToCharArray());

        //            // List the categories selected
        //            List<string> refIDs = new List<string>();
        //            bool found = false;

        //            // Check to see if the current record is in the list
        //            for (int i = 2; i < recs.Length; i++)
        //            {
        //                if (recs[i] == record.ReferenceID)
        //                {
        //                    found = true;
        //                }
        //                refIDs.Add(recs[i]);
        //            }

        //            // If the record is in the list, calculate the results
        //            if (found)
        //            {
        //                DisplayRecord dr = new DisplayRecord(record, (DisplayRecord.recordType)Convert.ToInt16(recs[0]), rankMaster[0].Records, DisplayRecord.recordTime.Then, string.Empty, refIDs, recs[1]);
        //                drList.Add(dr);
        //            }
        //        }
        //    }

        //    // Add the distance records
        //    for (int i = 0; i < distanceMaster.Count; i++)
        //    {
        //        // Only add it if the event created a record for that distance
        //        DisplayRecord dr = new DisplayRecord(record, DisplayRecord.recordTime.Then, distanceMaster, i, string.Empty);
        //        if (dr.RecValue != "-")
        //        {
        //            drList.Add(dr);
        //        }
        //    }

        //    treeActivity.RowData = drList;
        //}
    }
}