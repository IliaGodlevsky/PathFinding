using Pathfinding.Service.Interface.Algorithms;
using Pathfinding.Shared.Primitives;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Service.Interface.Extensions
{
    public static class AlgorithmExtensions
    {
        public static async ValueTask<T> FindPathAsync<T>(this IAlgorithm<T> algorithm,
            CancellationToken token = default)
            where T : IEnumerable<Coordinate>
        {
            return await Task.Run(() => algorithm.FindPath(), token);
        }
    }
}
