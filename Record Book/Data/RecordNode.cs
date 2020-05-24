namespace RecordBook.Data
{
    using RecordBook.UI.View;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using ZoneFiveSoftware.Common.Data;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Data.GPS;
    using ZoneFiveSoftware.Common.Data.Measurement;
    using ZoneFiveSoftware.Common.Visuals;

    /// <summary>
    /// Record Set as TreeListNode for display on a TreeList
    /// </summary>
    class RecordNode : TreeList.TreeListNode
    {
        #region Fields


        #endregion

        #region Constructor

        public RecordNode(RecordNode parent, RecordSet element)
            : base(parent, element)
        {
        }

        public RecordNode(RecordNode parent, Record element)
            : base(parent, element)
        {
        }

        #endregion

        #region Properties

        public bool IsRecord
        {
            get { return Element is Record; }
        }

        public bool IsRecordSet
        {
            get { return Element is RecordSet; }
        }

        public string Rank
        {
            get
            {
                if (this.IsRecordSet)
                    return RecSet.CategoryName;
                else
                    return Record.CategoryName;
            }
        }

        public RecordSet RecSet
        {
            get { return Element as RecordSet; }
        }

        public Record Record
        {
            get { return Element as Record; }
        }

        public bool Expanded
        {
            get;
            set;
        }
        #endregion
    }
}
