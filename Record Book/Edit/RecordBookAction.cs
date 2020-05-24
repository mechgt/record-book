using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;

using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Data.GPS;
using ZoneFiveSoftware.Common.Visuals;

namespace RecordBook.Edit
{
    class RecordBookAction : IAction
    {
        #region Fields

        private IList<IActivity> activities;

        #endregion

        #region Constructor

        public RecordBookAction(IList<IActivity> activities)
        {
            this.activities = activities;
        }

        #endregion
        
        #region IAction Members

        public bool Enabled
        {
            get { return true; }
        }

        public bool HasMenuArrow
        {
            get { return false; }
        }

        public Image Image
        {
            get
            {
                //return Resources.Resources.Image_16_Reverse;
                return null;
            }
        }

        public void Refresh()
        {

        }

        public void Run(Rectangle rectButton)
        {
            Debug.WriteLine("No. of activities selected: " + activities.Count);
            //TODO: Is there some way to store the activities somewhere else?!?

        }

        public string Title
        {
            get { return "Record Book"; }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
