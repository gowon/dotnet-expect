namespace Dotnet.Expect.Core.Abstractions
{
    using System.Collections;
    using System.Collections.Generic;

    // https://stackoverflow.com/a/1306544
    public class ItemsCache : Dictionary<string, object>
    {
        public ItemsCache()
        {
        }

        /// <summary>
        ///     Build the parameter dictionary from the public properties of the supplied object
        ///     where each key is a property name and each value is the corresponding property's
        ///     value.
        /// </summary>
        /// <param name="parameters">An object containing the properties making up the initial key/value dictionary state.</param>
        public ItemsCache(object parameters)
            : this()
        {
            if (parameters != null)
            {
                if (parameters is IDictionary)
                {
                    Merge(parameters as IDictionary, true);
                }
                else
                {
                    foreach (var info in parameters.GetType().GetProperties())
                    {
                        var value = info.GetValue(parameters, null);
                        Add(info.Name, value);
                    }
                }
            }
        }

        /// <summary>
        ///     Merge a dictionary of keys/values with the current dictionary.
        /// </summary>
        /// <param name="dict">The dictionary whose parameters will be added.  Must have string keys.</param>
        /// <param name="replace">True if conflicting parameters should replace the existing ones, false otherwise.</param>
        /// <returns>The updated Parameter dictionary.</returns>
        public ItemsCache Merge(IDictionary dict, bool replace)
        {
            foreach (string key in dict.Keys)
            {
                if (ContainsKey(key))
                {
                    if (replace)
                    {
                        this[key] = dict[key];
                    }
                }
                else
                {
                    Add(key, dict[key]);
                }
            }

            return this;
        }

        /// <summary>
        ///     Merge a dictionary of keys/values with the current dictionary.  Replaces conflicting keys.
        /// </summary>
        /// <param name="dict">The dictionary whose parameters will be added.  Must have string keys.</param>
        /// <returns>The updated Parameter dictionary.</returns>
        public ItemsCache Merge(object dict)
        {
            return Merge(dict, true);
        }

        /// <summary>
        ///     Merge a dictionary of keys/values with the current dictionary.
        /// </summary>
        /// <param name="dict">An object whose properties are used as the new keys/values for the update.</param>
        /// <param name="replace">True if conflicting parameters should replace the existing ones, false otherwise.</param>
        /// <returns>The updated Parameter dictionary.</returns>
        public ItemsCache Merge(object dict, bool replace)
        {
            var newDict = new ItemsCache(dict);
            return Merge(newDict, replace);
        }
    }
}