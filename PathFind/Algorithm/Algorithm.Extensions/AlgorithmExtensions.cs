using Algorithm.Interfaces;
using System.Threading.Tasks;

namespace Algorithm.Extensions
{
    public static class AlgorithmExtensions
    {
        public static async Task<IGraphPath> FindPathAsync(this IAlgorithm self)
        {
            return await Task.Run(self.FindPath).ConfigureAwait(false);
        }
    }
}
