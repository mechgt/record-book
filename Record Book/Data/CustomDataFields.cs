using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Data.Fitness.CustomData;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Visuals;
using System.Diagnostics;
using RecordBook.Settings;

namespace RecordBook.Data
{
    class CustomDataFields
    {
        private static bool warningMsgBadField;
        private static bool fieldsCheckPerformed = false;
        private static bool trimpExists;
        private static bool tssExists;

        public enum TLCustomFields
        {
            Trimp, TSS, FTPcycle, FTPrun, NormPwr, VDOT, DanielsPoints
        }

        public static bool TRIMPexists
        {
            get
            {
                if (!fieldsCheckPerformed)
                    CheckFields();

                return trimpExists;
            }
        }

        public static bool TSSexists
        {
            get
            {
                if (!fieldsCheckPerformed)
                    CheckFields();

                return tssExists;
            }
        }

        /// <summary>
        /// Get a Training Load related custom parameter
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static ICustomDataFieldDefinition GetCustomProperty(TLCustomFields field, bool autoCreate)
        {
            // All data types so far are numbers
            ICustomDataFieldDefinition fieldDef = null;
            ICustomDataFieldDataType dataType = CustomDataFieldDefinitions.StandardDataType(CustomDataFieldDefinitions.StandardDataTypes.NumberDataTypeId);
            ICustomDataFieldObjectType objType;
            Guid id;
            string name = "Custom";

            switch (field)
            {
                case TLCustomFields.Trimp:
                    objType = CustomDataFieldDefinitions.StandardObjectType(typeof(IActivity));
                    id = GUIDs.customTrimp;
                    fieldDef = GetCustomProperty(dataType, objType, id, name, autoCreate);
                    break;

                case TLCustomFields.TSS:
                    objType = CustomDataFieldDefinitions.StandardObjectType(typeof(IActivity));
                    id = GUIDs.customTSS;
                    fieldDef = GetCustomProperty(dataType, objType, id, name, autoCreate);
                    break;

                case TLCustomFields.NormPwr:
                    objType = CustomDataFieldDefinitions.StandardObjectType(typeof(IActivity));
                    id = GUIDs.customNP;
                    fieldDef = GetCustomProperty(dataType, objType, id, name, autoCreate);
                    break;

                //case TLCustomFields.DanielsPoints:
                //    objType = CustomDataFieldDefinitions.StandardObjectType(typeof(IActivity));
                //    id = GUIDs.customDanielsPoints;
                //    fieldDef = GetCustomProperty(dataType, objType, id, name, autoCreate);
                //    break;
            }

            return fieldDef;
        }

        /// <summary>
        /// Private helper to dig the logbook for a custom parameter
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private static ICustomDataFieldDefinition GetCustomProperty(ICustomDataFieldDataType dataType, ICustomDataFieldObjectType objType, Guid id, string name, bool autoCreate)
        {
            // Dig all of the existing custom params looking for a match.
            foreach (ICustomDataFieldDefinition customDef in PluginMain.GetLogbook().CustomDataFieldDefinitions)
            {
                if (customDef.Id == id)
                {
                    // Is this really necessary...?
                    if (customDef.DataType != dataType)
                    {
                        // Invalid match found!!! Bad news.
                        // This might occur if a user re-purposes a field.
                        if (!warningMsgBadField)
                        {
                            warningMsgBadField = true;
                            MessageDialog.Show("Invalid " + name + " Custom Field.  Was this field data type modified? (" + customDef.Name + ")", Resources.Strings.Label_RecordBook);
                        }

                        return null;
                    }
                    else
                    {
                        // Return custom def
                        return customDef;
                    }
                }
            }

            // No match found, create it
            if (autoCreate)
            {
                return PluginMain.GetLogbook().CustomDataFieldDefinitions.Add(id, objType, dataType, name);
            }
            else
            {
                return null;
            }
        }

        private static void CheckFields()
        {
            tssExists = GetCustomProperty(TLCustomFields.TSS, false) != null;
            trimpExists = GetCustomProperty(TLCustomFields.Trimp, false) != null;
            fieldsCheckPerformed = true;
        }
    }
}
