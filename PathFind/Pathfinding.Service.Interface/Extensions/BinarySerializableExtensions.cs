using Pathfinding.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.IO;

namespace Pathfinding.Service.Interface.Extensions
{
    public static class BinarySerializableExtensions
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

        public static T ReadSerializable<T>(this BinaryReader reader) 
            where T : IBinarySerializable, new()
        {
            T item = new();
            item.Deserialize(reader); 
            return item;
        }

        public static IReadOnlyCollection<T> ReadSerializableArray<T>(this BinaryReader reader)
            where T : IBinarySerializable, new()
        {
            int count = reader.ReadInt32();
            var list = new List<T>(count);
            while (count-- > 0)
            {
                list.Add(reader.ReadSerializable<T>());
            }
            return list.AsReadOnly();
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

        public static string ReadNullableString(this BinaryReader reader)
        {
            bool isNull = reader.ReadBoolean();
            return isNull ? string.Empty : reader.ReadString();
        }

        public static void WriteNullableInt(this BinaryWriter writer, int? value)
        {
            writer.Write(value.HasValue);
            if (value.HasValue)
            {
                writer.Write(value.Value);
            }
        }

        public static int? ReadNullableInt(this BinaryReader reader)
        {
            bool hasValue = reader.ReadBoolean();
            return hasValue ? reader.ReadInt32() : null;
        }

        public static void WriteNullableTimeSpan(this BinaryWriter writer, TimeSpan? time)
        {
            writer.Write(time.HasValue);
            if (time.HasValue)
            {
                writer.Write(time.Value.TotalMilliseconds);
            }
        }

        public static TimeSpan? ReadNullableTimeSpan(this BinaryReader reader)
        {
            bool hasValue = reader.ReadBoolean();
            return hasValue ? TimeSpan.FromMilliseconds(reader.ReadDouble()) : null;
        }

        public static void WriteNullableDouble(this BinaryWriter writer, double? value)
        {
            writer.Write(value.HasValue);
            if (value.HasValue)
            {
                writer.Write(value.Value);
            }
        }

        public static double? ReadNullableDouble(this BinaryReader reader)
        {
            bool hasValue = reader.ReadBoolean();
            return hasValue ? reader.ReadDouble() : null;
        }

        public static void Write(this BinaryWriter writer, IReadOnlyCollection<int> array)
        {
            writer.Write(array.Count);
            array.ForEach(x => writer.Write(x));
        }

        public static IReadOnlyList<int> ReadArray(this BinaryReader reader)
        {
            int count = reader.ReadInt32();
            var array = new List<int>(count);
            for (int i = 0; i < count; i++)
            {
                int value = reader.ReadInt32();
                array.Add(value);
            }
            return array.AsReadOnly();
        }
    }
}
