using Algorithm.Interfaces;
using System;
using System.Threading.Tasks;

namespace Algorithm.Extensions
{
    public static class AlgorithmExtensions
    {
        /// <summary>
        /// Finds the cheapest path in the graph asynchronously
        /// </summary>
        /// <param name="self"></param>
        /// <returns>An asynchronos operation that returns graph path</returns>
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
    }
}
