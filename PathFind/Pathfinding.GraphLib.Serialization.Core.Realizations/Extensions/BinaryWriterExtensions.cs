using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Shared.Extensions;
using Shared.Primitives.ValueRange;
using System.Collections.Generic;
using System.IO;

namespace Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions
{
    public static class BinaryWriterExtensions
    {
        public static void WriteGraph(this BinaryWriter writer,
            GraphSerializationInfo info)
        {
            writer.WriteIntArray(info.DimensionsSizes);
            writer.WriteVertices(info.VerticesInfo);
        }

        public static void WriteIntArray(this BinaryWriter writer,
            IReadOnlyCollection<int> array)
        {
            writer.Write(array.Count);
            array.ForEach(writer.Write);
        }

        public static void WriterCoordinates(this BinaryWriter writer,
            IReadOnlyList<ICoordinate> neighbourhood)
        {
            writer.Write(neighbourhood.Count);
            neighbourhood.ForEach(writer.WriteIntArray);
        }

        private static void WriteRange(this BinaryWriter writer,
            InclusiveValueRange<int> range)
        {
            writer.Write(range.UpperValueOfRange);
            writer.Write(range.LowerValueOfRange);
        }

        private static void WriteVertices(this BinaryWriter writer,
            VertexSerializationInfo[] vertices)
        {
            writer.Write(vertices.Length);
            foreach (var vertex in vertices)
            {
                writer.Write(vertex.IsObstacle);
                writer.Write(vertex.Cost.CurrentCost);
                writer.WriteRange(vertex.Cost.CostRange);
                writer.WriterCoordinates(vertex.Neighbourhood);
                writer.WriteIntArray(vertex.Position);
            }
        }
    }
}
