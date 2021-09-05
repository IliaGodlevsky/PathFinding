using Algorithm.Interfaces;
using GraphLib.Interfaces;
using System;
using System.Threading.Tasks;

namespace Algorithm.Extensions
{
    public static class AlgorithmExtensions
    {
        public static async Task<IGraphPath> FindPathAsync(this IAlgorithm self)
        {
            var task = Task.Run(self.FindPath);
            try
            {
                return await task.ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<IGraphPath> FindPathAndHighlightAsync(this IAlgorithm self,
            IIntermediateEndPoints endPoints)
        {
            var path = await self.FindPathAsync();
            return await path.HighlightAsync(endPoints);
        }
    }
}
