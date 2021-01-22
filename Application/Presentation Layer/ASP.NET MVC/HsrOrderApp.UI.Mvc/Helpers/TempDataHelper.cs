using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HsrOrderApp.UI.Mvc.Helpers
{
    public static class TempDataHelper
    {
        /// <summary>
        /// Adds or replaces a value in a TempDataDictionary.
        /// </summary>
        /// <param name="dict">The TempDataDictionary</param>
        /// <param name="key">The key to add or replace</param>
        /// <param name="value">The value for the corresponding key</param>
        /// <returns>Returns false, if the value of an existing key has changed, otherwise true.</returns>
        public static bool AddOrReplace(this TempDataDictionary dict, string key, object value)
        {
            if (dict == null) throw new ArgumentNullException(nameof(dict));
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));

            if (dict.ContainsKey(key))
            {
                dict[key] = value;
                return false;
            }
            dict.Add(key, value);
            return true;
        }
        /// <summary>
        /// Gets the value for a specific key in a TempDataDictionary.
        /// </summary>
        /// <param name="dict">The TempDataDictionary</param>
        /// <param name="key">The key to add or replace</param>
        /// <param name="value">The value for the corresponding key</param>
        /// <returns>Returns true if a value was found, otherwise false.</returns>
        public static bool TryGetValue<T>(this TempDataDictionary dict, string key, out T value)
        {
            if (dict == null) throw new ArgumentNullException(nameof(dict));

            if(dict.ContainsKey(key))
            {
                object o = dict[key];
                if(o is T)
                {
                    value = (T)o;
                    return true;
                }
            }

            value = default(T);
            return false;
        }
    }
}