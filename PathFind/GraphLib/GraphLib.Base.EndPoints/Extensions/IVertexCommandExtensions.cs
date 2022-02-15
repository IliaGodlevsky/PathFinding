using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using System.Collections.Generic;

namespace GraphLib.Base.EndPoints.Extensions
{
    internal static class IVertexCommandExtensions
    {
        public static void ExecuteForEach(this IVertexCommand self, IEnumerable<IVertex> range)
        {
            range.ForEach(self.Execute);
        }
    }
}
