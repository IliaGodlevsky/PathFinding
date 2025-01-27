using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Extensions;

namespace Pathfinding.Infrastructure.Business.Benchmarks.Data
{
    public sealed class Serializable : IBinarySerializable
    {
        public int Size { get; set; }

        public string Name { get; set; }

        public double Cost { get; set; }

        public float Strength { get; set; }

        public List<int> Values { get; set; } = new List<int>();

        public List<Serializable> Serializables { get; set; } = new List<Serializable>();

        public void Deserialize(BinaryReader reader)
        {
            Size = reader.ReadInt32();
            Cost = reader.ReadDouble();
            Name = reader.ReadString();
            Strength = reader.ReadSingle();
            Values = reader.ReadArray().ToList();
            Serializables = reader.ReadSerializableArray<Serializable>().ToList();
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Size);
            writer.Write(Cost);
            writer.Write(Name);
            writer.Write(Strength);
            writer.Write(Values);
            writer.Write(Serializables);
        }
    }
}
