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
    using System.Reflection;
    using System.Collections;
    using System.ComponentModel;

    /// <summary>
    /// Record will store 1 individual record of any type
    /// </summary>
    class Record : IComparable<Record>
    {
        #region Fields

        private int rank;
        private string recValue;
        private IActivity activity;
        private float startDistance;
        private float maxElevation;
        private float minElevation;
        private float trimp = float.NaN;
        private float tss = float.NaN;
        private RecordCategory category;
        private DateTime trueStartDate;
        private Guid id;
        private Guid partialActivityId;

        #endregion

        #region Constructors

        /// <summary>
        /// Used to generate the Distance record of the supplied activity, 
        /// where the data tracks below are a subset of the actual full
        /// activity.
        /// </summary>
        /// <param name="activity">The full activity for the record</param>
        /// <param name="category">The Record Category for this record</param>
        /// <param name="gpsTrack">The GPS route of the actual record</param>
        /// <param name="hrTrack">The HR track of the actual record</param>
        /// <param name="pwrTrack">The power track of the actual record</param>
        /// <param name="cadTrack">The cadence track of the actual record</param>
        public Record(IActivity activity, RecordCategory category, GPSRoute gpsTrack, NumericTimeDataSeries hrTrack, NumericTimeDataSeries pwrTrack, NumericTimeDataSeries cadTrack, DistanceDataTrack distTrack, NumericTimeDataSeries elevTrack)
        {
            // Create new activity from template
            IActivity recActivity = (IActivity)Activator.CreateInstance(activity.GetType());

            // HACK: (LOW) Manually Clone 'activity' until a better way is found
            recActivity.Category = activity.Category;
            recActivity.DistanceMetersTrack = distTrack;
            recActivity.ElevationMetersTrack = elevTrack;
            recActivity.GPSRoute = gpsTrack;
            recActivity.HasStartTime = activity.HasStartTime;
            recActivity.HeartRatePerMinuteTrack = hrTrack;
            recActivity.Intensity = activity.Intensity;
            recActivity.Location = activity.Location;
            recActivity.Name = activity.Name;
            recActivity.PowerWattsTrack = pwrTrack;
            recActivity.CadencePerMinuteTrack = cadTrack;
            partialActivityId = new Guid(activity.ReferenceId);
            recActivity.Weather.Conditions = activity.Weather.Conditions;
            recActivity.Weather.CurentDirectionDegrees = activity.Weather.CurentDirectionDegrees;
            recActivity.Weather.CurentSpeedKilometersPerHour = activity.Weather.CurentSpeedKilometersPerHour;
            recActivity.Weather.HumidityPercent = activity.Weather.HumidityPercent;
            recActivity.Weather.TemperatureCelsius = activity.Weather.TemperatureCelsius;
            recActivity.Weather.WindDirectionDegrees = activity.Weather.WindDirectionDegrees;
            recActivity.Weather.WindSpeedKilometersPerHour = activity.Weather.WindSpeedKilometersPerHour;
            recActivity.TimeZoneUtcOffset = activity.TimeZoneUtcOffset;

            this.trueStartDate = activity.StartTime;
            this.activity = recActivity;
            this.id = Guid.NewGuid();

            // Set the record category
            this.category = category;

            // Max and Min elevation seen over the route
            if (recActivity.GPSRoute != null && 0 < recActivity.GPSRoute.Count)
            {
                for (int i = 0; i < recActivity.GPSRoute.Count; i++)
                {
                    GPSPoint p = (GPSPoint)recActivity.GPSRoute[i].Value;
                    if (p.ElevationMeters > this.maxElevation)
                        this.maxElevation = p.ElevationMeters;

                    if (p.ElevationMeters < this.minElevation)
                        this.minElevation = p.ElevationMeters;
                }

                IDistanceDataTrack distance = activity.GPSRoute.GetDistanceMetersTrack();
                ITimeValueEntry<float> item = distance.GetInterpolatedValue(this.StartTime.Add(-activity.TimeZoneUtcOffset));
                if (item != null)
                    startDistance = item.Value;
                else
                { }

            }
            else if (recActivity.ElevationMetersTrack != null && 0 < recActivity.ElevationMetersTrack.Count)
            {
                // Collect elevation min/max from gpstrack since this isn't readily available
                this.maxElevation = recActivity.ElevationMetersTrack.Max;
                this.minElevation = recActivity.ElevationMetersTrack.Min;

                ITimeValueEntry<float> item = activity.DistanceMetersTrack.GetInterpolatedValue(this.StartTime.Add(-activity.TimeZoneUtcOffset));
                if (item != null)
                    startDistance = item.Value;
                else
                { }
            }

            activity.PropertyChanged += new PropertyChangedEventHandler(activity_PropertyChanged);
        }

        /// <summary>
        /// Typical Record constructor
        /// </summary>
        /// <param name="activity"></param>
        /// <param name="category"></param>
        public Record(IActivity activity, RecordCategory category)
        {
            this.activity = activity;
            this.category = category;
            this.id = Guid.NewGuid();

#if Debug
            // NOTE: Here's a good debug point to investivate specific oddball activities
            if (activity.StartTime.Add(activity.TimeZoneUtcOffset).Date == new DateTime(2012, 05, 28))
            { }
#endif

            if (activity != null)
            {
                // TODO: (MED) If GPS not available, maybe get from ElevationMetersTrack if available
                // Max and Min elevation seen over the route
                if (activity.GPSRoute != null && activity.GPSRoute.Count > 0)
                {
                    // Manually collect elevation min/max from gpstrack since this isn't readily available
                    this.maxElevation = 0;
                    this.minElevation = float.MaxValue;
                    for (int i = 0; i < activity.GPSRoute.Count; i++)
                    {
                        GPSPoint p = activity.GPSRoute[i].Value as GPSPoint;
                        if (p.ElevationMeters > this.maxElevation)
                            this.maxElevation = p.ElevationMeters;

                        if (p.ElevationMeters < this.minElevation)
                            this.minElevation = p.ElevationMeters;
                    }

                    if (this.minElevation == float.MaxValue)
                    {
                        this.minElevation = 0;
                        return;
                    }
                }
                else if (activity.ElevationMetersTrack != null && 0 < activity.ElevationMetersTrack.Count)
                {
                    // Collect elevation min/max from gpstrack since this isn't readily available
                    this.maxElevation = activity.ElevationMetersTrack.Max;
                    this.minElevation = activity.ElevationMetersTrack.Min;
                }

                activity.PropertyChanged += new PropertyChangedEventHandler(activity_PropertyChanged);
            }
        }

        #endregion

        #region User Set Properties

        /// <summary>
        /// The name of the record (used for overall record types)
        /// </summary>
        public string Name
        {
            get { return activity.Name; }
        }

        /// <summary>
        /// The rank of the record for the top x list (1st, 2nd, etc)
        /// </summary>
        public int Rank
        {
            get { return rank; }
            set { rank = value; }
        }

        /// <summary>
        /// The value of the record (used for overall record types)
        /// </summary>
        public string RecValue
        {
            get { return recValue; }
            set { recValue = value; }
        }

        public TimeSpan TotalTimeOneDay
        { get; set; }

        public float TotalDistanceOneDay
        { get; set; }

        #endregion

        #region Class Calculated Properties

        /// <summary>
        /// Activity containing or related to this record
        /// </summary>
        public IActivity Activity
        {
            get { return activity; }
            set { activity = value; }
        }

        public string ActivityCategory
        {
            get
            {
                IActivityCategory category = activity.Category;
                string catString = category.Name;
                if (category.Parent != null)
                {
                    while (category.Parent.Parent != null)
                    {
                        category = category.Parent;
                        catString = category.Name + ": " + catString;
                    }
                }

                return catString;
            }
        }

        /// <summary>
        /// The actual distance for the record (in meters)
        /// </summary>
        public float TotalDistanceMeters
        {
            get
            {
                double distance = ActivityInfoCache.Instance.GetInfo(activity).DistanceMeters;
                if (distance == 0 && activity.DistanceMetersTrack != null)
                {
                    distance = activity.DistanceMetersTrack.Max - activity.DistanceMetersTrack.Min;
                }

                return (float)distance;
            }
        }

        public float AvgGrade
        {
            get { return ActivityInfoCache.Instance.GetInfo(activity).SmoothedGradeTrack.Avg; }
        }

        /// <summary>
        /// The average heart rate of the duration of the record
        /// </summary>
        public int AvgHR
        {
            get
            {
                return (int)ActivityInfoCache.Instance.GetInfo(activity).AverageHeartRate;
            }
        }

        /// <summary>
        /// The fastest pace of the record (in user defined units (min/km or min/mile as appropriate)
        /// </summary>
        public TimeSpan AvgPace
        {
            get
            {
                if (MetersPerSecond != 0 && !float.IsNaN(MetersPerSecond))
                {
                    int seconds = (int)(1 / Length.Convert(MetersPerSecond, Length.Units.Meter, PluginMain.DistanceUnits));
                    return new TimeSpan(0, 0, 0, seconds);
                }
                else
                {
                    return new TimeSpan(0);
                }
            }
        }

        public float AvgCadence
        {
            get
            {
                ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);
                if (info.SmoothedCadenceTrack != null)
                {
                    return info.AverageCadence;
                }
                else
                {
                    return 0;
                }
            }
        }

        public float AvgPower
        {
            get
            {
                ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);
                return info.AveragePower;
            }
        }

        public string CategoryName
        {
            get { return category.Name; }
        }

        public RecordCategory Category
        {
            get { return category; }
            set { category = value; }
        }

        /// <summary>
        /// The amount of elevation change for the record (TotalAscend - TotalDescend)
        /// </summary>
        public float ElevationChange
        {
            get
            {
                float a = TotalAscend;
                float d = TotalDescend;
                return a + d;
            }
        }

        /// <summary>
        /// Lowest valley to highest peak in the activity 
        /// Displayed in user units
        /// </summary>
        public float ElevationDifference
        {
            get
            {
                return (float)Length.Convert(this.maxElevation - this.minElevation, Length.Units.Meter, PluginMain.GetApplication().SystemPreferences.ElevationUnits);
            }
        }

        /// <summary>
        /// The date and time that the record ends
        /// </summary>
        public DateTime EndTime
        {
            get
            {
                ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);
                DateTime endDateTime;

                if (info.ActualTrackEnd != DateTime.MinValue)
                    endDateTime = info.ActualTrackEnd;
                else
                    endDateTime = activity.StartTime + info.Time;

                return endDateTime.Add(activity.TimeZoneUtcOffset);
            }
        }

        /// <summary>
        /// The distance from the starting point of the activity to the ending point of the record 
        /// </summary>
        public float EndDistanceMeters
        {
            get
            {
                return TotalDistanceMeters + startDistance;
            }
        }

        /// <summary>
        /// The location of the record
        /// </summary>
        public string Location
        {
            get { return activity.Location; }
        }

        /// <summary>
        /// Maximum Grade
        /// </summary>
        public float MaxGrade
        {
            get
            {
                return ActivityInfoCache.Instance.GetInfo(activity).SmoothedGradeTrack.Max;
            }
        }

        /// <summary>
        /// The maximum heart rate for the record
        /// </summary>
        public int MaxHR
        {
            get
            {
                return (int)ActivityInfoCache.Instance.GetInfo(activity).MaximumHeartRate;
            }
        }

        public float MaxCadence
        {
            get
            {
                ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);
                if (info.SmoothedCadenceTrack != null)
                {
                    return info.MaximumCadence;
                }
                else
                {
                    return 0;
                }
            }
        }

        public float MaxPower
        {
            get
            {
                ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);
                return info.MaximumPower;
            }
        }

        /// <summary>
        /// Maximum Instantaneous Pace (in user defined units (min/km or min/mile as appropriate)
        /// </summary>
        public TimeSpan MaxPace
        {
            get
            {
                if (MaxSpeed != 0 && !float.IsNaN(MaxSpeed))
                {
                    int seconds = (int)(1 / Length.Convert(MaxSpeed, Length.Units.Meter, PluginMain.DistanceUnits));
                    return new TimeSpan(0, 0, 0, seconds);
                }
                else
                {
                    return new TimeSpan(0);
                }

            }
        }

        /// <summary>
        /// Maximum Instantaneous Speed in meters / second
        /// </summary>
        public float MaxSpeed
        {
            get
            {
                ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);
                double speed = info.FastestSpeedMetersPerSecond; // meter/sec

                return (float)speed;
            }
        }
        public float MaxElevation
        {
            get { return (float)Length.Convert(maxElevation, Length.Units.Meter, PluginMain.GetApplication().SystemPreferences.ElevationUnits); }
        }

        public float TRIMP
        {
            get
            {
                if (float.IsNaN(this.trimp))
                {
                    object value = activity.GetCustomDataValue(CustomDataFields.GetCustomProperty(CustomDataFields.TLCustomFields.Trimp, false));
                    if (value != null)
                        this.trimp = (float)((double)value);
                }

                return this.trimp;
            }
        }

        public float TSS
        {
            get
            {
                if (float.IsNaN(this.tss))
                {
                    object value = activity.GetCustomDataValue(CustomDataFields.GetCustomProperty(CustomDataFields.TLCustomFields.TSS, false));
                    if (value != null)
                        this.tss = (float)((double)value);
                }

                return this.tss;
            }
        }

        /// <summary>
        /// Minimum Grade
        /// </summary>
        public float MinGrade
        {
            get
            {
                return ActivityInfoCache.Instance.GetInfo(activity).SmoothedGradeTrack.Min;
            }
        }

        public float MinElevation
        {
            get { return (float)Length.Convert(minElevation, Length.Units.Meter, PluginMain.GetApplication().SystemPreferences.ElevationUnits); }
        }

        public string ActivityId
        {
            get
            {
                if (partialActivityId == null)
                    return activity.ReferenceId;
                else
                    return partialActivityId.ToString();
            }
        }

        public string ReferenceId
        {
            get { return id.ToString(); }
        }

        /// <summary>
        /// The average speed of the record
        /// </summary>
        public float MetersPerSecond
        {
            get
            {
                ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);
                double speed = info.AverageSpeedMetersPerSecond; // meter/sec

                if (double.IsNaN(speed) && activity.DistanceMetersTrack != null)
                {
                    speed = (activity.DistanceMetersTrack.Max - activity.DistanceMetersTrack.Min) / activity.DistanceMetersTrack.TotalElapsedSeconds;
                }

                return (float)speed;
            }
        }

        /// <summary>
        /// The date and time that the record starts in the local time (local to the activity).
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                DateTime startDateTime;

                if (activity.StartTime != DateTime.MinValue)
                {
                    startDateTime = activity.StartTime;
                }
                else if (ActivityInfoCache.Instance.GetInfo(activity).ActualTrackStart != DateTime.MinValue)
                {
                    startDateTime = ActivityInfoCache.Instance.GetInfo(activity).ActualTrackStart;
                }
                else
                {
                    return activity.StartTime;
                }

                return startDateTime.Add(activity.TimeZoneUtcOffset);
            }
        }

        /// <summary>
        /// The distance from the starting point of the activity to the starting point of the record
        /// </summary>
        public float StartDistanceMeters
        {
            get
            {
                return startDistance;
            }
        }

        /// <summary>
        /// The total distance ascended for the record
        /// </summary>
        public float TotalAscend
        {
            get
            {
                ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);
                float totalAscend = (float)info.TotalAscendingMeters(PluginMain.GetApplication().Logbook.ClimbZones[0]);

                if (totalAscend == 0 || float.IsNaN(totalAscend))
                {
                    totalAscend = info.Activity.TotalAscendMetersEntered;
                }

                totalAscend = (float)Length.Convert(totalAscend, Length.Units.Meter, PluginMain.GetApplication().SystemPreferences.ElevationUnits);

                return totalAscend;
            }
        }

        /// <summary>
        /// The total distance descended for the record
        /// </summary>
        public float TotalDescend
        {
            get
            {
                ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);
                float totalDescend = (float)info.TotalDescendingMeters(PluginMain.GetApplication().Logbook.ClimbZones[0]);

                if (totalDescend == 0 || float.IsNaN(totalDescend))
                {
                    totalDescend = info.Activity.TotalDescendMetersEntered;
                }

                totalDescend = (float)Length.Convert(totalDescend, Length.Units.Meter, PluginMain.GetApplication().SystemPreferences.ElevationUnits);

                return totalDescend;
            }
        }

        /// <summary>
        /// The temperature for the record in the correct format according to preferences
        /// </summary>
        public float Temp
        {
            get
            {
                return (float)Temperature.Convert(activity.Weather.TemperatureCelsius, Temperature.Units.Celsius, PluginMain.GetApplication().SystemPreferences.TemperatureUnits);
            }
        }

        /// <summary>
        /// Activity Total Time
        /// </summary>
        public TimeSpan TotalTime
        {
            get
            {
                //TimeSpan span = EndTime - StartTime;
                //if (TimeSpan.Zero < span)
                //    return span;

                ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);

                if (info.Time != TimeSpan.Zero)
                    return info.Time;
                else if (activity.TotalTimeEntered != TimeSpan.Zero)
                    return activity.TotalTimeEntered;
                else if (activity.DistanceMetersTrack != null && activity.DistanceMetersTrack.TotalElapsedSeconds != 0)
                    return new TimeSpan(0, 0, (int)activity.DistanceMetersTrack.TotalElapsedSeconds);
                else
                    return TimeSpan.Zero;
            }
        }

        public Speed.Units SpeedUnits
        {
            get
            {
                return activity.Category.SpeedUnits;
            }
        }

        public float TotalCalories
        {
            get
            {
                return activity.TotalCalories;
            }
        }

        public DateTime TrueStartDate
        {
            get
            {
                if (trueStartDate == DateTime.MinValue)
                {
                    if (activity.StartTime != DateTime.MinValue)
                    {
                        return activity.StartTime.Add(activity.TimeZoneUtcOffset);
                    }

                    // Activity start = min value.  This is bad.
                    return DateTime.MinValue;
                }

                return trueStartDate.Add(activity.TimeZoneUtcOffset);
            }
        }

        #endregion

        #region Methods

        public override bool Equals(object obj)
        {
            Record other = obj as Record;
            return other.ReferenceId == this.ReferenceId;
        }

        /// <summary>
        /// Gets the custom formatted text with proper decimal places, etc.
        /// Values are ready for user display (converted to proper units etc.)
        /// </summary>
        /// <param name="column">Column defines the property value to get</param>
        /// <returns>Returns custom formatted string ready for display to user.</returns>
        public string GetFormattedText(string Id, bool showUnits)
        {
            return GetFormattedText(this, Id, showUnits);
        }

        /// <summary>
        /// Gets the custom formatted text with proper decimal places, etc.  
        /// Values are ready for user display (converted to proper units etc.)
        /// </summary>
        /// <param name="activity">Activity containing value</param>
        /// <param name="column">Column defines the property value to get</param>
        /// <returns>Returns custom formatted string ready for display to user.</returns>
        public static string GetFormattedText(Record record, string Id, bool showUnits)
        {
            Type recordType = typeof(Record);  // Used to collect property value from activity
            string text = null;                         // text to display in cell (if defined)

            if (Id == "StartDistanceMeters" ||
                Id == "EndDistanceMeters" ||
                Id == "TotalDistanceMeters" ||
                Id == "TotalDistanceOneDay")
            {
                // 1.54
                float value = (float)recordType.GetProperty(Id).GetValue(record, null);
                value = (float)Length.Convert(value, Length.Units.Meter, PluginMain.DistanceUnits);
                text = value.ToString("0.##", CultureInfo.CurrentCulture);
            }
            else if (Id == "MetersPerSecond" ||
                Id == "MaxSpeed")
            {
                // 1.5
                //text = Speed.ToSpeedString(record.TotalDistanceMeters, record.TotalTime, new Length(1, PluginMain.DistanceUnits), "#.##");
                float value = (float)recordType.GetProperty(Id).GetValue(record, null);
                value = (float)Length.Convert(value, Length.Units.Meter, PluginMain.DistanceUnits);
                value = value * 60 * 60; // Convert sec. to hours (mph or km/hr)

                text = value.ToString("0.#", CultureInfo.CurrentCulture);
            }
            else if (Id == "TotalTime" ||
                Id == "AvgPace" ||
                Id == "MaxPace" ||
                Id == "TotalTimeOneDay")
            {
                // 1:23:09
                TimeSpan value = (TimeSpan)recordType.GetProperty(Id).GetValue(record, null);
                text = Utilities.ToTimeString(value);
            }
            else if (Id == "MaxCadence" ||
                Id == "AvgCadence" ||
                Id == "MaxPower" ||
                Id == "AvgPower" ||
                Id == "TotalCalories" ||
                Id == "TRIMP" ||
                Id == "TSS")
            {
                // 154 (Ignore '0' values)
                float value = (float)recordType.GetProperty(Id).GetValue(record, null);
                text = value.ToString("#");
            }
            else if (Id == "Temp" ||
                Id == "TotalAscend" ||
                Id == "TotalDescend" ||
                Id == "ElevationChange" ||
                Id == "ElevationDifference" ||
                Id == "MaxElevation" ||
                Id == "MinElevation")
            {
                // 154 (Include '0' values)
                float value = (float)recordType.GetProperty(Id).GetValue(record, null);
                text = value.ToString("0");
            }
            else if (Id == "AvgHR" ||
                Id == "MaxHR")
            {
                // 154
                int value = (int)recordType.GetProperty(Id).GetValue(record, null);
                text = value.ToString("#");
            }
            else if (Id == "AvgGrade" ||
                Id == "MaxGrade" ||
                Id == "MinGrade")
            {
                // 3.2%, -3.2%, 0.0%
                float value = (float)recordType.GetProperty(Id).GetValue(record, null);
                text = value.ToString("0.0%", CultureInfo.CurrentCulture);
            }
            else if (Id == "SpeedPace")
            {
                // 6.8 mph / 8:48 min/mi
                return string.Format("{0} {1} / {2} {3}", record.GetFormattedText("MetersPerSecond", false), Units.Speed, record.GetFormattedText("AvgPace", false), Units.Pace);
            }
            else if (Id == "AscentDescent")
            {
                // 701 / -691 ft
                return string.Format("{0} / {1} {2}", record.GetFormattedText("TotalAscend", false), record.GetFormattedText("TotalDescend", false), Units.Elevation);
            }
            else if (Id == "MaxMinElevation")
            {
                // 417 / 292 ft
                return string.Format("{0} / {1} {2}", record.GetFormattedText("MaxElevation", false), record.GetFormattedText("MinElevation", false), Units.Elevation);
            }
            else if (Id == "MaxMinGrade")
            {
                // 14.1 / -8.1 %
                return string.Format("{0} / {1}", record.GetFormattedText("MaxGrade", false), record.GetFormattedText("MinGrade", false), Units.Elevation);
            }
            else
            {
                // Default
                PropertyInfo info = recordType.GetProperty(Id);

                if (info != null)
                {
                    object value = info.GetValue(record, null);
                    if (value != null)
                        text = value.ToString();
                }
            }

            if (string.Equals("NaN", text))
                text = string.Empty;

            if (showUnits && !string.IsNullOrEmpty(text))
                text = string.Format("{0} {1}", text, GetFormattedUnits(Id));

            return text;
        }

        public static string GetFormattedUnits(string Id)
        {
            if (Id == "Temp")
                return Units.Temp;

            else if (Id.Contains("Distance"))
                return Units.Distance;

            else if (Id.Contains("Elevation") || Id.Contains("Ascend") || Id.Contains("Descend"))
                return Units.Elevation;

            else if (Id.Contains("Cadence"))
                return CommonResources.Text.LabelRPM;

            else if (Id.Contains("Pace"))
                return Units.Pace;

            else if (Id.Contains("Speed") || Id.Equals("MetersPerSecond"))
                return Units.Speed;

            else if (Id.Contains("HR"))
                return CommonResources.Text.LabelBPM;

            else if (Id.Contains("Power"))
                return CommonResources.Text.LabelWatts;

            else
                return string.Empty;
        }

        #endregion

        void activity_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // Since most are connected to activity, there may not be that much that needs to be updated...?
            IActivity activity = sender as IActivity;
            if (activity != null)
            {
                ChartData.QueueActivity(activity);
                ChartData.IsDirty = true;
            }
        }

        #region IComparable Members

        public int CompareTo(Record other)
        {
            return DateTime.Compare(other.StartTime, this.StartTime);
        }

        /// <summary>
        /// Column specific sort
        /// </summary>
        /// <param name="a2"></param>
        /// <param name="comparisonMethod"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public int CompareTo(Record other, RecordComparer comparer)
        {
            return CompareTo(other, comparer.ComparisonMethod, comparer.SortOrder);
        }

        /// <summary>
        /// Column specific sort
        /// </summary>
        /// <param name="a2"></param>
        /// <param name="comparisonMethod"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public int CompareTo(Record other, RecordComparer.ComparisonType comparisonMethod, RecordComparer.Order sortOrder)
        {
            Type recordType = typeof(Record);  // Used to collect property value from activity
            int result = 0;
            string property;

            if (comparisonMethod == RecordComparer.ComparisonType.TotalDistanceMeters)
                property = "TotalDistanceMeters";

            else if (comparisonMethod == RecordComparer.ComparisonType.AvgSpeed ||
                comparisonMethod == RecordComparer.ComparisonType.AvgPace)
                property = "MetersPerSecond";

            else
                property = comparisonMethod.ToString();

            object thisVal = recordType.GetProperty(property).GetValue(this, null);
            object otherVal = recordType.GetProperty(property).GetValue(other, null);

            if (thisVal.GetType() == typeof(int))
            {
                result = ((int)thisVal).CompareTo(otherVal);
            }
            else if (thisVal.GetType() == typeof(float))
            {
                result = ((float)thisVal).CompareTo(otherVal);
            }
            else if (thisVal.GetType() == typeof(double))
            {
                result = ((double)thisVal).CompareTo(otherVal);
            }
            else if (thisVal.GetType() == typeof(string))
            {
                result = ((string)thisVal).CompareTo(otherVal);
            }
            else if (thisVal.GetType() == typeof(DateTime))
            {
                result = ((DateTime)thisVal).CompareTo(otherVal);
            }
            else if (thisVal.GetType() == typeof(TimeSpan))
            {
                result = ((TimeSpan)thisVal).CompareTo(otherVal);
            }
            else
            { }

            if (sortOrder == RecordComparer.Order.Descending)
                return result * -1;

            else
                return result;
        }

        #endregion

        public class RecordComparer : IComparer
        {
            #region Fields

            private ComparisonType compareType;
            private Order sortOrder;

            #endregion

            #region Constructors

            public RecordComparer(RecordCategory.RecordType catType)
                : this()
            {
                switch (catType)
                {
                    case RecordCategory.RecordType.MaxTemperature:
                        compareType = Record.RecordComparer.ComparisonType.Temp;
                        break;

                    case RecordCategory.RecordType.MinTemperature:
                        compareType = Record.RecordComparer.ComparisonType.Temp;
                        sortOrder = Record.RecordComparer.Order.Ascending;
                        break;

                    case RecordCategory.RecordType.MaxDistance:
                        compareType = ComparisonType.TotalDistanceMeters;
                        break;

                    case RecordCategory.RecordType.MaxAscent:
                        compareType = ComparisonType.TotalAscend;
                        break;

                    case RecordCategory.RecordType.MaxDescent:
                        compareType = ComparisonType.TotalDescend;
                        sortOrder = Order.Ascending;
                        break;

                    case RecordCategory.RecordType.SpeedPace:
                        compareType = ComparisonType.AvgSpeed;
                        break;

                    case RecordCategory.RecordType.AvgPace:
                        compareType = ComparisonType.AvgSpeed;
                        break;

                    case RecordCategory.RecordType.MaxPace:
                        compareType = ComparisonType.MaxSpeed;
                        break;

                    // TODO: Are these comparisons right??? These are used to rank Now/Then Records
                    case RecordCategory.RecordType.AscentDescent:
                        compareType = ComparisonType.TotalAscend;
                        break;
                    case RecordCategory.RecordType.HottestColdest:
                        compareType = ComparisonType.Temp;
                        break;
                    case RecordCategory.RecordType.MaxMinElevation:
                        compareType = ComparisonType.MaxElevation;
                        break;
                    case RecordCategory.RecordType.MaxMinGrade:
                        compareType = ComparisonType.MaxGrade;
                        break;

                    case RecordCategory.RecordType.Distance:
                        compareType = ComparisonType.AvgSpeed;
                        break;

                    default:
                        string cat = catType.ToString();
                        compareType = (ComparisonType)Enum.Parse(typeof(ComparisonType), cat);
                        if (cat.StartsWith("Min")) sortOrder = Order.Ascending;
                        break;
                }
            }

            public RecordComparer()
            {
                sortOrder = Order.Descending;
                compareType = ComparisonType.StartTime;
            }

            #endregion

            #region Enumerations

            public enum ComparisonType
            {
                StartTime = 1,
                ElevationChange = 2, // max - min elevation
                NormPower = 3,
                AvgPower = 4,
                Location = 5,
                Category = 6,
                TotalDistanceMeters = 7,
                TotalTime = 8,
                TotalAscend = 9,
                TotalDescend = 10,
                Temp = 11,
                Rank = 12,
                ATL = 13,
                CTL = 14,
                AvgPace = 15,
                AvgSpeed = 16,
                AvgHR = 17,
                TotalCalories = 18,
                AvgCadence = 19,
                AvgGrade = 20,
                Day = 21,
                MaxPace = 22,
                MaxSpeed = 23,
                MaxHR = 24,
                MaxPower = 25,
                Name = 26,
                StartDistanceMeters = 27,
                EndDistanceMeters = 28,
                EndTime = 29,
                ActivityCategory = 30,
                MaxElevation = 32,
                MinElevation = 33,
                MaxCadence = 34,
                MinCadence = 35,
                MaxGrade = 36,
                MinGrade = 37,
                TotalTimeOneDay = 38,
                TotalDistanceOneDay = 39,
                ElevationDifference = 40,
                TRIMP = 41,
                TSS = 42
            }

            public enum Order
            {
                Ascending = 1,
                Descending = 2
            }

            #endregion

            #region Properties

            public ComparisonType ComparisonMethod
            {
                get { return compareType; }
                set { compareType = value; }
            }

            public Order SortOrder
            {
                get { return sortOrder; }
                set { sortOrder = value; }
            }

            #endregion

            #region IComparer<Record> Members

            public int Compare(Record x, Record y)
            {
                return x.CompareTo(y, compareType, sortOrder);
            }

            #endregion

            #region IComparer Members

            public int Compare(object x, object y)
            {
                Record rec1 = x as Record;
                Record rec2 = y as Record;
                return Compare(rec1, rec2);
            }

            #endregion
        }
    }
}
