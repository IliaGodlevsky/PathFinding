using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using System;
using System.Threading.Tasks;

namespace GraphLib.Extensions
{
    public static class GraphAssembleExtensions
    {
        public static async Task<IGraph> AssembleGraphAsync(this IGraphAssemble self,
            int percentOfObstacles, params int[] dimensionSizes)
        {
            var task = Task.Run(() => self.AssembleGraph(percentOfObstacles, dimensionSizes));
            try
            {
                return await task.ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}