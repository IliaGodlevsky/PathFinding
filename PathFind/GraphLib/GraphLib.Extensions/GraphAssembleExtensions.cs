using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using System;
using System.Threading.Tasks;

namespace GraphLib.Extensions
{
    public static class GraphAssembleExtensions
    {
        /// <summary>
        /// Assembles graph asynchronously
        /// </summary>
        /// <param name="self"></param>
        /// <param name="percentOfObstacles"></param>
        /// <param name="dimensionSizes"></param>
        /// <returns>A task, that return an assembled graph</returns>
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