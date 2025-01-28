using Pathfinding.Shared.Extensions;

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

        public static void WriteNullableInt32(this BinaryWriter writer, int? value)
        {
            bool isNull = !value.HasValue;
            writer.Write(isNull);
            if (!isNull)
            {
                writer.Write(value.Value);
            }
        }

        public static void Write(this BinaryWriter writer, double? value)
        {
            bool hasValue = value.HasValue;
            writer.Write(hasValue);
            if (hasValue)
            {
                writer.Write(value.Value);
            }
        }

        public static void Write(this BinaryWriter writer, IReadOnlyCollection<int> array)
        {
            writer.Write(array.Count);
            array.ForEach(writer.Write);
        }
    }
}
