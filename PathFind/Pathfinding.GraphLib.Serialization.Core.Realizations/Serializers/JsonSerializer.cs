using Newtonsoft.Json;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Exceptions;
using System;
using System.IO;
using System.Text;

namespace Pathfinding.GraphLib.Serialization.Core.Realizations.Serializers
{
    public sealed class JsonSerializer<T> : ISerializer<T>
    {
        public T DeserializeFrom(Stream stream)
        {
            try
            {
                using (var reader = new StreamReader(stream, Encoding.Default, false, 1024, leaveOpen: true))
                {
                    string deserialized = reader.ReadToEnd();
                    var dtos = JsonConvert.DeserializeObject<T>(deserialized);
                    return dtos;
                }
            }
            catch (Exception ex)
            {
                throw new SerializationException(ex.Message, ex);
            }
        }

        public void SerializeTo(T item, Stream stream)
        {
            try
            {
                using (var writer = new StreamWriter(stream, Encoding.Default, 1024, leaveOpen: true))
                {
                    string serialized = JsonConvert.SerializeObject(item);
                    writer.Write(serialized);
                }
            }
            catch (Exception ex)
            {
                throw new SerializationException(ex.Message, ex);
            }
        }
    }
}
