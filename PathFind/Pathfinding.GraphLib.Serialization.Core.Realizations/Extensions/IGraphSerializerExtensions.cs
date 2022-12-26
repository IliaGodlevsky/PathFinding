using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using System.IO;
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

        public static void SendGraphToPipe<TGraph, TVertex>(this IGraphSerializer<TGraph, TVertex> self, IGraph<IVertex> graph, string pipeName)
            where TGraph : IGraph<TVertex>
            where TVertex : IVertex
        {
            using (var clientStream = new NamedPipeClientStream(pipeName))
            {
                clientStream.Connect();
                self.SaveGraph(graph, clientStream);
            }
        }

        public static TGraph RecieveGraphFromPipe<TGraph, TVertex>(this IGraphSerializer<TGraph, TVertex> self, string pipeName)
            where TGraph : IGraph<TVertex>
            where TVertex : IVertex
        {
            using (var serverStream = new NamedPipeServerStream(pipeName))
            {
                serverStream.WaitForConnection();
                return self.LoadGraph(serverStream);
            }
        }
    }
}
