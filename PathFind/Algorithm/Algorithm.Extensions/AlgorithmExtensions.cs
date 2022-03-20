using Algorithm.Interfaces;
using System;
using System.Threading.Tasks;

namespace Algorithm.Extensions
{
    public static class AlgorithmExtensions
    {
        public static async Task<IGraphPath> FindPathAsync(this IAlgorithm self)
        {
            try
            {
                return await Task.Run(self.FindPath).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
