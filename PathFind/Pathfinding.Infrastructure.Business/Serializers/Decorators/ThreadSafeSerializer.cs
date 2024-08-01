using Pathfinding.Infrastructure.Business.Serializers.Exceptions;
using Pathfinding.Service.Interface;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Business.Serializers.Decorators
{
    public sealed class ThreadSafeSerializer<T> : ISerializer<T>
    {
        private readonly object syncRoot = new();
        private readonly ISerializer<T> serializer;

        public ThreadSafeSerializer(ISerializer<T> serializer)
        {
            this.serializer = serializer;
        }

        public Task<T> DeserializeFromAsync(Stream stream, CancellationToken token = default)
        {
            lock (syncRoot)
            {
                try
                {
                    return serializer.DeserializeFromAsync(stream, token);
                }
                catch (Exception ex)
                {
                    throw new SerializationException(ex.Message, ex);
                }
            }
        }

        public Task SerializeToAsync(T graph, Stream stream, CancellationToken token = default)
        {
            lock (syncRoot)
            {
                try
                {
                    return serializer.SerializeToAsync(graph, stream, token);
                }
                catch (Exception ex)
                {
                    throw new SerializationException(ex.Message, ex);
                }
            }
        }
    }
}
