using Commands.Interfaces;
using Common.Extensions.EnumerableExtensions;
using System.Collections.Generic;

namespace Commands.Extensions
{
    public static class IExecutableExtensions
    {
        public static void Execute<T>(this IExecutable<T> self, IEnumerable<T> range)
        {
            range.ForEach(self.Execute);
        }
    }
}
