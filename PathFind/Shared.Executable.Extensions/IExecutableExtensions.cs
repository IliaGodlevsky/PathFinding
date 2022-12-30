using Shared.Extensions;
using System.Collections.Generic;

namespace Shared.Executable.Extensions
{
    public static class IExecutableExtensions
    {
        public static void Execute<T>(this IExecutable<T> self, IEnumerable<T> range)
        {
            range.ForEach(self.Execute);
        }
    }
}
