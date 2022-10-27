using Algorithm.Interfaces;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Algorithm.Extensions
{
    public static class AlgorithmExtensions
    {
        public static async ValueTask<TResult> FindPathAsync<TResult>(this IAlgorithm<TResult> self)
            where TResult : IEnumerable<IVertex>
        {
            return await Task.Run(self.FindPath).ConfigureAwait(false);
        }
    }
}
