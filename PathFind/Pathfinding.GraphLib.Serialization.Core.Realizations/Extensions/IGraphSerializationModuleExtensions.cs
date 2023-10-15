using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using System.Threading.Tasks;

namespace Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions
{
    public static class IGraphSerializationModuleExtensions
    {
        public static async Task<IGraph<TVertex>> LoadGraphAsync<TVertex>(this IGraphSerializationModule<TVertex> self)
            where TVertex : IVertex
        {
            return await Task.Run(() => self.LoadGraph()).ConfigureAwait(false);
        }

        public static async Task SaveGraphAsync<TVertex>(this IGraphSerializationModule<TVertex> self, IGraph<TVertex> graph)
            where TVertex : IVertex
        {
            await Task.Run(() => self.SaveGraph(graph)).ConfigureAwait(false);
        }
    }
}
