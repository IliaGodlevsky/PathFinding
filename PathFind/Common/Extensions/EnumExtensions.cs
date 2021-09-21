using System;

namespace Common.Extensions
{
    public static class EnumExtensions
    {
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
