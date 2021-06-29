using GraphLib.Interfaces;
using GraphLib.Serialization.Interfaces;
using System.IO;

namespace GraphLib.Serialization.Extensions
{
    public static class GraphSerializerExtensions
    {
        public static void SaveToFile(this IGraphSerializer self, IGraph graph, string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                self.SaveGraph(graph, stream);
            }
        }

        public static IGraph LoadFromFile(this IGraphSerializer self, string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                var newGraph = self.LoadGraph(stream);
                return newGraph;
            }
        }
    }
}
