using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using System.Collections.Generic;

namespace GraphLib.Extensions
{
    public static class IExecutableExtensions
    {
        public static void ExecuteForEach<T>(this IExecutable<T> self, IEnumerable<T> range)
        {
            range.ForEach(self.Execute);
        }
    }
}
