using GraphLib.Interfaces;
using GraphLib.Serialization.Interfaces;
using System.IO;

namespace GraphLib.Serialization.Extensions
{
    public static class GraphSerializerExtensions
    {
        public static void SaveGraphToFile(this IGraphSerializer self, IGraph graph, string fileName)
        {
            using (var stream = new FileStream(fileName, DefineFileMode(fileName), FileAccess.Write))
            {
                self.SaveGraph(graph, stream);
            }
        }

        public static IGraph LoadGraphFromFile(this IGraphSerializer self, string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                return self.LoadGraph(stream);
            }
        }

        private static FileMode DefineFileMode(string filePath)
        {
            return File.Exists(filePath) ? FileMode.Truncate : FileMode.Create;
        }
    }
}
