using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphLib.Extensions
{
    public static class GraphAssembleExtensions
    {
        public static TGraph AssembleGraph<TGraph, TVertex>(this IGraphAssemble<TGraph, TVertex> self,
            int obstaclePercent = 0, params int[] dimensionSizes)
            where TGraph : IGraph<TVertex>
            where TVertex : IVertex
        {
            return self.AssembleGraph(obstaclePercent, (IReadOnlyList<int>)dimensionSizes);
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
            return await self.AssembleGraphAsync(percentOfObstacles, (IReadOnlyList<int>)dimensionSizes);
        }
    }
}