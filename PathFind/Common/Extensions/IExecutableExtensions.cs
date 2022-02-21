using Common.Extensions.EnumerableExtensions;
using Common.Interface;
using System.Collections.Generic;

namespace Common.Extensions
{
    public static class IExecutableExtensions
    {
        public static void ExecuteForEach<T>(this IExecutable<T> self, IEnumerable<T> range)
        {
            range.ForEach(self.Execute);
        }
    }
}
