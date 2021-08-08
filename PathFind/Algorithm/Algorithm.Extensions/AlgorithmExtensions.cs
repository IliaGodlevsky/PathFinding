using Algorithm.Interfaces;
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
            catch (ArgumentException)
            {
                throw;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
