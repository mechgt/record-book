namespace RecordBook
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using ZoneFiveSoftware.Common.Data;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Data.GPS;
    using ZoneFiveSoftware.Common.Visuals;
    using ZoneFiveSoftware.Common.Visuals.Fitness;
    using ZoneFiveSoftware.Common.Visuals.Util;
    
    internal static class Utilities
    {
        /// <summary>
        /// Formats a timespan into hh:mm:ss format.
        /// </summary>
        /// <param name="span">Timespan</param>
        /// <returns>hh:mm:ss formatted string (omits hours if less than 1 hour).  Returns empty string if timespan = 0.</returns>
        internal static string ToTimeString(TimeSpan span)
        {
            if (span == TimeSpan.Zero)
            {
                // Return empty if zero.
                return string.Empty;
            }

            string displayTime;

            if ((span.Days * 24) + span.Hours > 0)
            {
                // Hours & minutes
                displayTime = ((span.Days * 24) + span.Hours).ToString("#0") + ":" +
                              span.Minutes.ToString("00") + ":";
            }
            else if (span.Minutes < 10)
            {
                // Single digit minutes
                displayTime = span.Minutes.ToString("#0") + ":";
            }
            else
            {
                // Double digit minutes
                displayTime = span.Minutes.ToString("00") + ":";
            }

            displayTime = displayTime +
                          span.Seconds.ToString("00");

            return displayTime;
        }
        
        /// <summary>
        /// Converts speed to pace (in length/minute)
        /// </summary>
        /// <param name="speed">Speed in some distance units (length units are maintained)</param>
        /// <returns>Returns a number of minutes per length unit (miles for instance)</returns>
        public static double SpeedToPace(double speed)
        {
            ushort MinutesPerHour = 60;
            return MinutesPerHour / speed;
        }

        /// <summary>
        /// Converts a pace unit (number of minutes) to a speed
        /// </summary>
        /// <param name="pace"></param>
        /// <returns></returns>
        public static double PaceToSpeed(double paceMinutes)
        {
            ushort MinutesPerHour = 60;
            return MinutesPerHour / paceMinutes;
        }
    }
}
