using System;
using System.ComponentModel;

namespace Common.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var attribute = value.GetAttributeOrNull<DescriptionAttribute>();
            return attribute?.Description ?? value.ToString();
        }

        public static TAttribute GetAttributeOrNull<TAttribute>(this Enum value)
            where TAttribute : Attribute
        {
            return value
                .GetType()
                .GetField(value.ToString())
                ?.GetAttributeOrNull<TAttribute>();
        }
    }
}
