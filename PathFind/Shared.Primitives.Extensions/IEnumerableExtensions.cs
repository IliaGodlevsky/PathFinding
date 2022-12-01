using Shared.Extensions;
using Shared.Primitives.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace Shared.Primitives.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IOrderedEnumerable<T> OrderByOrderAttribute<T, TAttribute>(this IEnumerable<T> collection)
            where TAttribute : OrderAttribute
        {
            return collection.OrderBy(item => item.GetAttributeOrDefault<TAttribute>().Order);
        }

        public static IOrderedEnumerable<T> OrderByOrderAttribute<T>(this IEnumerable<T> collection)
        {
            return collection.OrderBy(item => item.GetAttributeOrDefault<OrderAttribute>().Order);
        }
    }
}
