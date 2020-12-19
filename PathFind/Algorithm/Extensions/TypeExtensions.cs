using Algorithm.Attributes;
using System;

namespace Algorithm.Extensions
{
    public static class TypeExtensions
    {
        internal static bool IsFilterable(this Type self)
        {
            var attribute = (FilterableAttribute)Attribute
                .GetCustomAttribute(self, typeof(FilterableAttribute));
            return attribute != null;
        }
    }
}
