namespace RecordBook.Data
{
    using RecordBook.UI.View;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.Diagnostics;
    using ZoneFiveSoftware.Common.Data;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Data.GPS;
    using ZoneFiveSoftware.Common.Data.Measurement;
    using ZoneFiveSoftware.Common.Visuals;
    using System.Reflection;
    using Microsoft.VisualBasic;
    using System.Drawing;
    using RecordBook.Resources;

    /// <summary>
    /// A set of records representing a record category
    /// </summary>
    class RecordSet : CollectionBase
    {
        public event CollectionChangeEventHandler CollectionChanged;

        #region Fields

        private RecordCategory category;
        private bool sorted = false;
        private static Dictionary<DateTime, double> oneDayDistance = new Dictionary<DateTime, double>();
        private static Dictionary<DateTime, TimeSpan> oneDayTime = new Dictionary<DateTime, TimeSpan>();

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a set of records for a single specified category
        /// </summary>
        /// <param name="category">Category in which to find records for</param>
        /// <param name="activities">List of activities to search</param>
        public RecordSet(RecordCategory category)
        {
            this.category = category;
        }

        #endregion

        #region Properties

        internal Record this[int index]
        {
            get { return (Record)this.List[index]; }
            set
            {
                this.List[index] = value;
                sorted = false;
            }
        }

        internal new int Count
        {
            get { return this.List.Count; }
        }

        /// <summary>
        /// The Category associated with the record
        /// </summary>
        public RecordCategory Category
        {
            get { return category; }
        }

        /// <summary>
        /// The Category name for this record set.
        /// </summary>
        public string CategoryName
        {
            get { return category.Name; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Populate entire record set with all activities in the logbook.
        /// </summary>
        public void AddRecords()
        {
            AddRecords(PluginMain.GetLogbook().Activities);
        }

        public void AddRecords(IEnumerable<IActivity> activities)
        {
            Record record;

            if (category.Type == RecordCategory.RecordType.Distance)
            {
                foreach (IActivity activity in activities)
                {
#if Debug
                    // NOTE: Here's a good debug point to investivate specific oddball activities
                    if (activity.StartTime.Add(activity.TimeZoneUtcOffset).Date == new DateTime(2012, 05, 28))
                    { }
#endif

                    if (CheckCategory(activity, category))
                    {
                        record = GetRecord(activity, category);

                        if (record != null)
                            Add(record);
                    }
                }

                // Assign rank to each record in this record set
                RankRecords();
            }
            else if (category.Type == RecordCategory.RecordType.AllActivities)
            {
                foreach (IActivity activity in activities)
                {
                    record = GetRecord(activity, category);
                    if (record != null)
                        Add(record);
                }
            }
            else
            {
                // Store Extreme Overall Records
                foreach (IActivity activity in activities)
                {
#if Debug
                    // NOTE: Here's a good debug point to investivate specific oddball activities
                    if (activity.StartTime.Add(activity.TimeZoneUtcOffset).Date == new DateTime(2012, 05, 28))
                    { }
#endif

                    record = GetExtremeOverallRecord(activity, category);
                    if (record != null)
                        Add(record);
                }

                RankRecords();
            }
        }

        /// <summary>
        /// Gets the custom formatted text with proper decimal places, etc.
        /// Values are ready for user display (converted to proper units etc.)
        /// </summary>
        /// <param name="column">Column defines the property value to get</param>
        /// <returns>Returns custom formatted string ready for display to user.</returns>
        public string GetFormattedText(string Id)
        {
            return GetFormattedText(this, Id);
        }

        /// <summary>
        /// Gets the custom formatted text with proper decimal places, etc.  
        /// Values are ready for user display (converted to proper units etc.)
        /// </summary>
        /// <param name="activity">Activity containing value</param>
        /// <param name="column">Column defines the property value to get</param>
        /// <returns>Returns custom formatted string ready for display to user.</returns>
        public static string GetFormattedText(RecordSet recordSet, string Id)
        {
            Type recordType = typeof(RecordSet);  // Used to collect property value from activity
            string text = null;                         // text to display in cell (if defined)

            if (Id == "Rank")
            {
                return recordSet.CategoryName;
            }
            else
            {
                // Default
                PropertyInfo info = recordType.GetProperty(Id);

                if (info != null)
                {
                    object value = info.GetValue(recordSet, null);
                    if (value != null)
                    {
                        text = value.ToString();
                    }
                }
            }

            return text;
        }

        /// <summary>
        /// Checks to see if the activity's category is in the list
        /// </summary>
        /// <param name="act">Activity to check</param>
        /// <param name="cat">RecordCategory to check against</param>
        /// <returns></returns>
        public static bool CheckCategory(IActivity act, RecordCategory cat)
        {
            if (cat.Display_Categories != null && cat.Display_Categories.Count > 0)
            {
                foreach (string refID in cat.Categories)
                {
                    if (refID == act.Category.ReferenceId)
                    {
                        return true;
                    }
                }

                return false;
            }

            return true;
        }

        public void WriteRecordsToCSV()
        {
            WriteRecordsToCSV(this);
        }

        public static void WriteRecordsToCSV(RecordSet records)
        {
            System.IO.StreamWriter csvWriter = new System.IO.StreamWriter("c:\\temp\\records.csv", true);
            csvWriter.WriteLine("Record_Category, Rec_Cat_Meters, Location, Actual_Rec_Distance, AvgHR, MaxHR, From_Time, To_Time, From_Distance, To_Distance");
            foreach (Record r in records)
            {
                csvWriter.WriteLine(r.CategoryName + "," + r.Category.Meters + "," + r.Location + "," + r.TotalDistanceMeters + "," + r.AvgHR + "," + r.MaxHR + "," + r.StartTime + "," + r.EndTime + "," + r.StartDistanceMeters + "," + r.EndDistanceMeters);
            }

            csvWriter.Close();
        }

        /// <summary>
        /// Adds a record to the list.
        /// </summary>
        /// <param name="record">Record to add</param>
        public void Add(Record record)
        {
            this.List.Add(record);
            sorted = false;

            if (CollectionChanged != null)
            {
                CollectionChanged.Invoke(this, new CollectionChangeEventArgs(CollectionChangeAction.Add, record));
            }
        }

        public void Remove(Record record)
        {
            this.List.Remove(record);

            if (CollectionChanged != null)
            {
                CollectionChanged.Invoke(this, new CollectionChangeEventArgs(CollectionChangeAction.Remove, record));
            }
        }

        public bool Contains(object value)
        {
            return this.List.Contains(value);
        }

        /// <summary>
        /// Determines the index of a specific record in the list.
        /// </summary>
        /// <param name="phase">The record to locate in the list</param>
        /// <returns></returns>
        public int IndexOf(Record record)
        {
            return List.IndexOf(record);
        }

        protected override void OnValidate(object value)
        {
            base.OnValidate(value);
            if (!(value is Record))
            {
                throw new ArgumentException("Collection only supports Record objects");
            }
        }

        public void Sort()
        {
            if (!sorted)
            {
                Sort(new Record.RecordComparer(this.category.Type));
                sorted = true;
            }
        }

        public void Sort(Record.RecordComparer comparer)
        {
            this.InnerList.Sort(comparer);
        }

        /// <summary>
        /// Assign/Update records rank
        /// </summary>
        /// <param name="comparer"></param>
        /// <returns></returns>
        internal void RankRecords()
        {
            // Rank records
            Sort();

            for (int i = 0; i < Count; i++)
                this[i].Rank = i + 1;
        }

        /// <summary>
        /// Reset Total for One Day records.  This will cause recalculation next time the record is calculated
        /// </summary>
        internal static void ResetTotals()
        {
            // TODO: RecordSet reset needs to occur when categories change, or settings change.  Test this
            oneDayDistance = new Dictionary<DateTime, double>();
            oneDayTime = new Dictionary<DateTime, TimeSpan>();
        }

        /// <summary>
        /// Returns the category record for a single activity
        /// </summary>
        /// <param name="activity">Activity to search for a record in</param>
        /// <param name="category">Type/category of record to search for</param>
        /// <returns></returns>
        internal static Record GetRecord(IActivity activity, RecordCategory category)
        {
            switch (category.Type)
            {
                case RecordCategory.RecordType.Distance:
                    return GetDistancePaceRecord(activity, category);
                case RecordCategory.RecordType.AllActivities:
                    return GetAllActivities(activity, category);
                default:
                    // Category should always be defined
                    return null;
            }
        }

        internal static Record GetDistancePaceRecord(IActivity activity, RecordCategory category)
        {
            float maxSpeed = 0;
            float currentSpeed;
            float currentDistance = 0;
            ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);

            bool usingGpsTrack = activity.GPSRoute != null && activity.GPSRoute.TotalElapsedSeconds > 0 && info.DistanceMetersMoving >= category.Meters;
            bool usingDistanceTrack = activity.DistanceMetersTrack != null && activity.DistanceMetersTrack.Max >= category.Meters;

            if (usingGpsTrack || usingDistanceTrack)
            {
                int recordStart = 0, recordEnd = 0, startIndex, endIndex = 0;
                int count = usingGpsTrack ? activity.GPSRoute.Count : activity.DistanceMetersTrack.Count;

                // Go through each starting point
                for (startIndex = 0; startIndex < count; startIndex++)
                {
                    // Find end GPS point that's the proper distance away
                    while (currentDistance <= category.Meters)
                    {
                        endIndex += 1;

                        // Typical return point.  End has exceeded route.  Construct and return record for this activity/category.
                        if (endIndex >= count)
                        {
                            // Construct record GPS route
                            GPSRoute recordGPS = null;

                            if (usingGpsTrack)
                            {
                                recordGPS = new GPSRoute();
                            }

                            NumericTimeDataSeries recordHRTrack = new NumericTimeDataSeries();
                            NumericTimeDataSeries pwrTrack = new NumericTimeDataSeries();
                            NumericTimeDataSeries elevTrack = new NumericTimeDataSeries();
                            NumericTimeDataSeries cadTrack = new NumericTimeDataSeries();
                            DistanceDataTrack distTrack = new DistanceDataTrack();

                            for (int i = recordStart; i <= recordEnd; i++)
                            {
                                // Record information/statistics
                                DateTime time;
                                if (usingGpsTrack)
                                {
                                    time = activity.GPSRoute.EntryDateTime(activity.GPSRoute[i]);
                                    recordGPS.Add(time, activity.GPSRoute[i].Value);
                                }
                                else
                                {
                                    time = activity.DistanceMetersTrack.EntryDateTime(activity.DistanceMetersTrack[i]);
                                }

                                if (activity.HeartRatePerMinuteTrack != null
                                    && activity.HeartRatePerMinuteTrack.StartTime <= time
                                    && activity.HeartRatePerMinuteTrack.StartTime.AddSeconds(activity.HeartRatePerMinuteTrack.TotalElapsedSeconds) >= time)
                                {
                                    recordHRTrack.Add(time, activity.HeartRatePerMinuteTrack.GetInterpolatedValue(time).Value);
                                }

                                if (activity.PowerWattsTrack != null
                                    && activity.PowerWattsTrack.StartTime <= time
                                    && activity.PowerWattsTrack.StartTime.AddSeconds(activity.PowerWattsTrack.TotalElapsedSeconds) >= time)
                                {
                                    pwrTrack.Add(time, activity.PowerWattsTrack.GetInterpolatedValue(time).Value);
                                }

                                if (activity.CadencePerMinuteTrack != null
                                    && activity.CadencePerMinuteTrack.StartTime <= time
                                    && activity.CadencePerMinuteTrack.StartTime.AddSeconds(activity.CadencePerMinuteTrack.TotalElapsedSeconds) >= time)
                                {
                                    cadTrack.Add(time, activity.CadencePerMinuteTrack.GetInterpolatedValue(time).Value);
                                }

                                if (activity.DistanceMetersTrack != null
                                    && activity.DistanceMetersTrack.StartTime <= time
                                    && activity.DistanceMetersTrack.StartTime.AddSeconds(activity.DistanceMetersTrack.TotalElapsedSeconds) >= time)
                                {
                                    distTrack.Add(time, activity.DistanceMetersTrack.GetInterpolatedValue(time).Value);
                                }

                                if (activity.ElevationMetersTrack != null
                                    && activity.ElevationMetersTrack.StartTime <= time
                                    && activity.ElevationMetersTrack.StartTime.AddSeconds(activity.ElevationMetersTrack.TotalElapsedSeconds) >= time)
                                {
                                    elevTrack.Add(time, activity.ElevationMetersTrack.GetInterpolatedValue(time).Value);
                                }
                            }

                            // Return record
                            Record record = new Record(activity, category, recordGPS, recordHRTrack, pwrTrack, cadTrack, distTrack, elevTrack);
                            return record;
                        }
                        else
                        {
                            // Add to end of route until category distance is found
                            if (usingGpsTrack)
                                currentDistance += activity.GPSRoute[endIndex].Value.DistanceMetersToPoint(activity.GPSRoute[endIndex - 1].Value);
                            else
                                currentDistance = activity.DistanceMetersTrack[endIndex].Value - activity.DistanceMetersTrack[startIndex].Value;
                        }
                    }

                    if (usingGpsTrack)
                    {
                        // In meters / second
                        currentSpeed = currentDistance / (activity.GPSRoute[endIndex].ElapsedSeconds - activity.GPSRoute[startIndex].ElapsedSeconds);
                        // Remove first point from routeDistance, and go to next starting point.
                        currentDistance -= activity.GPSRoute[startIndex].Value.DistanceMetersToPoint(activity.GPSRoute[startIndex + 1].Value);
                    }
                    else
                    {
                        // In meters / second
                        currentSpeed = currentDistance / (activity.DistanceMetersTrack[endIndex].ElapsedSeconds - activity.DistanceMetersTrack[startIndex].ElapsedSeconds);
                        // Remove first point from routeDistance, and go to next starting point.
                        currentDistance = activity.DistanceMetersTrack[endIndex].Value - activity.DistanceMetersTrack[startIndex + 1].Value;
                    }

                    // Store fastest info (in meters / second)
                    if (maxSpeed < currentSpeed)
                    {
                        maxSpeed = currentSpeed;
                        recordStart = startIndex;
                        recordEnd = endIndex;
                    }
                }
            }
            else if (info.DistanceMeters >= category.Meters && info.Time.TotalSeconds > 0)
            {
                // NOTE: Manually entered distance record activities
                double metersPerSec = info.DistanceMeters / info.Time.TotalSeconds;
                DateTime endTime = activity.StartTime.AddSeconds(category.Meters / metersPerSec);
                Record record = new Record(activity, category);

                return record;
            }

            // Activity does not contain GPS data and not long enough
            return null;
        }

        internal static Record GetAllActivities(IActivity activity, RecordCategory category)
        {
            Record record = new Record(activity, category);
            return record;
        }

        internal static Record GetExtremeOverallRecord(IActivity activity, RecordCategory category)
        {
            Record record = null;
            ArrayList allAct = new ArrayList();

            if (CheckCategory(activity, category))
            {
                switch (category.Type)
                {
                    case RecordCategory.RecordType.TotalTimeOneDay:
                        record = GetOneDayTotalRecord(activity, category);
                        break;

                    case RecordCategory.RecordType.TotalDistanceOneDay:
                        record = GetOneDayTotalRecord(activity, category);
                        break;

                    default:
                        record = new Record(activity, category);
                        record.RecValue = record.GetFormattedText(category.GetValuePropertyName(), true);
                        break;
                }
            }

            if (record != null && !string.IsNullOrEmpty(record.RecValue))
            {
                return record;
            }
            else
            {
                return null;
            }          
        }

        internal static Record GetOneDayTotalRecord(IActivity activity, RecordCategory category)
        {
            Record record = null;
            DateTime date;

            if (category.Type == RecordCategory.RecordType.TotalDistanceOneDay)
            {
                // Initialize dictionary if required
                if (oneDayDistance.Count == 0 && 0 < PluginMain.GetLogbook().Activities.Count)
                {
                    foreach (IActivity item in PluginMain.GetLogbook().Activities)
                    {
                        date = item.StartTime.Add(item.TimeZoneUtcOffset).Date;
                        if (CheckCategory(item, category))
                        {
                            if (oneDayDistance.ContainsKey(date))
                            {
                                oneDayDistance[date] += ActivityInfoCache.Instance.GetInfo(item).DistanceMeters;
                            }
                            else
                            {
                                oneDayDistance.Add(date, ActivityInfoCache.Instance.GetInfo(item).DistanceMeters);
                            }
                        }
                    }
                }

                // Lookup and record Record.
                date = activity.StartTime.Add(activity.TimeZoneUtcOffset).Date;

                if (oneDayDistance.ContainsKey(date))
                {
                    record = new Record(activity, category);
                    record.TotalDistanceOneDay = (float)oneDayDistance[date];
                    record.RecValue = record.GetFormattedText("TotalDistanceOneDay", true);
                }
                else
                {
                    record = null;
                }
            }
            else if (category.Type == RecordCategory.RecordType.TotalTimeOneDay)
            {
                // Initialize dictionary if required
                if (oneDayTime.Count == 0 && 0 < PluginMain.GetLogbook().Activities.Count)
                {
                    foreach (IActivity item in PluginMain.GetLogbook().Activities)
                    {
                        date = item.StartTime.Add(item.TimeZoneUtcOffset).Date;
                        if (CheckCategory(item, category))
                        {
                            if (oneDayTime.ContainsKey(date))
                            {
                                oneDayTime[date] += ActivityInfoCache.Instance.GetInfo(item).Time;
                            }
                            else
                            {
                                oneDayTime.Add(date, ActivityInfoCache.Instance.GetInfo(item).Time);
                            }
                        }
                    }
                }

                // Lookup and record Record.
                date = activity.StartTime.Add(activity.TimeZoneUtcOffset).Date;

                if (oneDayTime.ContainsKey(date))
                {
                    record = new Record(activity, category);
                    record.TotalTimeOneDay = oneDayTime[date];
                    record.RecValue = record.GetFormattedText("TotalTimeOneDay", true);
                }
                else
                {
                    record = null;
                }
            }
            else
            {
                throw new ArgumentException();
            }

            return record;
        }

        #endregion

        #region IEnumerable Members

        public new IEnumerator GetEnumerator()
        {
            return this.List.GetEnumerator();
        }

        #endregion
    }
}
