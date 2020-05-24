
namespace RecordBook.Settings
{
    using System.Globalization;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Visuals;
    using System.Windows.Forms;

    class SettingsPage : ISettingsPage
    {
        #region Private Members

        private static SettingsPageControl control;
        private static bool pageActive;

        #endregion

        /// <summary>
        /// Gets a value indicating if the Settings Page is currently displayed.
        /// </summary>
        public static bool IsActiveDisplay
        {
            get { return pageActive; }
        }

        internal static SettingsPageControl CurrentControl
        {
            get
            {
                if (control == null)
                {
                    control = new SettingsPageControl();
                }

                return control;
            }
        }

        #region ISettingsPage Members

        /// <summary>
        /// Gets the Settings Page GUID
        /// </summary>
        public Guid Id
        {
            get { return GUIDs.RBSettingsPage; }
        }

        /// <summary>
        /// Gets the Settings Page SubPages
        /// </summary>
        public IList<ISettingsPage> SubPages
        {
            get { return null; }
        }

        #endregion

        #region IDialogPage Members

        /// <summary>
        /// Create SettingsPage control
        /// </summary>
        /// <returns>SettingsPage control</returns>
        public Control CreatePageControl()
        {
            if (control == null)
            {
                control = CurrentControl;
            }

            return control;
        }

        public bool HidePage()
        {
            // Save any changes on settings exit
            PluginMain.GetApplication().PropertyChanged -= SportTracksApplication_PropertyChanged;
            pageActive = false;
            return true;
        }

        public string PageName
        {
            get { return Resources.Strings.Label_RecordBook; }
        }

        public void ShowPage(string bookmark)
        {
            // Populate page with the logbook data
            UserData.LoadSettings();

            // Initialize some items only if this is not already the active view
            if (IsActiveDisplay)
            {
                PluginMain.GetApplication().PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(SportTracksApplication_PropertyChanged);
            }

            pageActive = true;
        }

        public IPageStatus Status
        {
            get { return null; }
        }

        public void ThemeChanged(ITheme visualTheme)
        {
            if (control != null)
            {
                control.ThemeChanged(visualTheme);
            }
        }

        public string Title
        {
            get { return Resources.Strings.Label_RecordBook; }
        }

        public void UICultureChanged(CultureInfo culture)
        {
            if (control != null)
            {
                control.UICultureChanged(culture);
            }
        }

        #endregion

        private void SportTracksApplication_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Logbook")
            {
                // Logbook was changed.  Reload data from new logbook.
                UserData.Loaded = false;
            }
        }

        #region INotifyPropertyChanged Members

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
