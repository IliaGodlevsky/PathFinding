using Pathfinding.Infrastructure.Business.Serializers.Exceptions;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Business.Serializers
{
    public sealed class BinarySerializer<T> : ISerializer<IEnumerable<T>>
        where T : IBinarySerializable, new()
    {
        public async Task<IEnumerable<T>> DeserializeFromAsync(Stream stream, CancellationToken token = default)
        {
            try
            {
                using var reader = new BinaryReader(stream, Encoding.Default, leaveOpen: true);
                return await Task.Run(() => reader.ReadSerializableArray<T>(), token).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new SerializationException(ex.Message, ex);
            }
        }

        public async Task SerializeToAsync(IEnumerable<T> item, Stream stream, CancellationToken token = default)
        {
            try
            {
                using var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true);
                await Task.Run(() => writer.Write(item.OfType<IBinarySerializable>().ToList()), token).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new SerializationException(ex.Message, ex);
            }
        }
    }
}
