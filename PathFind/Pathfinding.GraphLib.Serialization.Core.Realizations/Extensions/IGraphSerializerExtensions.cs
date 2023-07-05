using Pathfinding.GraphLib.Serialization.Core.Interface;
using Shared.Primitives;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions
{
    public static class IGraphSerializerExtensions
    {
        public static async Task SerializeToFileAsync<T>(this ISerializer<T> self,
            T value, string filePath)
        {
            await Task.Run(() => self.SerializeToFile(value, filePath)).ConfigureAwait(false);
        }

        public static void SerializeToFile<T>(this ISerializer<T> self,
            T value, string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                self.SerializeTo(value, fileStream);
            }
        }

        public static T DeserializeFromFile<T>(this ISerializer<T> self, string filePath)
        {
            using (var fileStream = File.OpenRead(filePath))
            {
                return self.DeserializeFrom(fileStream);
            }
        }

        public static void SerializeToNetwork<T>(this ISerializer<T> self, T value, string host, int port)
        {
            using (var client = new TcpClient(host, port))
            {
                using (var networkStream = client.GetStream())
                {
                    self.SerializeTo(value, networkStream);
                }
            }
        }

        public static async Task SerializeToNetworkAsync<T>(this ISerializer<T> self,
            T graph, string host, int port)
        {
            await Task.Run(() => self.SerializeToNetwork(graph, host, port)).ConfigureAwait(false);
        }

        public static async Task SerializeToNetworkAsync<T>(this ISerializer<T> self,
            T value, (string Host, int Port) address)
        {
            await self.SerializeToNetworkAsync(value, address.Host, address.Port);
        }

        public static T DeserializeFromNetwork<T>(this ISerializer<T> self, int port)
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
