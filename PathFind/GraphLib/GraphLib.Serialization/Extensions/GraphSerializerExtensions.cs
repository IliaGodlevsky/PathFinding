using GraphLib.Interfaces;
using GraphLib.Serialization.Interfaces;
using System.IO;

namespace GraphLib.Serialization.Extensions
{
    public static class GraphSerializerExtensions
    {
        public static void SaveGraphToFile(this IGraphSerializer self, IGraph graph, string filePath)
        {
            var fileMode = File.Exists(filePath) ? FileMode.Truncate : FileMode.Create;
            using (var fileStream = new FileStream(filePath, fileMode, FileAccess.Write))
            {
                self.SaveGraph(graph, fileStream);
            }
        }

        public static IGraph LoadGraphFromFile(this IGraphSerializer self, string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return self.LoadGraph(fileStream);
            }
        }
    }
}
