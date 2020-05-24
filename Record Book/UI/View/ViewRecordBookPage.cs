namespace RecordBook.UI.View
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Visuals;
    using ZoneFiveSoftware.Common.Visuals.Fitness;
    using RecordBook.Data;

    class ViewRecordBookPage : IView
    {
        #region Fields

        private static RecordBookControl control;

        #endregion

        #region Constructor

        /// <summary>
        /// Analyze all activities.
        /// </summary>
        public ViewRecordBookPage()
        {
            PluginMain.GetApplication().PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(SportTracksApplication_PropertyChanged);
        }

        #endregion

        internal static RecordBookControl CurrentControl
        {
            get
            {
                if (control == null)
                {
                    // Initialize
                    control = new RecordBookControl();
                }

                return control;
            }
        }

        public static bool IsActiveDisplay
        {
            get { return PluginMain.GetApplication().ActiveView.Id == GUIDs.RecordBookPage; }
        }

        #region IView Members

        public IList<IAction> Actions
        {
            get { return null; }
        }

        public Guid Id
        {
            get { return GUIDs.RecordBookPage; }
        }

        public string SubTitle
        {
            get
            {
                IActivityCategory selected = PluginMain.GetApplication().DisplayOptions.SelectedCategoryFilter;
                string subtitle = selected.Name;
                while (selected.Parent != null)
                {
                    selected = selected.Parent;
                    subtitle = selected.Name + ": " + subtitle;
                }

                return subtitle;
            }
        }

        public void SubTitleClicked(Rectangle subTitleRect)
        {
            OpenActivityCategoriesFilterPopup(subTitleRect);
        }

        public bool SubTitleHyperlink
        {
            get { return true; }
        }

        public string TasksHeading
        {
            get { return null; }
        }

        #endregion

        #region IDialogPage Members

        /// <summary>
        /// Called the first time the page is loaded.
        /// </summary>
        /// <returns></returns>
        public Control CreatePageControl()
        {
            // Filter Activites for initial load.
            if (control == null)
            {
                control = CurrentControl;
            }

            return control;
        }

        public bool HidePage()
        {
            // Remove handler to interact with calender changes
            PluginMain.GetApplication().Calendar.SelectedChanged -= CurrentControl.Calendar_SelectedChanged;
            return true;
        }

        public string PageName
        {
            get { return Title; }
        }

        public void ShowPage(string bookmark)
        {
            // Filter activities to be shown on this page if necessary
            IActivityCategory selectedCategory = PluginMain.GetApplication().DisplayOptions.SelectedCategoryFilter;

            // TODO: (TEST) Re-rank records as filter changes
            // TODO: (TEST) Re-rank records as activities change, or are added/removed
            ChartData.FilterCategory = selectedCategory;

            // Only refresh if the logbook has changed since last time.
            if (ChartData.IsDirty)
                CurrentControl.RefreshPage();

            // Add handler to interact with calender changes
            PluginMain.GetApplication().Calendar.SelectedChanged += CurrentControl.Calendar_SelectedChanged;
        }

        public IPageStatus Status
        {
            get { return null; }
        }

        public void ThemeChanged(ITheme visualTheme)
        {
            CurrentControl.ThemeChanged(visualTheme);
        }

        public string Title
        {
            get { return Resources.Strings.Label_RecordBook; }
        }

        public void UICultureChanged(CultureInfo culture)
        {
            CurrentControl.UICultureChanged(culture);
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public/Private methods

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void SportTracksApplication_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Logbook")
            {
                // Need to recalculate data anytime logbook is changed
                ILogbook logbook = PluginMain.GetApplication().Logbook;
                if (logbook != null)
                {
                    PluginMain.GetApplication().Logbook.PropertyChanged += new PropertyChangedEventHandler(Logbook_DataChanged);
                    ChartData.IsSuperDirty = true;
                }
            }
        }

        private void Logbook_DataChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Activities")
            {
                ChartData.IsDirty = true;
            }
        }

        public void OpenActivityCategoriesFilterPopup(Rectangle subTitleRect)
        {
            // From: http://www.zonefivesoftware.com/sporttracks/Forums/viewtopic.php?p=40402
            //OpenActivityCategoriesFilterPopup(subTitleRect);

            List<PopupCategory> categories = new List<PopupCategory>();
            ILogbook logbook = PluginMain.GetLogbook();
            ITheme theme = PluginMain.GetApplication().VisualTheme;

            // Collect all categories
            SearchSubcategory(logbook.ActivityCategories, ref categories, 0);

            int i = 0;
            string name;
            foreach (PopupCategory cat in categories)
            {
                name = string.Empty;
                for (int j = 0; j < cat.Depth; j++)
                {
                    name = name + "   ";
                }

                cat.DisplayName = name + cat.Category.Name;
                cat.DisplayIndex = i;
                i++;
            }

            // Create TreeListPopup
            TreeListPopup popup = new TreeListPopup();
            popup.ThemeChanged(theme);
            popup.Tree.Columns.Add(new TreeList.Column("DisplayName"));
            popup.Tree.RowData = categories;

            popup.ItemSelected += delegate(object sender, TreeListPopup.ItemSelectedEventArgs e)
            {
                CategoryChangedHandler(sender, e);
            };

            // Show the popup
            popup.Popup(subTitleRect);
        }

        /// <summary>
        /// Handler for changing category selection filter
        /// </summary>
        /// <param name="sender">The paratmeter is not used.</param>
        /// <param name="e">Used to acquire activity category</param>
        private void CategoryChangedHandler(object sender, TreeListPopup.ItemSelectedEventArgs e)
        {
            // Add handler for when something is selected.
            // Store category selected in ST application
            IActivityCategory selectedCategory = ((PopupCategory)e.Item).Category;
            if (PluginMain.GetApplication().DisplayOptions.SelectedCategoryFilter != selectedCategory)
            {
                PluginMain.GetApplication().DisplayOptions.SelectedCategoryFilter = selectedCategory;

                // Notify that Subtitle has changed
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SubTitle"));

                // Filter activities to be shown on this page if necessary
                ChartData.FilterCategory = selectedCategory;

                // Refresh display
                CurrentControl.RefreshPage();
            }
        }

        /// <summary>
        /// Traverses through all categories in order and records their depth within the tree
        /// </summary>
        /// <param name="subCategory">List of Categories to search</param>
        /// <param name="categories">List of PopupCategory (By Reference)</param>
        /// <param name="depth">Depth associated with subCategory</param>
        private void SearchSubcategory(IEnumerable<IActivityCategory> subCategory, ref List<PopupCategory> categories, int depth)
        {
            foreach (IActivityCategory activityCategory in subCategory)
            {
                PopupCategory catInfo = new PopupCategory();
                catInfo.Category = activityCategory;
                catInfo.Depth = depth;

                categories.Add(catInfo);
                if (activityCategory.SubCategories.Count > 0)
                {
                    SearchSubcategory(activityCategory.SubCategories, ref categories, depth + 1);
                }
            }
        }


        #endregion

        private class PopupCategory
        {
            #region Fields

            private IActivityCategory category;
            private string textName;
            private int depth;
            private int displayIndex;

            #endregion

            #region Properties

            public IActivityCategory Category
            {
                get { return category; }

                set { category = value; }
            }

            public string DisplayName
            {
                get { return textName; }

                set { textName = value; }
            }

            public int Depth
            {
                get { return depth; }

                set { depth = value; }
            }

            public int DisplayIndex
            {
                get { return displayIndex; }

                set { displayIndex = value; }
            }

            public string ReferenceId
            {
                get { return category.ReferenceId; }
            }

            #endregion
        }
    }
}
