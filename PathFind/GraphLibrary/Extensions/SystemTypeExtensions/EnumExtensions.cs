using System;
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

        public static int GetValue(this Enum enumValue)
        {
            var names = Enum.GetNames(enumValue.GetType()).Cast<string>().ToList();
            var enumValueIndex = names.IndexOf(enumValue.ToString());
            return Convert.ToInt32(Enum.GetValues(enumValue.GetType()).GetValue(enumValueIndex));
        }
    }
}
