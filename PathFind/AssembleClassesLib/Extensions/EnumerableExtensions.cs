using System.Collections.Generic;
using System.Linq;

namespace AssembleClassesLib.Extensions
{
    public static class EnumerableExtensions
    {
        public static IDictionary<string, T> AsNameInstanceDictionary<T>(this IEnumerable<T> collection)
        {
            return collection.ToDictionary(item => item.GetClassName());
        }
    }
}
