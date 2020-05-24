using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ZoneFiveSoftware.Common.Data.Measurement;

namespace RecordBook.UI.View
{
    public static partial class Units
    {
        private static bool unitsLoaded;
        private static string distUnits;
        private static string paceUnits;
        private static string spdUnits;
        private static string elevUnits;
        private static string tempUnits;

        /// <summary>
        /// Distance Units to be displayed
        /// </summary>
        public static string Distance
        {
            get
            {
                if (!unitsLoaded)
                {
                    LoadUnits();
                }

                return distUnits;
            }
        }

        /// <summary>
        /// Pace Units to be displayed
        /// </summary>
        public static string Pace
        {
            get
            {
                if (!unitsLoaded)
                {
                    LoadUnits();
                }

                return paceUnits;
            }
        }

        /// <summary>
        /// Speed Units to be displayed
        /// </summary>
        public static string Speed
        {
            get
            {
                if (!unitsLoaded)
                {
                    LoadUnits();
                }

                return spdUnits;
            }
        }

        /// <summary>
        /// Elevation Units to be displayed
        /// </summary>
        public static string Elevation
        {
            get
            {
                if (!unitsLoaded)
                {
                    LoadUnits();
                }

                return elevUnits;
            }
        }

        /// <summary>
        /// Temperature Units to be displayed
        /// </summary>
        public static string Temp
        {
            get
            {
                if (!unitsLoaded)
                    LoadUnits();

                return tempUnits;
            }
        }

        public static string Power
        {
            get
            {
                return ZoneFiveSoftware.Common.Visuals.CommonResources.Text.LabelWatts;
            }
        }
        internal static void LoadUnits()
        {
            // Setup all the units text
            Length.Units units = PluginMain.GetApplication().SystemPreferences.DistanceUnits;
            distUnits = Length.LabelAbbr(units);
            paceUnits = ZoneFiveSoftware.Common.Data.Measurement.Speed.Label(ZoneFiveSoftware.Common.Data.Measurement.Speed.Units.Pace, new Length(1, units));
            spdUnits = ZoneFiveSoftware.Common.Data.Measurement.Speed.Label(ZoneFiveSoftware.Common.Data.Measurement.Speed.Units.Speed, new Length(1, units));

            elevUnits = Length.LabelAbbr(PluginMain.GetApplication().SystemPreferences.ElevationUnits);

            tempUnits = Temperature.LabelAbbr(PluginMain.GetApplication().SystemPreferences.TemperatureUnits);

            unitsLoaded = true;

        }
    }
}
