using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace GraphLibrary.Extensions.SystemTypeExtensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum enumValue)
        {
            var description = enumValue.ToString();
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            if (fieldInfo != null)
            {
                var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs?.Any() == true)
                    description = ((DescriptionAttribute)attrs.First()).Description;
            }

            return description;
        }

        public static IEnumerable<string> GetDescriptions(this Enum enumValue)
        {
            return Enum.GetValues(enumValue.GetType()).Cast<Enum>().
                Select(enumeration => enumeration.GetDescription());
        }
    }
}
