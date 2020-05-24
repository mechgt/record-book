namespace RecordBook
{
    using System;
    using System.ComponentModel;
    using System.Xml;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Visuals.Fitness;
    using ZoneFiveSoftware.Common.Data.Measurement;

    class PluginMain : IPlugin
    {
        #region Fields

        private static IApplication application;
        private static bool logbookChanged;

        #endregion

        #region Mechgt License Fields

        /// <summary>
        /// Plugin product Id as listed in license application
        /// </summary>
        internal static string ProductId
        {
            get
            {
                return "rb";
            }
        }

        internal static string SupportEmail
        {
            get
            {
                return "mechgt@gmail.com";
            }
        }

        /// <summary>
        /// Number of days in history that data will be displayed.
        /// This is the amount of data that will be displayed, not a number of days for a trial version.
        /// </summary>
        internal static int EvalDays
        {
            get { return 120; }
        }

        /// <summary>
        /// Number of days in history that data will be displayed.
        /// This is the amount of data that will be displayed, not a number of days for a trial version.
        /// </summary>
        internal static int EvalCategories
        {
            get { return 5; }
        }

        #endregion

        public IApplication Application
        {
            set
            {
                if (application != null)
                {
                    application.PropertyChanged -= new PropertyChangedEventHandler(AppPropertyChanged);
                }

                application = value;

                if (application != null)
                {
                    application.PropertyChanged += new PropertyChangedEventHandler(AppPropertyChanged);
                    logbookChanged = true;
                }
            }
        }

        public Guid Id
        {
            get { return RecordBook.GUIDs.PluginMain; }
        }

        public string Name
        {
            get { return Resources.Strings.Label_RecordBook; }
        }

        public string Version
        {
            get
            {
                string version = GetType().Assembly.GetName().Version.ToString(3);
                return version;
            }
        }

        public static IApplication GetApplication()
        {
            return application;
        }

        public static ILogbook GetLogbook()
        {
            return GetApplication().Logbook;
        }

        public void ReadOptions(XmlDocument xmlDoc, XmlNamespaceManager nsmgr, XmlElement pluginNode)
        {            
        }

        public void WriteOptions(XmlDocument xmlDoc, XmlElement pluginNode)
        {
        }

        private XmlElement XmlSetting(XmlDocument xmlDoc, string name, string value)
        {
            XmlElement child;

            child = xmlDoc.CreateElement(name);
            child.AppendChild(xmlDoc.CreateTextNode(value));

            return child;
        }

        private void AppPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Fired when Logbook is switched (open new logbook)
            if (e != null && e.PropertyName == "Logbook")
            {
                logbookChanged = true;
                RecordBook.Settings.UserData.EnsureSettingsLoaded();
            }
        }

        #region Commonly Accessed Application/Logbook Properties

        /// <summary>
        /// Returns user defined distance units
        /// </summary>
        internal static Length.Units DistanceUnits
        {
            get { return application.SystemPreferences.DistanceUnits; }
        }

        #endregion
    }
}
