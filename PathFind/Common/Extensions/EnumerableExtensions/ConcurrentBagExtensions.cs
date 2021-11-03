using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Common.Extensions.EnumerableExtensions
{
    public static class ConcurrentBagExtensions
    {
        public static void AddRange<T>(this ConcurrentBag<T> self, IEnumerable<T> range)
        {
            foreach (var item in range)
            {
                self.Add(item);
            }
        }
    }
}
