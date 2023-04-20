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
        public static async Task SerializeToFileAsync<TGraph, TVertex>(this IGraphSerializer<TGraph, TVertex> self,
            TGraph graph, string filePath)
            where TGraph : IGraph<TVertex>
            where TVertex : IVertex
        {
            await Task.Run(() => self.SerializeToFile(graph, filePath)).ConfigureAwait(false);
        }

        public static void SerializeToFile<TGraph, TVertex>(this IGraphSerializer<TGraph, TVertex> self,
            TGraph graph, string filePath)
            where TGraph : IGraph<TVertex>
            where TVertex : IVertex
        {
            var fileMode = File.Exists(filePath) ? FileMode.Truncate : FileMode.Create;
            using (var fileStream = new FileStream(filePath, fileMode, FileAccess.Write))
            {
                self.SerializeTo(graph, fileStream);
            }
        }

        public static TGraph DeserializeFromFile<TGraph, TVertex>(this IGraphSerializer<TGraph, TVertex> self, string filePath)
            where TGraph : IGraph<TVertex>
            where TVertex : IVertex
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return self.DeserializeFrom(fileStream);
            }
        }

        public static void SerializeToNetwork<TGraph, TVertex>(this IGraphSerializer<TGraph, TVertex> self,
           TGraph graph, string host, int port)
           where TGraph : IGraph<TVertex>
           where TVertex : IVertex
        {
            using (var client = new TcpClient(host, port))
            {
                using (var networkStream = client.GetStream())
                {
                    self.SerializeTo(graph, networkStream);
                }
            }
        }

        public static async Task SerializeToNetworkAsync<TGraph, TVertex>(this IGraphSerializer<TGraph, TVertex> self,
            TGraph graph, string host, int port)
            where TGraph : IGraph<TVertex>
            where TVertex : IVertex
        {
            await Task.Run(() => self.SerializeToNetwork(graph, host, port));
        }

        public static async Task SerializeToNetworkAsync<TGraph, TVertex>(this IGraphSerializer<TGraph, TVertex> self,
            TGraph graph, (string Host, int Port) address)
            where TGraph : IGraph<TVertex>
            where TVertex : IVertex
        {
            await self.SerializeToNetworkAsync(graph, address.Host, address.Port);
        }

        public static TGraph DeserializeFromNetwork<TGraph, TVertex>(this IGraphSerializer<TGraph, TVertex> self, int port)
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
                        return self.DeserializeFrom(networkStream);
                    }
                }
            }
        }
    }
}
