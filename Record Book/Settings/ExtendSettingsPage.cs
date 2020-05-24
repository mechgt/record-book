namespace RecordBook.Settings
{
    using System.Collections.Generic;
    using ZoneFiveSoftware.Common.Visuals;
    using ZoneFiveSoftware.Common.Visuals.Fitness;
    
    class ExtendSettingsPages : IExtendSettingsPages
    {
        #region IExtendSettingsPages Members

        public IList<ISettingsPage> SettingsPages
        {
            get
            {
                // Create & return the new menu item under 'Select View'
                IList<ISettingsPage> views = new List<ISettingsPage>();
                views.Add(new SettingsPage());
                return views;
            }
        }

        #endregion
    }
}
