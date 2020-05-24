namespace RecordBook.Settings
{
    using System.Text;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Xml;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Data.Measurement;
    using ZoneFiveSoftware.Common.Visuals;
    using RecordBook.Data;
    using System.ComponentModel;

    /// <summary>
    /// Record Book User settings.
    /// </summary>
    [Serializable()]    // Set this attribute to all the classes that want to serialize
    class UserData : ISerializable
    {
        #region Fields

        internal static event PropertyChangedEventHandler PropertyChanged;

        internal static bool newInstall = true;

        private static bool modified; // Modification status
        private static bool loaded; // Indicates if data has been loaded from the logbook

        private static string userColumns_AllActivites; // User Activity list column settings
        private static string userColumns_Distance; // User Activity list column settings
        private static string userColumns_Extreme; // User Activity list column settings
        private static string extreme_Record_Categories;
        private static string nowThen_Record_Categories;
        private static string distance_Record_Categories;
        private static string distance_chart_items;
        private static string version;

        private static DateTime endDateFilter;
        private static DateTime startDateFilter;

        private static IList<string> distanceChartItems;

        private static List<RecordCategory> distanceRecordCats;
        private static List<RecordCategory> extremeRecordCats;
        private static List<RecordCategory> nowThenRecordCats;

        private static int numRecords;

        #endregion

        #region Constructors

        public UserData()
        {
            if (loaded == false)
            {
                LoadSettings();
            }
        }

        #endregion

        #region Enumerations

        internal enum CatType
        {
            Distance,
            NowThen,
            Extremes
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether any settings have been modified.
        /// </summary>
        public static bool Modified
        {
            get
            {
                return modified;
            }

            set
            {
                if (value == false)
                {
                    modified = false;
                }
                else
                {
                    StoreSettings();
                    ChartData.IsDirty = true;
                    modified = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets list columns stored in user profile
        /// </summary>
        public static string UserColumns_AllActivites
        {
            get
            {
                EnsureSettingsLoaded();
                if (string.IsNullOrEmpty(userColumns_AllActivites))
                {
                    return "StartTime|181;Location|205;Name|195;TotalDistanceMeters|88;TotalTime|64;ActivityCategory|184";
                }
                else
                {
                    return userColumns_AllActivites.Trim(';');
                }
            }

            set
            {
                userColumns_AllActivites = value;
                Modified = true;
            }
        }

        /// <summary>
        /// Gets or sets list columns stored in user profile
        /// </summary>
        public static string UserColumns_Distance
        {
            get
            {
                EnsureSettingsLoaded();
                if (string.IsNullOrEmpty(userColumns_Distance))
                {
                    return "Rank|205;StartTime|156;Name|168;AvgPace|72;Location|178;ActivityCategory|154;TotalDistanceMeters|64;TotalTime|58;";
                }
                else
                {
                    return userColumns_Distance.Trim(';');
                }
            }

            set
            {
                userColumns_Distance = value;
                Modified = true;
            }
        }

        /// <summary>
        /// Gets or sets list columns stored in user profile
        /// </summary>
        public static string UserColumns_Extreme
        {
            get
            {
                EnsureSettingsLoaded();
                if (string.IsNullOrEmpty(userColumns_Extreme))
                {
                    return "Rank|197;StartTime|144;RecValue|80;Name|186;TotalTime|55;AvgHR|66;Location|192;MetersPerSecond|67;AvgPace|83";
                }
                else
                {
                    return userColumns_Extreme.Trim(';');
                }
            }

            set
            {
                userColumns_Extreme = value;
                Modified = true;
            }
        }

        /// <summary>
        /// Gets or sets distance record categories stored in user profile
        /// </summary>
        public static List<RecordCategory> DistanceRecordCategories
        {
            get
            {
                EnsureSettingsLoaded();

                if (distanceRecordCats == null || distanceRecordCats.Count == 0)
                {
                    distanceRecordCats = new List<RecordCategory>();
                    distanceRecordCats.Add(new RecordCategory("1 " + Length.LabelAbbr(Length.Units.Mile), RecordCategory.RecordType.Distance, GetAllRefIDs(), 1, Length.Units.Mile));
                    distanceRecordCats.Add(new RecordCategory("5 " + Length.LabelAbbr(Length.Units.Kilometer), RecordCategory.RecordType.Distance, GetAllRefIDs(), 5, Length.Units.Kilometer));
                    distanceRecordCats.Add(new RecordCategory("10 " + Length.LabelAbbr(Length.Units.Kilometer), RecordCategory.RecordType.Distance, GetAllRefIDs(), 10, Length.Units.Kilometer));
                }

                return distanceRecordCats;
            }

            set
            {
                distanceRecordCats = value;
                ChartData.IsSuperDirty = true;
                Modified = true;
            }
        }

        /// <summary>
        /// Gets or sets extreme record categories stored in user profile
        /// </summary>
        public static List<RecordCategory> ExtremeRecordCategories
        {
            get
            {
                EnsureSettingsLoaded();

                if (extremeRecordCats == null || extremeRecordCats.Count == 0)
                {
                    // Setup Default settings
                    extremeRecordCats = new List<RecordCategory>();
                    extremeRecordCats.Add(new RecordCategory(Resources.Strings.Label_MaxTemperature, RecordCategory.RecordType.MaxTemperature, GetAllRefIDs()));
                    extremeRecordCats.Add(new RecordCategory(Resources.Strings.Label_MinTemperature, RecordCategory.RecordType.MinTemperature, GetAllRefIDs()));
                    extremeRecordCats.Add(new RecordCategory(Resources.Strings.Label_MaxDistance, RecordCategory.RecordType.MaxDistance, GetAllRefIDs()));
                    extremeRecordCats.Add(new RecordCategory(Resources.Strings.Label_TotalDistanceForOneDay, RecordCategory.RecordType.TotalDistanceOneDay, GetAllRefIDs()));
                    extremeRecordCats.Add(new RecordCategory(Resources.Strings.Label_MaxTime, RecordCategory.RecordType.TotalTime, GetAllRefIDs()));
                    extremeRecordCats.Add(new RecordCategory(Resources.Strings.Label_TotalTimeForOneDay, RecordCategory.RecordType.TotalTimeOneDay, GetAllRefIDs()));
                    extremeRecordCats.Add(new RecordCategory(CommonResources.Text.LabelAvgSpeed, RecordCategory.RecordType.AvgSpeed, GetAllRefIDs()));
                    extremeRecordCats.Add(new RecordCategory(CommonResources.Text.LabelFastestSpeed, RecordCategory.RecordType.MaxSpeed, GetAllRefIDs()));
                    extremeRecordCats.Add(new RecordCategory(CommonResources.Text.LabelAvgPace, RecordCategory.RecordType.AvgPace, GetAllRefIDs()));
                    extremeRecordCats.Add(new RecordCategory(CommonResources.Text.LabelFastestPace, RecordCategory.RecordType.MaxPace, GetAllRefIDs()));
                    extremeRecordCats.Add(new RecordCategory(CommonResources.Text.LabelMaxAvgHR, RecordCategory.RecordType.AvgHR, GetAllRefIDs()));
                    extremeRecordCats.Add(new RecordCategory(CommonResources.Text.LabelMaxHR, RecordCategory.RecordType.MaxHR, GetAllRefIDs()));

                    if (CustomDataFields.TRIMPexists)
                        extremeRecordCats.Add(new RecordCategory(Resources.Strings.Label_MaxTRIMP, RecordCategory.RecordType.TRIMP, GetAllRefIDs()));

                    extremeRecordCats.Add(new RecordCategory(Resources.Strings.Label_MaxElevation, RecordCategory.RecordType.MaxElevation, GetAllRefIDs()));
                    extremeRecordCats.Add(new RecordCategory(Resources.Strings.Label_MinElevation, RecordCategory.RecordType.MinElevation, GetAllRefIDs()));
                    extremeRecordCats.Add(new RecordCategory(CommonResources.Text.LabelElevationChange, RecordCategory.RecordType.ElevationDifference, GetAllRefIDs()));
                    extremeRecordCats.Add(new RecordCategory(CommonResources.Text.LabelAscending, RecordCategory.RecordType.MaxAscent, GetAllRefIDs()));
                    extremeRecordCats.Add(new RecordCategory(CommonResources.Text.LabelDescending, RecordCategory.RecordType.MaxDescent, GetAllRefIDs()));
                    extremeRecordCats.Add(new RecordCategory(CommonResources.Text.LabelMaxAvgCadence, RecordCategory.RecordType.AvgCadence, GetAllRefIDs()));
                    extremeRecordCats.Add(new RecordCategory(CommonResources.Text.LabelMaxCadence, RecordCategory.RecordType.MaxCadence, GetAllRefIDs()));
                    extremeRecordCats.Add(new RecordCategory(CommonResources.Text.LabelMaxAvgPower, RecordCategory.RecordType.AvgPower, GetAllRefIDs()));

                    if (CustomDataFields.TSSexists)
                        extremeRecordCats.Add(new RecordCategory(Resources.Strings.Label_MaxTSS, RecordCategory.RecordType.TSS, GetAllRefIDs()));

                    extremeRecordCats.Add(new RecordCategory(CommonResources.Text.LabelMaxPower, RecordCategory.RecordType.MaxPower, GetAllRefIDs()));
                    extremeRecordCats.Add(new RecordCategory(CommonResources.Text.LabelCalories, RecordCategory.RecordType.TotalCalories, GetAllRefIDs()));
                    extremeRecordCats.Add(new RecordCategory(CommonResources.Text.LabelMaxGrade, RecordCategory.RecordType.MaxGrade, GetAllRefIDs()));
                    extremeRecordCats.Add(new RecordCategory(Resources.Strings.Label_MinGrade, RecordCategory.RecordType.MinGrade, GetAllRefIDs()));
                }

                return extremeRecordCats;
            }

            set
            {
                extremeRecordCats = value;
                ChartData.IsSuperDirty = true;
                Modified = true;
            }
        }

        /// <summary>
        /// Gets or sets extreme record categories stored in user profile
        /// </summary>
        public static List<RecordCategory> NowThenRecordCategories
        {
            get
            {
                EnsureSettingsLoaded();

                if (nowThenRecordCats == null || nowThenRecordCats.Count == 0)
                {
                    // Setup Default settings
                    nowThenRecordCats = new List<RecordCategory>();
                    nowThenRecordCats.Add(new RecordCategory(CommonResources.Text.LabelDistance, RecordCategory.RecordType.Distance, GetAllRefIDs()));
                    nowThenRecordCats.Add(new RecordCategory(CommonResources.Text.LabelTotalTime, RecordCategory.RecordType.TotalTime, GetAllRefIDs()));
                    nowThenRecordCats.Add(new RecordCategory(CommonResources.Text.LabelSpeed + " / " + CommonResources.Text.LabelPace, RecordCategory.RecordType.SpeedPace, GetAllRefIDs()));
                    nowThenRecordCats.Add(new RecordCategory(CommonResources.Text.LabelAvgHR, RecordCategory.RecordType.AvgHR, GetAllRefIDs()));
                    nowThenRecordCats.Add(new RecordCategory(CommonResources.Text.LabelMaxHR, RecordCategory.RecordType.MaxHR, GetAllRefIDs()));

                    if (CustomDataFields.TRIMPexists)
                        nowThenRecordCats.Add(new RecordCategory(Resources.Strings.Label_MaxTRIMP, RecordCategory.RecordType.TRIMP, GetAllRefIDs()));

                    nowThenRecordCats.Add(new RecordCategory(CommonResources.Text.LabelElevationChange, RecordCategory.RecordType.ElevationDifference, GetAllRefIDs()));
                    nowThenRecordCats.Add(new RecordCategory(CommonResources.Text.LabelAscending + " / " + CommonResources.Text.LabelDescending, RecordCategory.RecordType.AscentDescent, GetAllRefIDs()));
                    nowThenRecordCats.Add(new RecordCategory(Resources.Strings.Label_Max + " / " + Resources.Strings.Label_Min + " " + CommonResources.Text.LabelTemperature, RecordCategory.RecordType.HottestColdest, GetAllRefIDs()));
                    nowThenRecordCats.Add(new RecordCategory(CommonResources.Text.LabelFastestSpeed, RecordCategory.RecordType.MaxSpeed, GetAllRefIDs()));
                    nowThenRecordCats.Add(new RecordCategory(CommonResources.Text.LabelFastestPace, RecordCategory.RecordType.MaxPace, GetAllRefIDs()));
                    nowThenRecordCats.Add(new RecordCategory(Resources.Strings.Label_Max + " / " + Resources.Strings.Label_Min + " " + CommonResources.Text.LabelElevation, RecordCategory.RecordType.MaxMinElevation, GetAllRefIDs()));
                    nowThenRecordCats.Add(new RecordCategory(CommonResources.Text.LabelAvgCadence, RecordCategory.RecordType.AvgCadence, GetAllRefIDs()));
                    nowThenRecordCats.Add(new RecordCategory(CommonResources.Text.LabelMaxCadence, RecordCategory.RecordType.MaxCadence, GetAllRefIDs()));
                    nowThenRecordCats.Add(new RecordCategory(CommonResources.Text.LabelAvgPower, RecordCategory.RecordType.AvgPower, GetAllRefIDs()));
                    nowThenRecordCats.Add(new RecordCategory(CommonResources.Text.LabelMaxPower, RecordCategory.RecordType.MaxPower, GetAllRefIDs()));

                    if (CustomDataFields.TSSexists)
                        nowThenRecordCats.Add(new RecordCategory(Resources.Strings.Label_MaxTSS, RecordCategory.RecordType.TSS, GetAllRefIDs()));

                    nowThenRecordCats.Add(new RecordCategory(CommonResources.Text.LabelCalories, RecordCategory.RecordType.TotalCalories, GetAllRefIDs()));
                    nowThenRecordCats.Add(new RecordCategory(Resources.Strings.Label_Max + " / " + Resources.Strings.Label_Min + " " + CommonResources.Text.LabelGrade, RecordCategory.RecordType.MaxMinGrade, GetAllRefIDs()));
                }

                return nowThenRecordCats;
            }

            set
            {
                nowThenRecordCats = value;
                ChartData.IsSuperDirty = true;
                Modified = true;
            }
        }

        /// <summary>
        /// Gets or sets distance record categories stored in user profile
        /// </summary>
        public static IList<string> DistanceChartItems
        {
            get
            {
                EnsureSettingsLoaded();
                if (distanceChartItems == null || distanceChartItems.Count == 0)
                {
                    distanceChartItems = new List<string> { ColumnDefinition.speedID };
                    return distanceChartItems;
                }
                else
                {
                    return distanceChartItems;
                }
            }

            set
            {
                distanceChartItems = value;
                Modified = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the settings have been loaded from the logbook.
        /// </summary>
        public static bool Loaded
        {
            set
            {
                loaded = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the number of records to
        /// display.  Ex: 25, means show only the top 25 performances
        /// on the record tables.
        /// </summary>
        public static int NumRecords
        {
            get { return numRecords; }
            set
            {
                if (value != numRecords)
                {
                    numRecords = value;
                    Modified = true;
                }
            }
        }

        /// <summary>
        /// Beginning fileter date.  Default value is min date (include all activities)
        /// </summary>
        public static DateTime StartDateFilter
        {
            get
            {
                return startDateFilter;
            }
            set
            {
                if (startDateFilter != value)
                {
                    startDateFilter = value;
                    RaisePropertyChanged("StartDateFilter");
                }
            }
        }

        /// <summary>
        /// Ending fileter date.  Default value is max date (include all activities)
        /// </summary>
        public static DateTime EndDateFilter
        {
            get
            {
                if (endDateFilter == DateTime.MinValue)
                    return DateTime.MaxValue;

                return endDateFilter;
            }
            set
            {
                if (endDateFilter != value)
                {
                    endDateFilter = value;
                    RaisePropertyChanged("EndDateFilter");
                }
            }
        }

        public static Version Version
        {
            get
            {
                if (!string.IsNullOrEmpty(version))
                    return new Version(version);
                else
                    return new Version(0, 0);
            }
        }

        #endregion

        #region Public Utilities

        /// <summary>
        /// Load user settings from logbook
        /// </summary>
        public static void LoadSettings()
        {
            ILogbook logbook = PluginMain.GetApplication().Logbook;

            if (logbook != null)
            {
                byte[] byteUserData = logbook.GetExtensionData(new PluginMain().Id);

                if (byteUserData.Length > 0)
                {
                    // Deserialize settings data from logbook
                    try
                    {
                        MemoryStream stream = new MemoryStream(byteUserData);
                        BinaryFormatter formatter = new BinaryFormatter();
                        formatter.Binder = new Binder(); // Help Deserializer resolve my class
                        stream.Position = 0;
                        formatter.Deserialize(stream);
                        loaded = true;
                    }
                    catch (Exception e)
                    {
                        loaded = true;
                    }
                }
                else
                {
                    LoadDefaultSettings();
                }
            }
        }

        /// <summary>
        /// Initialize all Training Load settings to default values.
        /// </summary>
        public static void LoadDefaultSettings()
        {
            ILogbook logbook = PluginMain.GetApplication().Logbook;

            modified = true;
            userColumns_AllActivites = string.Empty;
            userColumns_Distance = string.Empty;
            userColumns_Extreme = string.Empty;
            nowThenRecordCats = null;
            distanceRecordCats = null;
            extremeRecordCats = null;
            distance_chart_items = null;
            loaded = true;
            numRecords = 25;
        }

        /// <summary>
        /// Save all user settings, and mark logbook as Modified.
        /// </summary>
        public static void StoreSettings()
        {
            ILogbook logbook = PluginMain.GetApplication().Logbook;

            // Store Serialized Data
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, new UserData());
            logbook.SetExtensionData(RecordBook.GUIDs.PluginMain, stream.ToArray());

            logbook.Modified = true;
        }

        /// <summary>
        /// Returns category refIds for My Activities and all subcategories in a single string separated with '|'.  Excludes 'My Friends Activities'.
        /// </summary>
        /// <returns>A single string of activity categories delimited with '|'</returns>
        public static List<string> GetAllRefIDs()
        {
            List<string> refIDs = new List<string>();
            if (PluginMain.GetApplication().Logbook.ActivityCategories.Count > 0)
            {
                IActivityCategory cat = PluginMain.GetApplication().Logbook.ActivityCategories[0];
                GetActivityCategories(cat, ref refIDs);
            }

            return refIDs;
        }

        public static void GetActivityCategories(IActivityCategory cat, ref List<string> display)
        {
            if (cat.SubCategories.Count != 0)
            {
                display.Add(cat.ReferenceId);
                for (int i = 0; i < cat.SubCategories.Count; i++)
                {
                    GetActivityCategories(cat.SubCategories[i], ref display);
                }
            }
            else
            {
                display.Add(cat.ReferenceId);
            }
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Updates a record.  Unique identifier RefId
        /// If record does not exist, it is added.
        /// </summary>
        /// <param name="catType">Which record category set is to be updated</param>
        /// <param name="updateItem">Item to add/update.</param>
        internal static void UpdateCategory(CatType catType, RecordCategory updateItem)
        {
            List<RecordCategory> records;

            // Set which recordset we'll be modifying
            switch (catType)
            {
                case CatType.Distance:
                    records = DistanceRecordCategories;
                    break;

                case CatType.Extremes:
                    records = ExtremeRecordCategories;
                    break;

                case CatType.NowThen:
                    records = NowThenRecordCategories;
                    break;

                default:
                    return;
            }

            // Attempt to remove existing record
            int index = records.Count;
            for (int i = 0; i < records.Count; i++)
            {
                if (records[i].RefId == updateItem.RefId)
                {
                    index = i;
                    records.Remove(records[i]);
                    break;
                }
            }

            // Add new (or updated) record
            records.Insert(index, updateItem);

            UserData.StoreSettings();
            ChartData.IsSuperDirty = true;
        }

        internal static void EnsureSettingsLoaded()
        {
            if (loaded == false)
            {
                LoadSettings();
                Migrate.MigrateSettings();
            }
        }

        /// <summary>
        /// To convert a Byte Array of Unicode values (UTF-8 encoded) to a complete String.
        /// </summary>
        /// <param name="characters">Unicode Byte Array to be converted to String</param>
        /// <returns>String converted from Unicode Byte Array</returns>
        internal static string UTF8ByteArrayToString(byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            string constructedString = encoding.GetString(characters);
            return constructedString;
        }

        /// <summary>
        /// Converts the String to UTF8 Byte array and is used in De serialization
        /// </summary>
        /// <param name="pXmlString"></param>
        /// <returns></returns>
        internal static byte[] StringToUTF8ByteArray(string pXmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        }

        private static string GetCategoryXMLString(List<RecordCategory> categories)
        {
            string xmlizedString = string.Empty;

            MemoryStream memoryStream = new MemoryStream();
            XmlSerializer xs = new XmlSerializer(typeof(List<RecordCategory>));
            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
            xs.Serialize(xmlTextWriter, categories);
            memoryStream = (MemoryStream)xmlTextWriter.BaseStream;

            xmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());

            return xmlizedString;
        }

        private static void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(null, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region ISerializable Members

        /// <summary>
        /// Serialize data
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Get XML string describing RecordCategory List for each of the 3 categories
            distance_Record_Categories = GetCategoryXMLString(DistanceRecordCategories);
            extreme_Record_Categories = GetCategoryXMLString(ExtremeRecordCategories);
            nowThen_Record_Categories = GetCategoryXMLString(NowThenRecordCategories);

            distance_chart_items = string.Empty;
            foreach (string item in DistanceChartItems)
            {
                distance_chart_items += item + ";";
            }
            distance_chart_items.TrimEnd(';');

            // Store data during serialization
            info.SetType(typeof(UserData));
            info.AddValue("UserColumns_AllActivities", userColumns_AllActivites);
            info.AddValue("UserColumns_Distance", userColumns_Distance);
            info.AddValue("UserColumns_Extreme", userColumns_Extreme);
            info.AddValue("Distance_Record_Categories", distance_Record_Categories);
            info.AddValue("Extreme_Record_Categories", extreme_Record_Categories);
            info.AddValue("NowThen_Record_Categories", nowThen_Record_Categories);
            info.AddValue("Distance_chart_items", distance_chart_items);
            info.AddValue("NumRecords", numRecords);
            info.AddValue("Version", GetType().Assembly.GetName().Version.ToString(3));
        }

        /// <summary>
        /// Deserialization
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected UserData(SerializationInfo info, StreamingContext context)
        {
            try
            {
                userColumns_AllActivites = info.GetString("UserColumns_AllActivities");
                newInstall = false;
                userColumns_Distance = info.GetString("UserColumns_Distance");
                userColumns_Extreme = info.GetString("UserColumns_Extreme");

                distance_chart_items = info.GetString("Distance_chart_items");
                distance_Record_Categories = info.GetString("Distance_Record_Categories");
                extreme_Record_Categories = info.GetString("Extreme_Record_Categories");
                nowThen_Record_Categories = info.GetString("NowThen_Record_Categories");

                string[] distChartArray = distance_chart_items.Split(';');
                distanceChartItems = new List<string>();
                foreach (string chart in distChartArray)
                {
                    distanceChartItems.Add(chart);
                }

                distanceRecordCats = DeserializeRecordCategoriesFromXML(distance_Record_Categories);
                nowThenRecordCats = DeserializeRecordCategoriesFromXML(nowThen_Record_Categories);
                extremeRecordCats = DeserializeRecordCategoriesFromXML(extreme_Record_Categories);
                numRecords = info.GetInt32("NumRecords");
                version = info.GetString("Version");
            }
            catch (Exception e)
            { }
        }

        private static List<RecordCategory> DeserializeRecordCategoriesFromXML(string categories)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<RecordCategory>));
            MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(categories));
            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

            object deserialize = xs.Deserialize(memoryStream);
            return (List<RecordCategory>)deserialize;
        }

        private class Binder : SerializationBinder
        {
            public override Type BindToType(string assemblyName, string typeName)
            {
                Type tyType = null;
                string shortName = assemblyName.Split(',')[0];
                System.Reflection.Assembly[] ayAssemblies = AppDomain.CurrentDomain.GetAssemblies();

                foreach (System.Reflection.Assembly ayAssembly in ayAssemblies)
                {
                    if (shortName == ayAssembly.FullName.Split(',')[0])
                    {
                        tyType = ayAssembly.GetType(typeName);
                        break;
                    }
                }

                return tyType;
            }
        }

        #endregion
    }
}
