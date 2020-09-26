using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GraphLibrary.Extensions.CustomTypeExtensions
{
    public static class GraphLibraryExtensions
    {
        public static T DeepCopy<T>(this T self)
        {
            if (!typeof(T).IsSerializable)
                throw new ArgumentException("Type must be serialiable");
            if (self == null)
                return default;
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, self);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}
