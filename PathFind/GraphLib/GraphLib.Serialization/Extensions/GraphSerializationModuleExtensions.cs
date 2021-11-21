using GraphLib.Interfaces;
using System;
using System.Threading.Tasks;

namespace GraphLib.Serialization.Extensions
{
    public static class GraphSerializationModuleExtensions
    {
        public static async Task<IGraph> LoadGraphAsync(this GraphSerializationModule self)
        {
            string filePath = self.input.InputLoadPath();
            return await Task.Run(() =>
            {
                try
                {
                    return self.serializer.LoadGraphFromFile(filePath);
                }
                catch (Exception)
                {
                    throw;
                }
            }).ConfigureAwait(false);
        }

        public static async Task SaveGraphAsync(this GraphSerializationModule self, IGraph graph)
        {
            string filePath = self.input.InputSavePath();
            await Task.Run(() =>
            {
                try
                {
                    self.serializer.SaveGraphToFile(graph, filePath);
                }
                catch (Exception)
                {
                    throw;
                }
            }).ConfigureAwait(false);
        }
    }
}
