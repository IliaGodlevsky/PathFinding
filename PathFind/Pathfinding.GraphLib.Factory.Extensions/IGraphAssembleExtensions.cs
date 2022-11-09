using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Factory.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pathfinding.GraphLib.Factory.Extensions
{
    public static class IGraphAssembleExtensions
    {
        public static TGraph AssembleGraph<TGraph, TVertex>(this IGraphAssemble<TGraph, TVertex> self,
            int obstaclePercent = 0, params int[] dimensionSizes)
            where TGraph : IGraph<TVertex>
            where TVertex : IVertex
        {
            return self.AssembleGraph(obstaclePercent, dimensionSizes);
        }

        public static TGraph AssembleSquareGraph<TGraph, TVertex>(this IGraphAssemble<TGraph, TVertex> self, int dimension)
            where TGraph : IGraph<TVertex>
            where TVertex : IVertex
        {
            return self.AssembleGraph(0, dimension, dimension);
        }

        public static async Task<TGraph> AssembleGraphAsync<TGraph, TVertex>(this IGraphAssemble<TGraph, TVertex> self,
            int percentOfObstacles, IReadOnlyList<int> dimensionSizes)
            where TGraph : IGraph<TVertex>
            where TVertex : IVertex
        {
            return await Task.Run(() => self.AssembleGraph(percentOfObstacles, dimensionSizes));
        }

        public static async Task<TGraph> AssembleGraphAsync<TGraph, TVertex>(this IGraphAssemble<TGraph, TVertex> self,
            int percentOfObstacles, params int[] dimensionSizes)
            where TGraph : IGraph<TVertex>
            where TVertex : IVertex
        {
            return await self.AssembleGraphAsync(percentOfObstacles, dimensionSizes);
        }
    }
}