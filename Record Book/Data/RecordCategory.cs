namespace RecordBook.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Xml.Serialization;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Data.Measurement;
    using ZoneFiveSoftware.Common.Visuals;
    using System.Resources;
    using System.Reflection;
    using RecordBook.Resources;
    using System.Drawing;

    /// <summary>
    /// RecordCategory is used to store the user defined set of categories for records
    /// </summary>
    [XmlRootAttribute(ElementName = "RecordCategory", IsNullable = false)]
    public class RecordCategory : IComparable
    {
        #region Fields

        private RecordType type;
        private string name;
        private float distance;
        private Length.Units lengthMeasure;
        private List<string> categories;
        private Guid id;

        #endregion

        #region Enumerations

        /// <summary>
        /// Enumeration to broadly define what type of record this is.
        /// </summary>
        public enum RecordType
        {
            Distance = 0,
            DistancePace = 1,
            AllActivities = 2,
            MaxTemperature = 3,
            MinTemperature = 4,
            MaxDistance = 5,
            TotalTime = 6,
            MaxSpeed = 7,
            MaxHR = 8,
            AvgSpeed = 9,
            AvgPace = 10,
            AvgHR = 11,
            MaxElevation = 12,
            MinElevation = 13,
            MaxCadence = 14,
            ElevationDifference = 15,
            MaxPower = 16,
            AvgPower = 17,
            MaxAscent = 18,
            MaxDescent = 19,
            TotalCalories = 20,
            AvgCadence = 21,
            TotalTimeOneDay = 22,
            TotalDistanceOneDay = 23,
            MaxGrade = 24,
            MinGrade = 25,
            AscentDescent = 26,
            HottestColdest = 27,
            MaxMinElevation = 28,
            MaxMinGrade = 29,
            SpeedPace = 30,
            MaxPace = 31,
            TRIMP = 32,
            TSS = 33
        }

        public enum RecordGroup
        {
            Distance,
            Extreme,
            NowThen
        }

        public static string DisplayValue(RecordType type)
        {
            switch (type)
            {
                case RecordType.AllActivities:
                    return Resources.Strings.Label_AllActivities;
                case RecordType.AscentDescent:
                    return CommonResources.Text.LabelAscending + " / " + CommonResources.Text.LabelDescending;
                case RecordType.AvgCadence:
                    return CommonResources.Text.LabelMaxAvgCadence;
                case RecordType.AvgHR:
                    return CommonResources.Text.LabelMaxAvgHR;
                case RecordType.AvgPower:
                    return CommonResources.Text.LabelMaxAvgPower;
                case RecordType.Distance:
                    return CommonResources.Text.LabelDistance;
                case RecordType.DistancePace:
                    return CommonResources.Text.LabelDistance + " / " + CommonResources.Text.LabelPace;
                case RecordType.ElevationDifference:
                    return CommonResources.Text.LabelElevationChange;
                case RecordType.AvgPace:
                    return CommonResources.Text.LabelFastestPace;
                case RecordType.AvgSpeed:
                    return CommonResources.Text.LabelFastestSpeed;
                case RecordType.HottestColdest:
                    return Resources.Strings.Label_Max + " / " + Resources.Strings.Label_Min + " " + CommonResources.Text.LabelTemperature;
                case RecordType.MaxAscent:
                    return CommonResources.Text.LabelAscending;
                case RecordType.MaxCadence:
                    return CommonResources.Text.LabelMaxCadence;
                case RecordType.MaxDescent:
                    return CommonResources.Text.LabelDescending;
                case RecordType.MaxDistance:
                    return Resources.Strings.Label_MaxDistance;
                case RecordType.MaxElevation:
                    return Resources.Strings.Label_MaxElevation;
                case RecordType.MaxGrade:
                    return CommonResources.Text.LabelMaxGrade;
                case RecordType.MaxHR:
                    return CommonResources.Text.LabelMaxHR;
                case RecordType.MaxMinElevation:
                    return Resources.Strings.Label_Max + " / " + Resources.Strings.Label_Min + " " + CommonResources.Text.LabelElevation;
                case RecordType.MaxMinGrade:
                    return Resources.Strings.Label_Max + " / " + Resources.Strings.Label_Min + " " + CommonResources.Text.LabelGrade;
                case RecordType.MaxPower:
                    return CommonResources.Text.LabelMaxPower;
                case RecordType.MaxSpeed:
                    return Resources.Strings.Label_MaxAvgSpeed;
                case RecordType.MaxPace:
                    return Resources.Strings.Label_MaxPace;
                case RecordType.MaxTemperature:
                    return Resources.Strings.Label_MaxTemperature;
                case RecordType.MinElevation:
                    return Resources.Strings.Label_MinElevation;
                case RecordType.MinGrade:
                    return Resources.Strings.Label_MinGrade;
                case RecordType.MinTemperature:
                    return Resources.Strings.Label_MinTemperature;
                case RecordType.SpeedPace:
                    return CommonResources.Text.LabelPaceOrSpeed;
                case RecordType.TotalCalories:
                    return CommonResources.Text.LabelCalories;
                case RecordType.TotalDistanceOneDay:
                    return Resources.Strings.Label_TotalDistanceForOneDay;
                case RecordType.TotalTimeOneDay:
                    return Resources.Strings.Label_TotalTimeForOneDay;
                case RecordType.TotalTime:
                    return CommonResources.Text.LabelTotalTime;
                case RecordType.TRIMP:
                    return Resources.Strings.Label_MaxTRIMP;
                case RecordType.TSS:
                    return Resources.Strings.Label_MaxTSS;
                default:
                    return type.ToString();
            }
        }

        /// <summary>
        /// Gets the appropriate record type combo definitions for the various record groups.  
        /// These are used for display on the settings page comboboxes.
        /// </summary>
        /// <param name="group"></param>
        /// <returns>The list of items to be displayed on the settings page.</returns>
        internal static IDictionary<object, string> GetSettingsComboItems(RecordGroup group)
        {
            IDictionary<object, string> data = new Dictionary<object, string>();

            // Distance is a special case because combobox is units, not record types
            if (group == RecordGroup.Distance)
            {
                foreach (Length.Units unit in Enum.GetValues(typeof(Length.Units)))
                {
                    data.Add(unit, Length.LabelPlural(unit));
                }
                return data;
            }

            // Handle Extreme and NowThen comboboxes
            foreach (RecordCategory.RecordType unit in Enum.GetValues(typeof(RecordCategory.RecordType)))
            {
                switch (group)
                {
                    case RecordGroup.Extreme:
                        // Filter available Extreme categories
                        if (unit == RecordCategory.RecordType.MaxTemperature ||
                            unit == RecordCategory.RecordType.MinTemperature ||
                            unit == RecordCategory.RecordType.MaxDistance ||
                            unit == RecordCategory.RecordType.TotalTime ||
                            unit == RecordCategory.RecordType.MaxSpeed ||
                            unit == RecordCategory.RecordType.MaxPace ||
                            unit == RecordCategory.RecordType.MaxHR ||
                            unit == RecordCategory.RecordType.AvgSpeed ||
                            unit == RecordCategory.RecordType.AvgPace ||
                            unit == RecordCategory.RecordType.AvgHR ||
                            unit == RecordCategory.RecordType.MaxElevation ||
                            unit == RecordCategory.RecordType.MinElevation ||
                            unit == RecordCategory.RecordType.MaxCadence ||
                            unit == RecordCategory.RecordType.ElevationDifference ||
                            unit == RecordCategory.RecordType.MaxPower ||
                            unit == RecordCategory.RecordType.AvgPower ||
                            unit == RecordCategory.RecordType.MaxAscent ||
                            unit == RecordCategory.RecordType.MaxDescent ||
                            unit == RecordCategory.RecordType.TotalCalories ||
                            unit == RecordCategory.RecordType.AvgCadence ||
                            unit == RecordCategory.RecordType.TotalTimeOneDay ||
                            unit == RecordCategory.RecordType.TotalDistanceOneDay ||
                            unit == RecordCategory.RecordType.MaxGrade ||
                            unit == RecordCategory.RecordType.MinGrade ||
                            unit == RecordCategory.RecordType.TRIMP ||
                            unit == RecordCategory.RecordType.TSS)
                        {
                            data.Add(unit, RecordCategory.DisplayValue(unit));
                        }
                        break;

                    case RecordGroup.NowThen:
                        // Filter available NowThen categories
                        if (unit == RecordCategory.RecordType.Distance ||
                            unit == RecordCategory.RecordType.TotalTime ||
                            unit == RecordCategory.RecordType.SpeedPace ||
                            unit == RecordCategory.RecordType.AvgHR ||
                            unit == RecordCategory.RecordType.MaxHR ||
                            unit == RecordCategory.RecordType.ElevationDifference ||
                            unit == RecordCategory.RecordType.AscentDescent ||
                            unit == RecordCategory.RecordType.HottestColdest ||
                            unit == RecordCategory.RecordType.MaxSpeed ||
                            unit == RecordCategory.RecordType.MaxPace ||
                            unit == RecordCategory.RecordType.MaxMinElevation ||
                            unit == RecordCategory.RecordType.AvgCadence ||
                            unit == RecordCategory.RecordType.MaxCadence ||
                            unit == RecordCategory.RecordType.AvgPower ||
                            unit == RecordCategory.RecordType.TotalCalories ||
                            unit == RecordCategory.RecordType.MaxMinGrade ||
                            unit == RecordCategory.RecordType.TRIMP ||
                            unit == RecordCategory.RecordType.TSS)
                        {
                            data.Add(unit, RecordCategory.DisplayValue(unit));
                        }
                        break;
                }
            }

            return data;
        }

        #endregion

        #region Constructors
        public RecordCategory()
        {
            this.id = Guid.NewGuid();
        }

        public RecordCategory(string name, RecordType type, List<string> refIds)
            : this()
        {
            this.name = name;
            this.type = type;
            this.categories = refIds;
        }

        public RecordCategory(string name, RecordType type, List<string> refIds, float distance, Length.Units units)
            : this(name, type, refIds)
        {
            this.Distance = distance;
            this.LengthMeasure = units;
        }


        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the unique reference id for this record category
        /// </summary>
        public string RefId
        {
            get
            {
                if (id == null || id == Guid.Empty)
                {
                    id = Guid.NewGuid();
                }

                return id.ToString("D");
            }

            set
            {
                try
                {
                    // Try to parse, if not at least set it to a new valid guid
                    id = new Guid(value);
                }
                catch
                {
                    id = Guid.NewGuid();
                }
            }
        }

        /// <summary>
        /// Gets or sets the type of record stored as an enumeration.
        /// For example, Distance, MaxTemperature, TotalTime, etc.
        /// </summary>
        public RecordType Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// Gets or sets the name of the record category
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating the distance for the record category
        /// </summary>
        public float Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        /// <summary>
        /// Gets or sets the unit used to measure the length of this record
        /// </summary>
        public Length.Units LengthMeasure
        {
            get { return lengthMeasure; }
            set { lengthMeasure = value; }
        }
        public string DisplayType
        {
            get { return DisplayValue(type); }
        }

        /// <summary>
        /// Gets or sets the categories.
        /// Categories will store an ArrayList of ST categories for which this record type applies
        /// </summary>
        public List<string> Categories
        {
            get
            {
                if (categories == null)
                {
                    categories = new List<string>();
                }

                return categories;
            }
            set
            {
                categories = value;
            }
        }

        /// <summary>
        /// Gets a string of the unit to be displayed
        /// </summary>
        public string Display_Unit
        {
            get { return Length.LabelPlural(lengthMeasure); }
        }

        public double Centimeters
        {
            get { return Length.Convert(distance, lengthMeasure, Length.Units.Centimeter); }
        }

        public double Meters
        {
            get { return Length.Convert(distance, lengthMeasure, Length.Units.Meter); }
        }

        public List<string> Display_Categories
        {
            get
            {
                List<string> display = new List<string>();
                foreach (IActivityCategory cat in PluginMain.GetApplication().Logbook.ActivityCategories)
                {
                    GetActivityCategories(cat, ref display);
                }

                return display;
            }
        }

        public string Display_RecordType
        {
            get
            {
                return DisplayValue(type);
            }
        }

        #endregion

        #region Methods

        internal string GetValuePropertyName()
        {
            // Used to translate category to the associated value property for display records
            return GetValuePropertyName(this.Type);
        }

        internal static string GetValuePropertyName(RecordCategory.RecordType categoryType)
        {
            // Used to translate category to the associated value property for display records
            if (categoryType == RecordType.Distance ||
                categoryType == RecordType.MaxDistance)
                return "TotalDistanceMeters";

            else if (categoryType == RecordType.MaxTemperature ||
                categoryType == RecordType.MinTemperature ||
                categoryType == RecordType.HottestColdest)
                return "Temp";

            else if (categoryType == RecordType.AvgSpeed ||
                categoryType == RecordType.AvgPace)
                return "MetersPerSecond";

            else if (categoryType == RecordType.MaxAscent)
                return "TotalAscend";

            else if (categoryType == RecordType.MaxDescent)
                return "TotalDescend";

            else
                return categoryType.ToString();

        }

        /// <summary>
        /// Get a list of the categories for display
        /// </summary>
        /// <param name="cat"></param>
        /// <param name="display"></param>
        public void GetActivityCategories(IActivityCategory cat, ref List<string> display)
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
                    if (cat.Parent != null)
                    {
                        display.Add(cat.Parent.Name + ": " + cat.Name);
                    }
                    else
                    {
                        display.Add(cat.Name);
                    }
                }
            }
        }

        public Image GetImage()
        {
            switch (this.Type)
            {
                case RecordType.AllActivities:
                    return null;
                case RecordType.AvgCadence:
                    return Images.cadence;
                case RecordType.MaxCadence:
                    return Images.cadence;

                case RecordType.AvgHR:
                    return Images.heart;
                case RecordType.MaxHR:
                    return Images.heart;
                case RecordType.TRIMP:
                    return Images.heart;

                case RecordType.AvgPace:
                    return Images.shoes;
                case RecordType.MaxPace:
                    return Images.shoes;
                case RecordType.SpeedPace:
                    return Images.shoes;

                case RecordType.AvgPower:
                    return Images.lightning;
                case RecordType.MaxPower:
                    return Images.lightning;
                case RecordType.TSS:
                    return Images.lightning;

                case RecordType.AvgSpeed:
                    return Images.speed;
                case RecordType.MaxSpeed:
                    return Images.speed;

                case RecordType.MaxGrade:
                    return Images.grade;
                case RecordType.MinGrade:
                    return Images.grade;
                case RecordType.MaxMinGrade:
                    return Images.grade;

                case RecordType.TotalCalories:
                    return Images.peppermint;

                case RecordType.TotalTime:
                    return Images.hourglass;
                case RecordType.TotalTimeOneDay:
                    return Images.hourglass;

                case RecordType.Distance:
                    return Images.distance;
                case RecordType.DistancePace:
                    return Images.distance;
                case RecordType.TotalDistanceOneDay:
                    return Images.distance;
                case RecordType.MaxDistance:
                    return Images.distance;

                case RecordType.MaxTemperature:
                    return Images.fire;
                case RecordType.HottestColdest:
                    return Images.fire;
                case RecordType.MinTemperature:
                    return Images.ice;
                case RecordType.AscentDescent:
                    return Images.mountain;
                case RecordType.ElevationDifference:
                    return Images.mountain;
                case RecordType.MaxElevation:
                    return Images.mountain;
                case RecordType.MinElevation:
                    return Images.mountain;
                case RecordType.MaxMinElevation:
                    return Images.mountain;
                case RecordType.MaxAscent:
                    return Images.mountain;
                case RecordType.MaxDescent:
                    return Images.mountain;

                default:
                    break;
            }

            return null;
        }

        #endregion

        #region IComparable Members
        public int CompareTo(object obj)
        {
            RecordCategory rc = (RecordCategory)obj;
            if (rc.Centimeters > this.Centimeters)
            {
                return -1;
            }
            else if (rc.Centimeters == this.Centimeters)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        #endregion

    }
}
