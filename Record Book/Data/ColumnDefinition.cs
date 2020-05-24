using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Visuals;
using System.Drawing;

namespace RecordBook.Data
{
    class ColumnDefinition : IListColumnDefinition
    {
        public const string cadenceID = "bf098136-ff1b-4ccd-89d4-fe2407437a6e";
        public const string elevationID = "382122b0-d9cb-4ec0-a0cf-b6042e1aa2ff";
        public const string gradeID = "ce7e8fb8-bd2f-4ba0-8c3c-6396b21b6e19";
        public const string hrID = "209f292b-044b-42f7-ad47-3c760d3d3be8";
        public const string powerID = "ecb3b972-bb88-4794-99c0-c2a34b54ef9d";
        public const string speedID = "00a34df6-fc29-4cf3-9b87-29c8c0dd931c";

        private string id;
        private int width;

        public ColumnDefinition(string id, int width)
        {
            this.id = id;
            this.width = width;
        }

        #region IListColumnDefinition Members

        public StringAlignment Align
        {
            get { return StringAlignment.Near; }
        }

        public string GroupName
        {
            get { return null; }
        }

        public string Id
        {
            get { return id; }
        }

        public string Text(string columnId)
        {
            switch (columnId)
            {
                case cadenceID:
                    return CommonResources.Text.LabelCadence;
                case elevationID:
                    return CommonResources.Text.LabelElevation;
                case gradeID:
                    return CommonResources.Text.LabelGrade;
                case hrID:
                    return CommonResources.Text.LabelHeartRate;
                case powerID:
                    return CommonResources.Text.LabelPower;
                case speedID:
                    return CommonResources.Text.LabelSpeed + "/" + CommonResources.Text.LabelPace;
            }

            return "Bad Data";
        }

        public override string ToString()
        {
            return Text(id);
        }

        public int Width
        {
            get { return width; }
        }

        #endregion
    }
}
