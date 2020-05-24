using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Visuals;
using System.Drawing;
using RecordBook.Settings;
using RecordBook.Data;

namespace RecordBook.UI.View
{
    partial class RecordBookControl
    {
        /// <summary>
        /// BuildExtremesColumns will setup the column structure for the extremes treelist
        /// </summary>
        /// <returns></returns>
        private static List<TreeList.Column> GetExtremesColumns()
        {
            List<TreeList.Column> columns = new List<TreeList.Column>();
            columns.Add(new TreeList.Column("Rank", Resources.Strings.Label_Rank, 200, StringAlignment.Near));
            columns.Add(new TreeList.Column("StartTime", CommonResources.Text.LabelStartTime, 150, StringAlignment.Near));
            columns.Add(new TreeList.Column("RecValue", CommonResources.Text.LabelValue, 75, StringAlignment.Near));
            columns.Add(new TreeList.Column("Location", CommonResources.Text.LabelLocation, 80, StringAlignment.Near));
            columns.Add(new TreeList.Column("Name", CommonResources.Text.LabelName, 80, StringAlignment.Near));
            columns.Add(new TreeList.Column("ActivityCategory", CommonResources.Text.LabelCategory, 80, StringAlignment.Near));
            columns.Add(new TreeList.Column("TotalDistanceMeters", CommonResources.Text.LabelDistance + " (" + Units.Distance + ")", 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("TotalTime", CommonResources.Text.LabelTime, 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("AvgPace", CommonResources.Text.LabelAvgPace + " (" + Units.Pace + ")", 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("AvgHR", CommonResources.Text.LabelAvgHR, 80, StringAlignment.Far));

            if (CustomDataFields.TRIMPexists)
                columns.Add(new TreeList.Column("TRIMP", Resources.Strings.Label_MaxTRIMP, 80, StringAlignment.Far));

            columns.Add(new TreeList.Column("EndTime", Resources.Strings.Label_End, 80, StringAlignment.Near));
            columns.Add(new TreeList.Column("Temp", CommonResources.Text.LabelTemperature + " (" + Units.Temp + ")", 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("ElevationChange", CommonResources.Text.LabelElevationChange + " (" + Units.Elevation + ")", 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("MaxHR", CommonResources.Text.LabelMaxHR, 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("MetersPerSecond", CommonResources.Text.LabelSpeed + " (" + Units.Speed + ")", 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("TotalAscend", CommonResources.Text.LabelAscending + " (" + Units.Elevation + ")", 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("TotalDescend", CommonResources.Text.LabelDescending + " (" + Units.Elevation + ")", 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("MaxSpeed", CommonResources.Text.LabelFastestSpeed + " (" + Units.Speed + ")", 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("MaxElevation", Resources.Strings.Label_MaxElevation + " (" + Units.Elevation + ")", 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("MinElevation", Resources.Strings.Label_MinElevation + " (" + Units.Elevation + ")", 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("MaxCadence", CommonResources.Text.LabelMaxCadence, 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("AvgCadence", CommonResources.Text.LabelAvgCadence, 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("MaxPower", CommonResources.Text.LabelMaxPower, 80, StringAlignment.Far));

            if (CustomDataFields.TSSexists)
                columns.Add(new TreeList.Column("TSS", Resources.Strings.Label_MaxTSS, 80, StringAlignment.Far));

            columns.Add(new TreeList.Column("MaxPace", CommonResources.Text.LabelFastestPace, 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("AvgPower", CommonResources.Text.LabelAvgPower, 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("MaxGrade", CommonResources.Text.LabelMaxGrade, 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("AvgGrade", CommonResources.Text.LabelAvgGrade, 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("MinGrade", Resources.Strings.Label_MinGrade, 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("TotalCalories", CommonResources.Text.LabelCalories, 80, StringAlignment.Far));

            string[] userColumns = UserData.UserColumns_Extreme.Split(";".ToCharArray());

            foreach (TreeList.Column column in columns)
            {
                foreach (string columnID in userColumns)
                {
                    string[] column_data = columnID.Split("|".ToCharArray());
                    if (column.Id == column_data[0])
                    {
                        column.CanSelect = true;
                    }
                }
            }

            return columns;
        }
    }
}
