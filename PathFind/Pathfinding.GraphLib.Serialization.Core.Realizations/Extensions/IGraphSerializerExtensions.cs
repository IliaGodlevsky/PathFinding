using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using System.IO;
using System.IO.Pipes;
using System.Threading.Tasks;

namespace Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions
{
    public static class IGraphSerializerExtensions
    {
        public static async ValueTask SaveGraphToFileAsync<TGraph, TVertex>(this IGraphSerializer<TGraph, TVertex> self,
            IGraph<IVertex> graph, string filePath)
            where TGraph : IGraph<TVertex>
            where TVertex : IVertex
        {
            await Task.Run(() => self.SaveGraphToFile(graph, filePath));
        }

        public static void SaveGraphToFile<TGraph, TVertex>(this IGraphSerializer<TGraph, TVertex> self,
            IGraph<IVertex> graph, string filePath)
            where TGraph : IGraph<TVertex>
            where TVertex : IVertex
        {
            var fileMode = File.Exists(filePath) ? FileMode.Truncate : FileMode.Create;
            using (var fileStream = new FileStream(filePath, fileMode, FileAccess.Write))
            {
                self.SaveGraph(graph, fileStream);
            }
        }

        public static TGraph LoadGraphFromFile<TGraph, TVertex>(this IGraphSerializer<TGraph, TVertex> self, string filePath)
            where TGraph : IGraph<TVertex>
            where TVertex : IVertex
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return self.LoadGraph(fileStream);
            }
        }

        public static void SaveGraphToPipe<TGraph, TVertex>(this IGraphSerializer<TGraph, TVertex> self, IGraph<IVertex> graph, string pipeName, string serverName = ".")
            where TGraph : IGraph<TVertex>
            where TVertex : IVertex
        {
            using (var stream = new NamedPipeClientStream(serverName, pipeName))
            {
                stream.Connect();
                self.SaveGraph(graph, stream);
            }
        }

        public static TGraph LoadGraphFromPipe<TGraph, TVertex>(this IGraphSerializer<TGraph, TVertex> self, string pipeName)
            where TGraph : IGraph<TVertex>
            where TVertex : IVertex
        {
            using (var stream = new NamedPipeServerStream(pipeName))
            {
                stream.WaitForConnection();
                return self.LoadGraph(stream);
            }
        }

        public static async ValueTask SaveGraphToPipeAsync<TGraph, TVertex>(this IGraphSerializer<TGraph, TVertex> self,
            IGraph<IVertex> graph, string pipeName, string serverName = ".")
            where TGraph : IGraph<TVertex>
            where TVertex : IVertex
        {
            await Task.Run(() => self.SaveGraphToPipe(graph, pipeName, serverName));
        }
    }
}
