using GraphLib.Interfaces;
using GraphLib.Serialization.Interfaces;
using System.Threading.Tasks;

namespace GraphLib.Serialization.Extensions
{
    public static class GraphSerializationModuleExtensions
    {
        public static async Task<IGraph> LoadGraphAsync(this IGraphSerializationModule self)
        {
            return await Task.Run(() => self.LoadGraph()).ConfigureAwait(false);
        }

        public static async Task SaveGraphAsync(this IGraphSerializationModule self, IGraph graph)
        {
            await Task.Run(() => self.SaveGraph(graph)).ConfigureAwait(false);            
        }
    }
}
