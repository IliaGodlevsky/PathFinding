using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Pathfinding.Infrastructure.Business.Extensions
{
    public static class XmlSerializerExtensions
    {
        public static async Task<T> DeserializeAsync<T>(this XmlSerializer serializer,
            TextReader reader, CancellationToken token = default)
        {
            var result = await Task.Run(() => serializer.Deserialize(reader), token).ConfigureAwait(false);
            return (T)result;
        }

        public static async Task SerializeAsync<T>(this XmlSerializer serializer, T item,
            TextWriter writer, CancellationToken token = default)
        {
            await Task.Run(() => serializer.Serialize(writer, item), token).ConfigureAwait(false);
        }
    }
}
