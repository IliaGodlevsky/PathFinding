using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using System.Threading.Tasks;

namespace GraphLib.Extensions
{
    public static class GraphAssembleExtensions
    {
        public static async Task<IGraph> AssembleGraphAsync(this IGraphAssemble self,
            int percentOfObstacles = 0, params int[] dimensionSizes)
        {
            return await Task.Run(() => self.AssembleGraph(percentOfObstacles, dimensionSizes))
                .ConfigureAwait(false);
        }
    }
}