using System;
using System.Collections.Generic;
using System.Text;
using RecordBook.Data;

namespace RecordBook.Settings
{
    static class Migrate
    {
        static internal void MigrateSettings()
        {
            // 1.3.0: Added TRIMP & TSS categories
            // Renamed many of the categories (Display_AvgXXXX, etc.)
            if (UserData.Version < new Version(1, 2))
            {
                MigrateTo130();
            }
        }

        /// <summary>
        /// 1.3.0: Added TRIMP & TSS categories
        /// Renamed many of the categories (Display_AvgXXXX, etc.)
        /// </summary>
        static internal void MigrateTo130()
        {
            // Initialize column settings because column names have changed significantly
            UserData.UserColumns_AllActivites = string.Empty;
            UserData.UserColumns_Distance = string.Empty;
            UserData.UserColumns_Extreme = string.Empty;

            bool tssFound = false, trimpFound = false;
            int tssIndex = 0, trimpIndex = 0;

            // TSS/TRIMP Extreme cats
            foreach (RecordCategory category in UserData.ExtremeRecordCategories)
            {
                if (category.Type == RecordCategory.RecordType.TRIMP)
                    trimpFound = true;
                else if (category.Type == RecordCategory.RecordType.TSS)
                    tssFound = true;
                else if (category.Type == RecordCategory.RecordType.MaxHR)
                    trimpIndex = UserData.ExtremeRecordCategories.IndexOf(category) + 1;
                else if (category.Type == RecordCategory.RecordType.MaxPower)
                    tssIndex = UserData.ExtremeRecordCategories.IndexOf(category) + 1;
            }

            if (CustomDataFields.TRIMPexists && !trimpFound)
                UserData.ExtremeRecordCategories.Insert(trimpIndex, new RecordCategory(Resources.Strings.Label_MaxTRIMP, RecordCategory.RecordType.TRIMP, UserData.GetAllRefIDs()));

            if (CustomDataFields.TSSexists && !tssFound)
                UserData.ExtremeRecordCategories.Insert(tssIndex, new RecordCategory(Resources.Strings.Label_MaxTSS, RecordCategory.RecordType.TSS, UserData.GetAllRefIDs()));

            // TSS/TRIMP Now/Then cats
            trimpFound = false; tssFound = false; trimpIndex = 0; tssIndex = 0;
            foreach (RecordCategory category in UserData.NowThenRecordCategories)
            {
                if (category.Type == RecordCategory.RecordType.TRIMP)
                    trimpFound = true;
                else if (category.Type == RecordCategory.RecordType.TSS)
                    tssFound = true;
                else if (category.Type == RecordCategory.RecordType.MaxHR)
                    trimpIndex = UserData.NowThenRecordCategories.IndexOf(category) + 1;
                else if (category.Type == RecordCategory.RecordType.MaxPower)
                    tssIndex = UserData.NowThenRecordCategories.IndexOf(category) + 1;
            }

            if (CustomDataFields.TRIMPexists && !trimpFound)
                UserData.NowThenRecordCategories.Insert(trimpIndex, new RecordCategory(Resources.Strings.Label_MaxTRIMP, RecordCategory.RecordType.TRIMP, UserData.GetAllRefIDs()));

            if (CustomDataFields.TSSexists && !tssFound)
                UserData.NowThenRecordCategories.Insert(tssIndex, new RecordCategory(Resources.Strings.Label_MaxTSS, RecordCategory.RecordType.TSS, UserData.GetAllRefIDs()));
        }
    }
}
