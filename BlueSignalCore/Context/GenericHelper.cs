using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BlueSignalCore.Context
{
    #region GenericHelper

    /// <summary>
    /// Generic Helper class to define the common re-usable helper methods
    /// </summary>
    public static class GenericHelper
    {
        #region String Extention Methods

        /// <summary>
        /// Replaces a substring within a string with another substring with optional case sensitivity turned off.
        /// </summary>
        /// <param name="originalString">String to do replacements on</param>
        /// <param name="oldValue">The string to find</param>
        /// <param name="newValue">The string to replace found string wiht</param>
        /// <param name="caseInsensitive">If true case insensitive search is performed</param>
        /// <returns>updated string or original string if no matches</returns>
        public static string ReplaceEx(this string originalString, string oldValue, string newValue, bool caseInsensitive)
        {
            var at1 = 0;
            while (true)
            {
                at1 = caseInsensitive ? originalString.IndexOf(oldValue, at1, originalString.Length - at1, StringComparison.OrdinalIgnoreCase) : originalString.IndexOf(oldValue, at1);
                if (at1 == -1) return originalString;
                originalString = originalString.Substring(0, at1) + newValue + originalString.Substring(at1 + oldValue.Length);
                at1 += newValue.Length;
            }
        }

        /// <summary>
        /// Get the Typed List which is required for the MVC Grid.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<Dictionary<string, Object>> GetTypedList(IEnumerable list)
        {
            var rows = new List<Dictionary<string, Object>>();
            if (list == null) return rows;
            foreach (var rec in list)
            {
                var row = new Dictionary<string, Object>();
                var oProps = rec.GetType().GetProperties();
                foreach (var pi in oProps)
                {
                    var colType = pi.PropertyType;
                    if (colType == typeof(Int32) || colType == typeof(Int32?) || colType == typeof(String))
                    {
                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }
                        row.Add(pi.Name, pi.GetValue(rec, null) ?? DBNull.Value);
                    }
                    else
                    {
                        continue;
                    }
                }
                rows.Add(row);
            }
            return rows;
        }

        public static List<Dictionary<string, Object>> GetTypedList(DataTable dataTable)
        {
            return (from DataRow dr in dataTable.Rows select dataTable.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => dr[col])).ToList();
        }

        #endregion String Extention Methods
    }


    #endregion GenericHelper
}


