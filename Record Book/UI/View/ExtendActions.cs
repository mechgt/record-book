namespace RecordBook.UI.View
{
    using System.Collections.Generic;
    using ZoneFiveSoftware.Common.Visuals;
    using ZoneFiveSoftware.Common.Visuals.Fitness;

    class ExtendActions : IExtendViews, IExtendActivityDetailPages
    {
        #region IExtendViews Members
        public IList<IView> Views
        {
            get
            {
                // Create & return the new menu item under 'Select View'
                IList<IView> views = new List<IView>();
                views.Add(new ViewRecordBookPage());
                return views;
            }
        }
        #endregion


        #region IExtendActivityDetailPages Members

        public IList<IDetailPage> GetDetailPages(IDailyActivityView view, ExtendViewDetailPages.Location location)
        {
            // TODO: (LOW) Add detail page (Not yet implemented)
            return null;
        }

        #endregion
    }
}