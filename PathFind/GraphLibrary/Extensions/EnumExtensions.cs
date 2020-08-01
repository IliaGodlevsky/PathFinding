using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace GraphLibrary.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription<T>(this T enumValue)
                where T : struct, IConvertible

        {
            if (!typeof(T).IsEnum)
                return null;

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
        public static List<string> GetDescriptions<T>(this Enum en)
            where T : struct, IConvertible
        {
            List<string> descriptions = new List<string>();
            var enumList = Enum.GetValues(typeof(T));

            foreach (var item in enumList)
                descriptions.Add(((T)item).GetDescription());
            
            return descriptions;
        }

    }
}
