using System.Collections.Generic;
using System.Linq;

namespace Shared.Primitives.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IOrderedEnumerable<T> OrderByOrderAttribute<T>(this IEnumerable<T> collection)
        {
            return collection.OrderBy(item => item.GetOrder());
        }

        public static IOrderedEnumerable<T> OrderByOrderAttributeDesc<T>(this IEnumerable<T> collection)
        {
            return collection.OrderByDescending(item => item.GetOrder());
        }
    }
}
