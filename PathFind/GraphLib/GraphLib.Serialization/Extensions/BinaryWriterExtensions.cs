using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.IO;
using ValueRange;

namespace GraphLib.Serialization.Extensions
{
    internal static class BinaryWriterExtensions
    {
        public static void WriteGraph(this BinaryWriter writer, IGraph graph)
        {
            var info = graph.ToGraphSerializationInfo();
            writer.WriteIntArray(info.DimensionsSizes);
            writer.WriteVertices(info.VerticesInfo);
            writer.WriteRange(info.CostRange);
        }

        private static void WriteIntArray(this BinaryWriter writer, int[] array)
        {
            writer.Write(array.Length);
            array.ForEach(writer.Write);
        }

        private static void WriteRange(this BinaryWriter writer, InclusiveValueRange<int> range)
        {
            writer.Write(range.UpperValueOfRange);
            writer.Write(range.LowerValueOfRange);
        }

        private static void WriterNeighborhood(this BinaryWriter writer, IReadOnlyCollection<ICoordinate> neighbourhood)
        {
            writer.Write(neighbourhood.Count);
            neighbourhood.ForEach(writer.WriteCoordinate);
        }

        private static void WriteCoordinate(this BinaryWriter writer, ICoordinate coordinate)
        {
            writer.WriteIntArray(coordinate.CoordinatesValues);
        }

        private static void WriteVertices(this BinaryWriter writer, IReadOnlyCollection<VertexSerializationInfo> vertices)
        {
            writer.Write(vertices.Count);
            foreach (var vertex in vertices)
            {
                writer.Write(vertex.IsObstacle);
                writer.Write(vertex.Cost.CurrentCost);
                writer.WriterNeighborhood(vertex.Neighbourhood.Neighbours);
                writer.WriteIntArray(vertex.Position.CoordinatesValues);
            }
        }
    }
}
