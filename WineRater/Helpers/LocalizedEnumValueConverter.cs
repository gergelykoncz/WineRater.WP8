using System;
using System.Collections.Generic;
using System.Windows.Data;
using WineRater.Resources;

namespace WineRater.Helpers
{
    /// <summary>
    /// Converter for enums to localized strings based on the enum values.
    /// </summary>
    public class LocalizedEnumValueConverter : IValueConverter
    {
        /// <summary>
        /// Convert an enum member to a localized string.
        /// </summary>
        /// <param name="value">The enum value.</param>
        /// <param name="targetType">System.String.</param>
        /// <param name="parameter">Not used.</param>
        /// <param name="culture">Not used.</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                Type enumType = value.GetType();
                string enumName = enumType.Name;
                return getLocalizedString(enumName, value);
            }
            return null;
        }

        /// <summary>
        /// Convert a localized string back to an enum member.
        /// </summary>
        /// <param name="value">The localized string.</param>
        /// <param name="targetType">The type of the enum.</param>
        /// <param name="parameter">Not used.</param>
        /// <param name="culture">Not used.</param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && targetType != null)
            {
                string enumName = targetType.Name;
                
                //Build a map between localized values and all members of the target enum, use localized values as the key.
                var mappedValues = new Dictionary<string, object>();
                foreach (var enumMemberValue in Enum.GetValues(targetType))
                {
                    string localizedValue = getLocalizedString(enumName, enumMemberValue);
                    mappedValues.Add(localizedValue, enumMemberValue);
                }

                //Check if the localized value exists in the map.
                if (mappedValues.ContainsKey(value.ToString()))
                {
                    //Return the enum member for the localized key.
                    return mappedValues[value.ToString()];
                }
            }
            return null;
        }

        /// <summary>
        /// Helper method to get the localized value.
        /// </summary>
        /// <param name="enumName">The name of the enum, without namespaces.</param>
        /// <param name="enumValue">The value of the enum to localize.</param>
        /// <returns></returns>
        private string getLocalizedString(string enumName, object enumValue)
        {
            string resourceKey = string.Format("{0}{1}", enumName, enumValue);
            return LocalizedStrings.GetResourceForKey(resourceKey);
        }
    }
}
