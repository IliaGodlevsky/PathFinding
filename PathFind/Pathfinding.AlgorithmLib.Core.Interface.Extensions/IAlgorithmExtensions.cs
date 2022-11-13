using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pathfinding.AlgorithmLib.Core.Interface.Extensions
{
    public static class IAlgorithmExtensions
    {
        public static async ValueTask<TResult> FindPathAsync<TResult>(this IAlgorithm<TResult> self)
            where TResult : IEnumerable<ICoordinate>
        {
            return await Task.Run(self.FindPath).ConfigureAwait(false);
        }
    }
}
