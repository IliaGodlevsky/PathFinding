using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphLib.Extensions
{
    public static class GraphAssembleExtensions
    {
        public static IGraph AssembleGraph(this IGraphAssemble self, int obstaclePercent = 0, params int[] dimensionSizes)
        {
            return self.AssembleGraph(obstaclePercent, (IReadOnlyList<int>)dimensionSizes);
        }

        public static async Task<IGraph> AssembleGraphAsync(this IGraphAssemble self,
            int percentOfObstacles, IReadOnlyList<int> dimensionSizes)
        {
            return await Task.Run(() => self.AssembleGraph(percentOfObstacles, dimensionSizes));
        }

        public static async Task<IGraph> AssembleGraphAsync(this IGraphAssemble self,
            int percentOfObstacles, params int[] dimensionSizes)
        {
            return await self.AssembleGraphAsync(percentOfObstacles, (IReadOnlyList<int>)dimensionSizes);
        }
    }
}