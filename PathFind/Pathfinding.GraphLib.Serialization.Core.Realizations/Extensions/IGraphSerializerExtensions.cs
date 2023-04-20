using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Shared.Primitives;
using System.IO;
using System.IO.Pipes;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions
{
    public static class IGraphSerializerExtensions
    {
        public static async Task SaveGraphToFileAsync<TGraph, TVertex>(this IGraphSerializer<TGraph, TVertex> self,
            IGraph<IVertex> graph, string filePath)
            where TGraph : IGraph<TVertex>
            where TVertex : IVertex
        {
            await Task.Run(() => self.SaveGraphToFile(graph, filePath)).ConfigureAwait(false);
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

        public static void SendGraph<TGraph, TVertex>(this IGraphSerializer<TGraph, TVertex> self,
           IGraph<IVertex> graph, string host, int port)
           where TGraph : IGraph<TVertex>
           where TVertex : IVertex
        {
            using (var client = new TcpClient(host, port))
            {
                using (var networkStream = client.GetStream())
                {
                    self.SaveGraph(graph, networkStream);
                }
            }
        }

        public static async Task SendGraphAsync<TGraph, TVertex>(this IGraphSerializer<TGraph, TVertex> self,
            IGraph<IVertex> graph, string host, int port)
            where TGraph : IGraph<TVertex>
            where TVertex : IVertex
        {
            await Task.Run(() => self.SendGraph(graph, host, port));
        }

        public static async Task SendGraphAsync<TGraph, TVertex>(this IGraphSerializer<TGraph, TVertex> self,
            IGraph<IVertex> graph, (string Host, int Port) address)
            where TGraph : IGraph<TVertex>
            where TVertex : IVertex
        {
            await self.SendGraphAsync(graph, address.Host, address.Port);
        }

        public static TGraph RecieveGraph<TGraph, TVertex>(this IGraphSerializer<TGraph, TVertex> self, int port)
            where TGraph : IGraph<TVertex>
            where TVertex : IVertex
        {
            var listener = new TcpListener(IPAddress.Any, port);
            using (Disposable.Use(listener.Stop))
            {
                listener.Start();
                using (var client = listener.AcceptTcpClient())
                {
                    using (var networkStream = client.GetStream())
                    {
                        return self.LoadGraph(networkStream);
                    }
                }
            }
        }
    }
}
