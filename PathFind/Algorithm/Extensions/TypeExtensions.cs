using Algorithm.Attributes;
using Common.Extensions;
using System;

namespace Algorithm.Extensions
{
    public static class TypeExtensions
    {
        internal static bool IsFilterable(this Type self)
        {
            return self.GetAttribute<FilterableAttribute>() != null;
        }
    }
}
