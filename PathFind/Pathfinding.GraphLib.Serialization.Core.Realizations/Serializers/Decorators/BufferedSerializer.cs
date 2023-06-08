using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Exceptions;
using System;
using System.IO;

namespace Pathfinding.GraphLib.Serialization.Core.Realizations.Serializers.Decorators
{
    public sealed class BufferedSerializer<T> : ISerializer<T>
    {
        private readonly ISerializer<T> serializer;

        public BufferedSerializer(ISerializer<T> serializer)
        {
            this.serializer = serializer;
        }

        public T DeserializeFrom(Stream stream)
        {
            try
            {
                using (var buffer = new BufferedStream(stream))
                {
                    return serializer.DeserializeFrom(buffer);
                }
            }
            catch (Exception ex)
            {
                throw new SerializationException(ex.Message, ex);
            }
        }

        public void SerializeTo(T graph, Stream stream)
        {
            try
            {
                using (var buffer = new BufferedStream(stream))
                {
                    serializer.SerializeTo(graph, buffer);
                }
            }
            catch (Exception ex)
            {
                throw new SerializationException(ex.Message, ex);
            }
        }
    }
}
