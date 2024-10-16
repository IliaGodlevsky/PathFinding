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
            collection.ForEach(x => writer.Write(x));
        }

        public static void Write(this BinaryWriter writer, IBinarySerializable serializable)
        {
            serializable.Serialize(writer);
        }

        public static void WriteNullableInt(this BinaryWriter writer, int? value)
        {
            writer.Write(value.HasValue);
            if (value.HasValue)
            {
                writer.Write(value.Value);
            }
        }

        public static void WriteNullableDouble(this BinaryWriter writer, double? value)
        {
            writer.Write(value.HasValue);
            if (value.HasValue)
            {
                writer.Write(value.Value);
            }
        }

        public static void WriteNullableTimeSpan(this BinaryWriter writer, TimeSpan? time)
        {
            writer.Write(time.HasValue);
            if (time.HasValue)
            {
                writer.Write(time.Value.TotalMilliseconds);
            }
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
    }
}
