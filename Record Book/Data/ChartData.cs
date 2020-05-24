using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Data.Fitness;
using RecordBook.UI.View;
using System.ComponentModel;
using RecordBook.Settings;
using System.Windows.Forms;

namespace RecordBook.Data
{
    static class ChartData
    {
        private static List<RecordSet> distanceMaster = new List<RecordSet>();
        private static List<RecordSet> extremesMaster = new List<RecordSet>();
        private static List<RecordSet> rankMaster = new List<RecordSet>();

        private static List<RecordSet> distanceDisplayed = new List<RecordSet>();
        private static List<RecordSet> extremesDisplayed = new List<RecordSet>();
        private static List<RecordSet> rankDisplayed = new List<RecordSet>();

        private static List<IActivity> updateQueue = new List<IActivity>();

        private static bool isDirty = true;
        private static bool isSuperDirty = true;

        internal static event ProgressChangedEventHandler ProgressTick;

        internal static List<RecordSet> ExtremeRecords
        {
            get { return extremesDisplayed; }
        }

        internal static List<RecordSet> DistanceRecords
        {
            get { return distanceDisplayed; }
        }

        internal static List<RecordSet> RankedRecords
        {
            get { return rankDisplayed; }
        }

        internal static IActivityCategory FilterCategory
        {
            get { return PluginMain.GetApplication().DisplayOptions.SelectedCategoryFilter; }
            set
            {
                if (PluginMain.GetApplication().DisplayOptions.SelectedCategoryFilter == value)
                    return;

                PluginMain.GetApplication().DisplayOptions.SelectedCategoryFilter = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating if the data needs to be recalculated/refreshed.
        /// Activities added,
        /// NOTE:  It can be set to TRUE, but cannot directly be set to false.
        /// </summary>
        internal static bool IsDirty
        {
            get { return isDirty; }
            set
            {
                isDirty = value || isDirty;
                if (isDirty)
                    RecordSet.ResetTotals();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating if the data needs to be reinitialized.
        /// NOTE:  It can be set to TRUE, but cannot directly be set to false.
        /// Category settings changed, system settings changed.
        /// </summary>
        internal static bool IsSuperDirty
        {
            get { return isSuperDirty; }
            set
            {
                isSuperDirty = value || isSuperDirty;
                IsDirty = IsSuperDirty;
            }
        }

        internal static void Initialize()
        {
            Calculate();

            // Copy display lists over to Master lists
            distanceMaster = new List<RecordSet>(distanceDisplayed);
            extremesMaster = new List<RecordSet>(extremesDisplayed);
            rankMaster = new List<RecordSet>(rankDisplayed);
            isSuperDirty = false;
        }

        internal static void ApplyFilter()
        {
            distanceDisplayed = GetFilteredSet(distanceMaster, FilterCategory);
            extremesDisplayed = GetFilteredSet(extremesMaster, FilterCategory); // TODO: This step is SLOW (Filtering Extremes list)
            rankDisplayed = GetFilteredSet(rankMaster, FilterCategory);
            isDirty = false;
        }

        private static List<RecordSet> GetFilteredSet(List<RecordSet> master, IActivityCategory category)
        {
            List<RecordSet> display = new List<RecordSet>();
            IActivityCategory recordCat;
            foreach (RecordSet set in master)
            {
                RecordSet displaySet = new RecordSet(set.Category);
                foreach (Record record in set)
                {
                    recordCat = record.Activity.Category;
                    do
                    {
                        if (recordCat == category)
                        {
                            displaySet.Add(record);
                            break;
                        }
                        else
                            recordCat = recordCat.Parent;

                    } while (recordCat != null);
                }

                // TODO: (MED) Get rid of repeatedly sorting/ranking records
                if (displaySet.Category.Type != RecordCategory.RecordType.AllActivities)
                    displaySet.RankRecords();

                display.Add(displaySet);
            }

            return display;
        }

        /// <summary>
        /// Determines if Record should be filtered from display.
        /// </summary>
        /// <param name="record"></param>
        /// <param name="recCount"></param>
        /// <returns>True if record should be hidden/filtered, or False if it is OK for display.</returns>
        internal static bool IsFiltered(Record record, int recordCount)
        {
            // If record rank is within boundaries AND record start time fits the date criteria
            return !((record.Rank <= recordCount || recordCount < 1) && (UserData.StartDateFilter <= record.StartTime && record.StartTime <= UserData.EndDateFilter));
        }

        /// <summary>
        /// Calculate records for ALL activities in the logbook (inc. My Friends Activities).
        /// </summary>
        /// <remarks>Calculated values are cached, and subsets can be filtered from this without re-calculation.
        /// This is only called by the Initialization method.</remarks>
        private static void Calculate()
        {
            // Get the record categories
            List<RecordCategory> distCategories = UserData.DistanceRecordCategories;
            List<RecordCategory> extCategories = UserData.ExtremeRecordCategories;
            RecordSet set;

            // Setup the progress bar
            float totalSteps = distCategories.Count + extCategories.Count;
            float currentStep = 0;

            // Calculate the distance records & update tree
            DistanceRecords.Clear();
            foreach (RecordCategory category in distCategories)
            {
                currentStep++;
                RaiseProgressTick((int)(currentStep / totalSteps * 100), string.Format(Resources.Strings.Label_CalculatingRecords, category.Name));
                Application.DoEvents();
                set = new RecordSet(category);
                set.AddRecords();
                ChartData.DistanceRecords.Add(set);
            }

            // Calculate the extreme records & update tree
            ChartData.ExtremeRecords.Clear();
            foreach (RecordCategory category in extCategories)
            {
                // Skip TRIMP or TSS if it doesn't exist
                if ((category.Type == RecordCategory.RecordType.TRIMP && !CustomDataFields.TRIMPexists) ||
                    (category.Type == RecordCategory.RecordType.TSS && !CustomDataFields.TSSexists))
                    continue;

                currentStep++;
                RaiseProgressTick((int)(currentStep / totalSteps * 100), string.Format(Resources.Strings.Label_CalculatingRecords, category.Name));
                Application.DoEvents();
                set = new RecordSet(category);
                set.AddRecords();
                ChartData.ExtremeRecords.Add(set);
            }

            // Populate Rank data
            RecordCategory rc = new RecordCategory();
            rc.Name = Resources.Strings.Label_AllActivities;
            rc.Type = RecordCategory.RecordType.AllActivities;
            set = new RecordSet(rc);
            set.AddRecords();
            ChartData.RankedRecords.Add(set); // Note that this is only 1 parent node
        }

        /// <summary>
        /// Adds an activity to the complete set of records.
        /// </summary>
        /// <param name="activity">Activity to be added</param>
        internal static void AddActivity(IActivity activity)
        {
            // Extremes...
            Record record;
            foreach (RecordSet set in extremesMaster)
            {
                record = RecordSet.GetExtremeOverallRecord(activity, set.Category);
                if (record != null)
                    set.Add(record);
            }

            // Distance
            foreach (RecordSet set in distanceMaster)
            {
                record = RecordSet.GetDistancePaceRecord(activity, set.Category);
                if (record != null)
                    set.Add(record);
            }

            // Rank
            foreach (RecordSet set in rankMaster)
            {
                record = RecordSet.GetAllActivities(activity, set.Category);
                if (record != null)
                    set.Add(record);
            }

            ChartData.IsDirty = true;
        }

        /// <summary>
        /// Adds an activity to the complete set of records.
        /// </summary>
        /// <param name="activity">Activity to be removed</param>
        internal static void RemoveActivity(IActivity activity)
        {
            // Extremes...
            foreach (RecordSet set in extremesMaster)
            {
                int i = 0;
                while (i < set.Count)
                {
                    if (set[i].Activity == activity)
                        set.Remove(set[i]);
                    else
                        i++;
                }
            }

            // Distance
            foreach (RecordSet set in distanceMaster)
            {
                int i = 0;
                while (i < set.Count)
                {
                    if (set[i].ActivityId == activity.ReferenceId)
                        set.Remove(set[i]);
                    else
                        i++;
                }
            }

            // Rank
            foreach (RecordSet set in rankMaster)
            {
                int i = 0;
                while (i < set.Count)
                {
                    if (set[i].Activity == activity)
                        set.Remove(set[i]);
                    else
                        i++;
                }

            }

            ChartData.IsDirty = true;
        }

        /// <summary>
        /// Queue an activity to be updated on the next refresh.
        /// </summary>
        /// <param name="activity">Activity to be be queued</param>
        internal static void QueueActivity(IActivity activity)
        {
            if (!updateQueue.Contains(activity))
                updateQueue.Add(activity);
        }

        internal static void ProcessUpdateQueue()
        {
            foreach (IActivity activity in updateQueue)
            {
                RemoveActivity(activity);
                AddActivity(activity);
            }

            updateQueue.Clear();
        }

        internal static List<Record> GetRecords(IActivity activity, RecordCategory.RecordGroup group)
        {
            // TODO: (LOW) Fetch records associated with single activity (not yet implemented)
            List<Record> matches = new List<Record>();
            List<RecordSet> collection;

            switch (group)
            {
                case RecordCategory.RecordGroup.Distance:
                    collection = distanceMaster;
                    break;
                case RecordCategory.RecordGroup.Extreme:
                    collection = extremesMaster;
                    break;
                case RecordCategory.RecordGroup.NowThen:
                    collection = rankMaster;
                    break;
                default:
                    return null;
            }

            // Search all record sets for activity
            foreach (RecordSet set in collection)
            {
                matches.AddRange(GetRecords(activity, set));
            }

            return matches;
        }

        internal static List<Record> GetRecords(IActivity activity, RecordSet set)
        {
            List<Record> matches = new List<Record>();

            foreach (Record record in set)
            {
                if (record.ActivityId == activity.ReferenceId)
                    matches.Add(record);
            }

            return matches;
        }

        private static void RaiseProgressTick(int percentage, string state)
        {
            if (ProgressTick != null)
                ProgressTick.Invoke(null, new ProgressChangedEventArgs(percentage, state));
        }
    }
}
