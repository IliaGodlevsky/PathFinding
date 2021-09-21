using Common.Extensions;
using EnumerationValues.Attributes;
using System;

namespace EnumerationValues.Extensions
{
    public static class EnumExtensions
    {
        public static bool IsIgnored(this Enum value)
        {
            return value.GetAttributeOrNull<EnumFetchIgnoreAttribute>() != null;
        }
    }
}
