using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using System.Threading.Tasks;

namespace Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions
{
    public static class IGraphSerializationModuleExtensions
    {
        public static async Task<TGraph> LoadGraphAsync<TGraph, TVertex>(this IGraphSerializationModule<TGraph, TVertex> self)
            where TGraph : IGraph<TVertex>
            where TVertex : IVertex
        {
            return await Task.Run(() => self.LoadGraph()).ConfigureAwait(false);
        }

        public static async Task SaveGraphAsync<TGraph, TVertex>(this IGraphSerializationModule<TGraph, TVertex> self, IGraph<IVertex> graph)
            where TGraph : IGraph<TVertex>
            where TVertex : IVertex
        {
            await Task.Run(() => self.SaveGraph(graph)).ConfigureAwait(false);
        }
    }
}
