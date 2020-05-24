using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using RecordBook.Data;
using RecordBook.Settings;
using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Data.Measurement;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Chart;

namespace RecordBook.UI.View
{
    partial class RecordBookControl
    {
        /// <summary>
        /// BuildDistanceColumns will setup the column structure for the distance treelist
        /// </summary>
        /// <returns></returns>
        private static List<TreeList.Column> GetDistanceColumns()
        {
            // Setup the Distance columns
            List<TreeList.Column> columns = new List<TreeList.Column>();
            /* NOTE: All columns added will need to have:
             *  1) Sort method enumeration in TSSActivity.TSSActivityComparer.ComparisonType <-- Record comparer
             *  2) Define ComparerInstance in treeRecord_ColumnClicked
             *  3) Define how to sort in TSSActivity.CompareTO(...) method
             *  4) Setup the column for localization changes
             */
            PluginMain.GetApplication().SystemPreferences.ElevationUnits.ToString();

            columns.Add(new TreeList.Column("Rank", Resources.Strings.Label_Rank, 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("AvgPace", CommonResources.Text.LabelAvgPace + " (" + Units.Pace + ")", 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("TotalTime", CommonResources.Text.LabelTime, 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("StartTime", CommonResources.Text.LabelStartTime, 80, StringAlignment.Near));
            columns.Add(new TreeList.Column("EndTime", CommonResources.Text.LabelEndTime, 80, StringAlignment.Near));
            columns.Add(new TreeList.Column("StartDistanceMeters", CommonResources.Text.LabelStart + " (" + Units.Distance + ")", 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("EndDistanceMeters", Resources.Strings.Label_End + " (" + Units.Distance + ")", 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("AvgHR", CommonResources.Text.LabelAvgHR, 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("TotalDistanceMeters", CommonResources.Text.LabelDistance + " (" + Units.Distance + ")", 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("Temp", CommonResources.Text.LabelTemperature + " (" + Units.Temp + ")", 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("ActivityCategory", CommonResources.Text.LabelCategory, 80, StringAlignment.Near));
            columns.Add(new TreeList.Column("ElevationChange", CommonResources.Text.LabelElevationChange + " (" + Units.Elevation + ")", 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("Location", CommonResources.Text.LabelLocation, 80, StringAlignment.Near));
            columns.Add(new TreeList.Column("MaxHR", CommonResources.Text.LabelMaxHR, 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("MetersPerSecond", CommonResources.Text.LabelSpeed + " (" + Units.Speed + ")", 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("TotalAscend", CommonResources.Text.LabelAscending + " (" + Units.Elevation + ")", 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("TotalDescend", CommonResources.Text.LabelDescending + " (" + Units.Elevation + ")", 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("MaxSpeed", CommonResources.Text.LabelFastestSpeed + " (" + Units.Speed + ")", 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("MaxPace", CommonResources.Text.LabelFastestPace, 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("MaxElevation", Resources.Strings.Label_MaxElevation + " (" + Units.Elevation + ")", 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("MinElevation", Resources.Strings.Label_MinElevation + " (" + Units.Elevation + ")", 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("MaxCadence", CommonResources.Text.LabelMaxCadence, 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("AvgCadence", CommonResources.Text.LabelAvgCadence, 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("MaxPower", CommonResources.Text.LabelMaxPower, 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("AvgPower", CommonResources.Text.LabelAvgPower, 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("MaxGrade", CommonResources.Text.LabelMaxGrade, 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("MinGrade", Resources.Strings.Label_MinGrade, 80, StringAlignment.Far));
            columns.Add(new TreeList.Column("Name", CommonResources.Text.LabelName, 80, StringAlignment.Near));

            string[] userColumns = UserData.UserColumns_Distance.Split(";".ToCharArray());

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

        /// <summary>
        /// Draws a bar chart showing when the records were scored by date.
        /// </summary>
        /// <param name="rs">Set of records to chart</param>
        private void DrawActivityRankChart(RecordSet rs)
        {
            // Clear the chart
            distanceChart.DataSeries.Clear();

            IAxis axis = distanceChart.YAxis;
            ChartDataSeries ds = new ChartDataSeries(distanceChart, axis);
            DateTime firstActivity = DateTime.Today;

            // Parse the records for the first activity startDateTime
            for (int j = 0; j < rs.Count; j++)
            {
                if (firstActivity > rs[j].StartTime)
                {
                    firstActivity = rs[j].StartTime;
                }
            }

            // Loop the records
            for (int j = 0; j < rs.Count; j++)
            {
                // Get the difference between the start time of this record and the start time of the first activity
                PointF point = new PointF((rs[j].StartTime - firstActivity).Days, rs[j].Rank);
                ds.Points.Add(j, point);
            }

            // Set the formatter to a Date style
            Formatter.DaysToDate xformatter = new Formatter.DaysToDate(firstActivity);
            distanceChart.XAxis.Formatter = xformatter;
            Formatter.General yformatter = new Formatter.General();
            distanceChart.YAxis.Formatter = yformatter;

            // Format and display the chart
            ds.ChartType = ChartDataSeries.Type.Bar;
            distanceChart.XAxis.Label = CommonResources.Text.LabelDate;

            distanceChart.YAxis.Label = Resources.Strings.Label_Rank;
            distanceChart.DataSeries.Add(ds);
            distanceChart.AutozoomToData(true);
        }

        /// <summary>
        /// Draw charts of selected records in distance tab.
        /// </summary>
        /// <param name="recs"></param>
        private void DrawChartSelectedRecords(ArrayList recs)
        {
            this.Cursor = Cursors.WaitCursor;

            // Clear the chart
            distanceChart.DataSeries.Clear();

            // Count the number of activities that use pace vs speed
            // We will display units based on the most popular
            int paceCount = 0;
            int speedCount = 0;

            // Find the most popular speed unit
            for (int j = 0; j < recs.Count; j++)
            {
                RecordNode r = recs[j] as RecordNode;
                if (r != null)
                {
                    if (r.Record.Activity.Category.SpeedUnits == Speed.Units.Pace)
                    {
                        paceCount++;
                    }
                    else if (r.Record.Activity.Category.SpeedUnits == Speed.Units.Speed)
                    {
                        speedCount++;
                    }
                }
            }

            // Set the formatter to a general style
            Formatter.General formatter = new Formatter.General();
            distanceChart.XAxis.Formatter = formatter;
            distanceChart.XAxis.OriginValue = 0;
            distanceChart.XAxis.Label = CommonResources.Text.LabelDistance;
            distanceChart.YAxisRight.Clear();

            // Parse all the selected activities
            for (int j = 0; j < recs.Count; j++)
            {
                RecordNode r = recs[j] as RecordNode;
                if (r != null)
                {
                    ActivityInfo ai = ActivityInfoCache.Instance.GetInfo(r.Record.Activity);
                    string speedPaceLabel;
                    IAxisFormatter fmatter;
                    if (paceCount > speedCount)
                    {
                        speedPaceLabel = CommonResources.Text.LabelPace;

                        // Format the yAxis to be pace
                        fmatter = new Formatter.SecondsToTime();
                    }
                    else
                    {
                        speedPaceLabel = CommonResources.Text.LabelSpeed;
                        fmatter = new Formatter.General();
                    }

                    // First chart will be filled, others will not (fill is set to false after first pass)
                    bool fill = true;

                    foreach (string s in UserData.DistanceChartItems)
                    {
                        IAxis axis;

                        if (fill)
                        {
                            axis = distanceChart.YAxis;
                        }
                        else
                        {
                            axis = new RightVerticalAxis(distanceChart);
                        }

                        switch (s)
                        {
                            case ColumnDefinition.cadenceID:
                                axis.Label = CommonResources.Text.LabelCadence;
                                axis.LabelColor = Constants.GetDataColor(Constants.ColorIndex.Cadence);
                                DrawSeriesGraph(ai.SmoothedCadenceTrack, ai, axis, recs.Count, Constants.GetDataColor(Constants.ColorIndex.Cadence), fill);
                                break;

                            case ColumnDefinition.elevationID:
                                axis.Label = CommonResources.Text.LabelElevation;
                                axis.LabelColor = Constants.GetDataColor(Constants.ColorIndex.Elevation);
                                DrawSeriesGraph(ai.SmoothedElevationTrack, ai, axis, recs.Count, Constants.GetDataColor(Constants.ColorIndex.Elevation), fill);
                                break;

                            case ColumnDefinition.gradeID:
                                axis.Formatter = new Formatter.Percent();
                                axis.Label = CommonResources.Text.LabelGrade;
                                axis.LabelColor = Constants.GetDataColor(Constants.ColorIndex.Grade);
                                DrawSeriesGraph(ai.SmoothedGradeTrack, ai, axis, recs.Count, Constants.GetDataColor(Constants.ColorIndex.Grade), fill);
                                break;

                            case ColumnDefinition.hrID:
                                axis.Label = CommonResources.Text.LabelHeartRate;
                                axis.LabelColor = Constants.GetDataColor(Constants.ColorIndex.HeartRateBPM);
                                DrawSeriesGraph(ai.SmoothedHeartRateTrack, ai, axis, recs.Count, Constants.GetDataColor(Constants.ColorIndex.HeartRateBPM), fill);
                                break;

                            case ColumnDefinition.powerID:
                                axis.Label = CommonResources.Text.LabelPower;
                                axis.LabelColor = Constants.GetDataColor(Constants.ColorIndex.Power);
                                DrawSeriesGraph(ai.SmoothedPowerTrack, ai, axis, recs.Count, Constants.GetDataColor(Constants.ColorIndex.Power), fill);
                                break;

                            case ColumnDefinition.speedID:
                                axis.LabelColor = Constants.GetDataColor(Constants.ColorIndex.Speed);
                                axis.Label = speedPaceLabel;
                                axis.Formatter = fmatter;
                                DrawSpeedGraph(ai, axis, paceCount, speedCount, recs.Count, fill);
                                break;
                        }

                        fill = false;
                    }
                }
            }

            // If the chart has stripes on it, redraw them for the current speed units
            if (distanceChart.YAxis.Stripes.Count != 0)
            {
                DrawStripes();
            }

            // Auto zoom to the data
            distanceChart.AutozoomToData(true);
            distanceChart.Refresh();

            this.Cursor = Cursors.Default;
        }

        private void DrawSeriesGraph(INumericTimeDataSeries series, ActivityInfo ai, IAxis axis, int totalRecs, Color color, bool fill)
        {
            ChartDataSeries ds = new ChartDataSeries(distanceChart, axis);

            float distanceTotal = 0;
            float secondsLast = 0;

            // Draw the track
            for (int i = 0; i < Math.Min(series.Count, ai.SmoothedSpeedTrack.Count); i++)
            {
                // Get the value in m/s
                float value = series[i].Value;
                DateTime time = series.EntryDateTime(ai.SmoothedSpeedTrack[i]);
                ITimeValueEntry<float> speedPoint = ai.SmoothedSpeedTrack.GetInterpolatedValue(time);

                if (speedPoint != null)
                {
                    // Convert the xaxis to be distance
                    distanceTotal = (speedPoint.Value * (speedPoint.ElapsedSeconds - secondsLast)) + distanceTotal;
                    secondsLast = speedPoint.ElapsedSeconds;

                    float distance = (float)Length.Convert(distanceTotal, Length.Units.Meter, PluginMain.GetApplication().SystemPreferences.DistanceUnits);
                    PointF point = new PointF(distance, value);
                    if (!ds.Points.ContainsKey(distance)) // Handle errors in activity data track
                        ds.Points.Add(distance, point);
                }
            }

            // Check to see if the right vertical axis already exists or not
            bool found = false;
            foreach (RightVerticalAxis rva in distanceChart.YAxisRight)
            {
                if (rva.Label == axis.Label)
                {
                    found = true;
                    ds.ValueAxis = rva;
                    break;
                }
            }

            if (!found)
            {
                if (axis.GetType() == typeof(RightVerticalAxis))
                {
                    distanceChart.YAxisRight.Add(axis);
                    ds.ValueAxis = axis;
                }
                else
                {
                    ds.ValueAxis = axis;
                }
            }

            // Draw the chart
            if (fill)
            {
                ds.ChartType = ChartDataSeries.Type.Fill;
            }
            else
            {
                ds.ChartType = ChartDataSeries.Type.Line;
            }

            ds.LineColor = Color.FromArgb(255, color);
            ds.FillColor = Color.FromArgb(20 + (100 / totalRecs), color);

            distanceChart.DataSeries.Add(ds);
        }

        private void DrawSpeedGraph(ActivityInfo ai, IAxis axis, int paceCount, int speedCount, int totalRecs, bool fill)
        {
            ChartDataSeries ds = new ChartDataSeries(distanceChart, axis);

            float distanceTotal = 0;
            float secondsLast = 0;

            // Draw the Speed/Pace Track
            for (int i = 0; i < ai.SmoothedSpeedTrack.Count; i++)
            {
                if (paceCount > speedCount)
                {
                    // Get the value in m/s
                    float speed = ai.SmoothedSpeedTrack[i].Value;
                    speed = (float)Length.Convert(speed, Length.Units.Meter, PluginMain.GetApplication().SystemPreferences.DistanceUnits); // (ST Units)/sec

                    // Convert sec. to hours (mph or km/hr)
                    speed = speed * 60 * 60;

                    // Conver the speed to seconds
                    int seconds = 0;
                    if (speed != 0 && !float.IsNaN(speed))
                    {
                        seconds = (int)(60 * 60 / speed);
                    }

                    // Convert the xaxis to be distance
                    distanceTotal = (ai.SmoothedSpeedTrack[i].Value * (ai.SmoothedSpeedTrack[i].ElapsedSeconds - secondsLast)) + distanceTotal;
                    if (!ds.Points.ContainsKey((float)Length.Convert(distanceTotal, Length.Units.Meter, PluginMain.GetApplication().SystemPreferences.DistanceUnits)))
                    {
                        secondsLast = ai.SmoothedSpeedTrack[i].ElapsedSeconds;
                        PointF point = new PointF((float)Length.Convert(distanceTotal, Length.Units.Meter, PluginMain.GetApplication().SystemPreferences.DistanceUnits), seconds);
                        ds.Points.Add((float)Length.Convert(distanceTotal, Length.Units.Meter, PluginMain.GetApplication().SystemPreferences.DistanceUnits), point);
                    }
                }
                else
                {
                    // Get the value in m/s
                    float speed = ai.SmoothedSpeedTrack[i].Value;
                    speed = (float)Length.Convert(speed, Length.Units.Meter, PluginMain.GetApplication().SystemPreferences.DistanceUnits); // (ST Units)/sec

                    // Convert sec. to hours (mph or km/hr)
                    speed = speed * 60 * 60;

                    // Convert the xaxis to be distance
                    distanceTotal = (ai.SmoothedSpeedTrack[i].Value * (ai.SmoothedSpeedTrack[i].ElapsedSeconds - secondsLast)) + distanceTotal;
                    if (!ds.Points.ContainsKey(distanceTotal))
                    {
                        secondsLast = ai.SmoothedSpeedTrack[i].ElapsedSeconds;
                        PointF point = new PointF((float)Length.Convert(distanceTotal, Length.Units.Meter, PluginMain.GetApplication().SystemPreferences.DistanceUnits), speed);
                        ds.Points.Add((float)Length.Convert(distanceTotal, Length.Units.Meter, PluginMain.GetApplication().SystemPreferences.DistanceUnits), point);
                    }
                }
            }
            // Draw the chart
            if (fill)
            {
                ds.ChartType = ChartDataSeries.Type.Fill;
            }
            else
            {
                ds.ChartType = ChartDataSeries.Type.Line;
            }
            ds.LineColor = Constants.GetDataColor(Constants.ColorIndex.Speed);
            ds.FillColor = Color.FromArgb(20 + (100 / totalRecs), ds.LineColor);

            if (axis.GetType() == typeof(RightVerticalAxis) && !distanceChart.YAxisRight.Contains(axis))
            {
                distanceChart.YAxisRight.Add(axis);
            }

            distanceChart.DataSeries.Add(ds);
        }

        private void DrawStripes()
        {
            distanceChart.YAxis.Stripes.Clear();
            ArrayList recs = (ArrayList)treeDistance.SelectedItems;

            // Count the number of activities that use pace vs speed
            // We will display stripes based on the most popular
            int paceCount = 0;
            int speedCount = 0;

            List<string> zoneCats = new List<string>();
            List<int> zoneCount = new List<int>();

            // Find the most popular speed unit and category
            for (int j = 0; j < recs.Count; j++)
            {
                RecordNode r = recs[j] as RecordNode;
                if (r != null)
                {
                    if (r.Record.Activity.Category.SpeedUnits == Speed.Units.Pace)
                    {
                        paceCount++;
                    }
                    else if (r.Record.Activity.Category.SpeedUnits == Speed.Units.Speed)
                    {
                        speedCount++;
                    }

                    if (zoneCats.Contains(r.Record.Activity.Category.SpeedZone.ReferenceId))
                    {
                        zoneCount[zoneCats.IndexOf(r.Record.Activity.Category.SpeedZone.ReferenceId)]++;
                    }
                    else
                    {
                        zoneCats.Add(r.Record.Activity.Category.SpeedZone.ReferenceId);
                        zoneCount.Add(1);
                    }
                }
            }

            // Get the most popular zone reference id
            string zoneRef = string.Empty;
            int highCount = 0;
            for (int i = 0; i < zoneCount.Count; i++)
            {
                if (zoneCount[i] > highCount)
                {
                    zoneRef = zoneCats[i];
                    highCount = zoneCount[i];
                }
            }

            List<AxisStripe> stripes = new List<AxisStripe>();

            foreach (IZoneCategory zcat in PluginMain.GetApplication().Logbook.SpeedZones)
            {
                if (zcat.ReferenceId == zoneRef)
                {
                    foreach (INamedLowHighZone z in zcat.Zones)
                    {
                        double low = 0;
                        double high = double.PositiveInfinity;
                        int seconds = 0;
                        if (paceCount > speedCount)
                        {
                            // Convert the low speed to pace
                            if (z.Low != 0)
                            {
                                double speed = z.Low;
                                speed = Length.Convert(speed, Length.Units.Meter, PluginMain.GetApplication().SystemPreferences.DistanceUnits); // (ST Units)/sec
                                // Convert sec. to hours (mph or km/hr)
                                speed = speed * 60 * 60;
                                // Conver the speed to seconds
                                if (speed != 0 && !double.IsNaN(speed))
                                {
                                    seconds = (int)(60 * 60 / speed);
                                }
                                high = seconds;
                            }

                            // Convert the high speed to pace
                            double hspeed = z.High;
                            hspeed = Length.Convert(hspeed, Length.Units.Meter, PluginMain.GetApplication().SystemPreferences.DistanceUnits); // (ST Units)/sec
                            // Convert sec. to hours (mph or km/hr)
                            hspeed = hspeed * 60 * 60;
                            // Conver the speed to seconds
                            seconds = 0;
                            if (hspeed != 0 && !double.IsNaN(hspeed))
                            {
                                seconds = (int)(60 * 60 / hspeed);
                            }
                            low = seconds;
                        }
                        else
                        {
                            // Convert the low speed to the pref units
                            double speed = z.Low;
                            speed = Length.Convert(speed, Length.Units.Meter, PluginMain.GetApplication().SystemPreferences.DistanceUnits); // (ST Units)/sec
                            // Convert sec. to hours (mph or km/hr)
                            speed = speed * 60 * 60;
                            low = speed;

                            // Convert the high speed to the pref units
                            speed = z.High;
                            speed = Length.Convert(speed, Length.Units.Meter, PluginMain.GetApplication().SystemPreferences.DistanceUnits); // (ST Units)/sec
                            // Convert sec. to hours (mph or km/hr)
                            speed = speed * 60 * 60;
                            high = speed;
                        }
                        AxisStripe stripe = new AxisStripe(low, high, Color.Transparent);
                        stripe.Name = z.Name;
                        stripe.Gradient = false;
                        stripe.LineStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        stripe.LineWidth = 2;
                        stripe.LineColor = Color.Gray;
                        stripes.Add(stripe);
                    }
                }
            }

            foreach (AxisStripe stripe in stripes)
            {
                distanceChart.YAxis.Stripes.Add(stripe);
            }

            Refresh();
        }
    }
}
