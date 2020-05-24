using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Fitness;
namespace RecordBook.UI
{
    public partial class RecordPane : IDetailPage
    {
        #region Fields

        private IActivity activity;
        private RecordPaneControl control;

        #endregion

        #region Properties

        public IActivity Activity
        {
            set
            {
                activity = value;

                RecordPaneControl control = CreatePageControl() as RecordPaneControl;
                control.Activity = activity;
                control.RefreshPage();
            }
        }

        #endregion

        #region IDialogPage Members

        public Control CreatePageControl()
        {
            if (control == null)
            {
                control = new RecordPaneControl();
            }

            return control;
        }

        public bool HidePage()
        {
            return true;
        }

        public string PageName
        {
            get { return Resources.Strings.Label_RecordBook; }
        }

        public void ShowPage(string bookmark)
        {
            control.RefreshPage();
        }

        public IPageStatus Status
        {
            get { throw new NotImplementedException(); }
        }

        public void ThemeChanged(ITheme visualTheme)
        {
            control.ThemeChanged(visualTheme);
        }

        public string Title
        {
            get { return Resources.Strings.Label_RecordBook; }
        }

        public void UICultureChanged(System.Globalization.CultureInfo culture)
        {
            control.UICultureChanged(culture);
        }

        #endregion

        #region IDetailPage Members

        public Guid Id
        {
            get { return GUIDs.RecordBookDetail; }
        }

        public bool MenuEnabled
        {
            get { return true; }
        }

        public IList<string> MenuPath
        {
            get { return null; }
        }

        public bool MenuVisible
        {
            get { return true; }
        }

        public bool PageMaximized
        {
            get
            {
                return false ;
            }
            set
            {
                
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
