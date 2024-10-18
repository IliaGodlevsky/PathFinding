using Pathfinding.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.IO;

namespace Pathfinding.Service.Interface.Extensions
{
    public static class BinaryWriterExtensions
    {
        public static void Write(this BinaryWriter writer, IReadOnlyCollection<IBinarySerializable> collection)
        {
            writer.Write(collection.Count);
            collection.ForEach(writer.Write);
        }

        public static void Write(this BinaryWriter writer, IBinarySerializable serializable)
        {
            serializable.Serialize(writer);
        }

        public static void WriteNullableString(this BinaryWriter writer, string value)
        {
            bool isNull = string.IsNullOrEmpty(value);
            writer.Write(isNull);
            if (!isNull)
            {
                writer.Write(value);
            }
        }

        public static void Write(this BinaryWriter writer, IReadOnlyCollection<int> array)
        {
            writer.Write(array.Count);
            array.ForEach(writer.Write);
        }
    }
}
