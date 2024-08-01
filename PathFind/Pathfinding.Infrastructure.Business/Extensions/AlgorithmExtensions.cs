using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface.Algorithms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Business.Extensions
{
    public static class AlgorithmExtensions
    {
        public static async ValueTask<TResult> FindPathAsync<TResult>(this IAlgorithm<TResult> self)
            where TResult : IEnumerable<ICoordinate>
        {
            return await Task.Run(self.FindPath).ConfigureAwait(false);
        }
    }
}
