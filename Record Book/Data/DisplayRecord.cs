namespace RecordBook.Data
{
    using RecordBook.UI.View;
    using RecordBook;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using ZoneFiveSoftware.Common.Data;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Data.GPS;
    using ZoneFiveSoftware.Common.Data.Measurement;
    using ZoneFiveSoftware.Common.Visuals;

    /// <summary>
    /// DisplayRecord is a class used to display a record's data in a row format instead of a column format
    /// </summary>
    class DisplayRecord : TreeList.TreeListNode
    {
        #region Fields

        private string name;
        private string recValue;
        private string rank;
        private RecordCategory.RecordType type;
        private List<string> categories;

        #endregion

        #region Enumerations

        public enum recordTime
        {
            Now,
            Then
        }

        #endregion

        #region Constructors

        public DisplayRecord(RecordNode parent, string element)
            : base(parent, element)
        {
        }

        /// <summary>
        /// Constructor to build the display record and rank it 'now' and 'then'
        /// </summary>
        /// <param name="parent">The current record</param>
        /// <param name="recList">The list of all the records to compare against</param>
        /// <param name="recType">Is it a 'now' or 'then' record</param>
        public DisplayRecord(RecordNode parent, RecordCategory.RecordType recType, RecordSet recList, recordTime recTime, string element, List<string> refIDs, string displayName)
            : base(parent, element)
        {
            // Set the display name and record value
            name = displayName;
            string propertyName = RecordCategory.GetValuePropertyName(recType);
            recValue = parent.Record.GetFormattedText(propertyName, true);

            // TODO: (HIGH) If the record is 0, don't rank it
            if (recValue == "0.00")
            {
                rank = "-";
                recValue = "-";
            }
            else
            {
                // Set the rank
                rank = GetRank(parent, recType, recList, refIDs, recTime).ToString();
            }
        }

        public DisplayRecord(RecordNode parent, recordTime recT, List<RecordSet> master_recordset, int listIndex, string element)
            : base(parent, element)
        {
            // Display prefs for distance, pace, speed units
            Length.Units units = PluginMain.GetApplication().SystemPreferences.DistanceUnits;
            string spdPaceUnits = " " + Speed.Label(parent.Record.SpeedUnits, new Length(1, units), false);

            name = master_recordset[listIndex].CategoryName;
            recValue = "-";
            rank = "-";

            if (recT == recordTime.Now)
            {
                for (int i = 0; i < master_recordset[listIndex].Count; i++)
                {
#if Debug
                    if (master_recordset[listIndex][i].StartTime.Date == new DateTime(2012, 5, 28))
                    { }
#endif
                    if (parent.Record.StartTime == master_recordset[listIndex][i].TrueStartDate)
                    {
                        if (parent.Record.SpeedUnits == Speed.Units.Pace)
                        {
                            recValue = string.Format("({0}) - {1} {2}", Utilities.ToTimeString(master_recordset[listIndex][i].TotalTime), Utilities.ToTimeString(master_recordset[listIndex][i].AvgPace), spdPaceUnits);
                        }
                        else
                        {
                            recValue = string.Format("({0}) - {1}", Utilities.ToTimeString(master_recordset[listIndex][i].TotalTime), master_recordset[listIndex][i].GetFormattedText("MetersPerSecond", true));
                        }

                        rank = master_recordset[listIndex][i].Rank.ToString();
                        break;
                    }
                }
            }
            else if (recT == recordTime.Then)
            {
                int recRank = -1;
                int recsBefore = 1;
                for (int i = 0; i < master_recordset[listIndex].Count; i++)
                {
                    if (Convert.ToDateTime(parent.Record.StartTime) == master_recordset[listIndex][i].TrueStartDate)
                    {
                        if (parent.Record.SpeedUnits == Speed.Units.Pace)
                        {
                            recValue = string.Format("({0}) - {1} {2}", Utilities.ToTimeString(master_recordset[listIndex][i].TotalTime), Utilities.ToTimeString(master_recordset[listIndex][i].AvgPace), spdPaceUnits);
                        }
                        else
                        {
                            recValue = string.Format("({0}) - {1}", Utilities.ToTimeString(master_recordset[listIndex][i].TotalTime), master_recordset[listIndex][i].GetFormattedText("MetersPerSecond", true));
                        }

                        recRank = master_recordset[listIndex][i].Rank;
                        break;
                    }
                    else if (Convert.ToDateTime(parent.Record.StartTime) > master_recordset[listIndex][i].TrueStartDate)
                    {
                        recsBefore++;
                    }
                }

                if (recRank != -1)
                {
                    rank = recsBefore.ToString();
                }
            }
        }

        #endregion

        #region Properties

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string RecValue
        {
            get
            {
                return recValue;
            }
        }

        public string Rank
        {
            get { return rank; }
        }

        /// <summary>
        /// The type of record stored as an enumeration
        /// </summary>
        public RecordCategory.RecordType Type
        {
            get { return type; }
            set { type = value; }
        }

        public string Display_RecordType
        {
            get
            {
                return RecordCategory.DisplayValue(type);
            }
        }

        /// <summary>
        /// Categories will store an ArrayList of ST categories for which this record type applies
        /// </summary>
        public List<string> Categories
        {
            get { return categories; }
            set { categories = value; }
        }

        public string Display_Categories
        {
            get
            {
                string display = string.Empty;
                foreach (IActivityCategory cat in PluginMain.GetApplication().Logbook.ActivityCategories)
                {
                    GetActivityCategories(cat, ref display);
                }

                if (display.Length > 2)
                {
                    display = display.Substring(2);
                }

                return display;
            }
        }

        #endregion

        private int GetRank(RecordNode parent, RecordCategory.RecordType recType, RecordSet recList, List<string> refIDs, recordTime recTime)
        {
            int iRank = 1;
            Record.RecordComparer comparer = new Record.RecordComparer(recType);

            // Parse through the record list and find the 'now' or 'then' ranking of the current record
            for (int i = 0; i < recList.Count; i++)
            {
                if (refIDs.Contains(recList[i].Activity.Category.ReferenceId))
                {
                    // 'Then' record.  If the current record occurred before the one in the list
                    if (parent.Record.StartTime > recList[i].StartTime
                        && recTime == recordTime.Then)
                    {
                        iRank++;
                        if (parent.Record.CompareTo(recList[i], comparer) < 0)
                            iRank--;
                    }

                    // 'Now' record.
                    else if (recTime == recordTime.Now
                            && parent.Record.StartTime != recList[i].StartTime)
                    {
                        iRank++;
                        if (parent.Record.CompareTo(recList[i], comparer) < 0)
                            iRank--;
                    }
                }
            }

            return iRank;
        }

        public void GetActivityCategories(IActivityCategory cat, ref string display)
        {
            if (cat.SubCategories.Count != 0)
            {
                for (int i = 0; i < cat.SubCategories.Count; i++)
                {
                    GetActivityCategories(cat.SubCategories[i], ref display);
                }
            }
            else
            {
                if (categories.Contains(cat.ReferenceId))
                {
                    display = display + ", " + cat.Parent.Name + ": " + cat.Name;
                }
            }
        }
    }
}
