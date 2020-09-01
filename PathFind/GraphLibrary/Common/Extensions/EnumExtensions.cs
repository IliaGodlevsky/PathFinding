using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace GraphLibrary.Extensions
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

        public static List<string> GetDescriptions(this Enum en)
        {
            List<string> descriptions = new List<string>();
            var enumList = Enum.GetValues(en.GetType());

            foreach (var item in enumList)
                descriptions.Add(((Enum)item).GetDescription());

            return descriptions;
        }
    }
}
