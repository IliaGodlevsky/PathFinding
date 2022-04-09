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
            for (int i = 0; i < array.Length; i++)
            {
                writer.Write(array[i]);
            }
        }

        private static void WriteRange(this BinaryWriter writer, InclusiveValueRange<int> range)
        {
            writer.Write(range.UpperValueOfRange);
            writer.Write(range.LowerValueOfRange);
        }

        private static void WriterNeighborhood(this BinaryWriter writer, IReadOnlyCollection<ICoordinate> neighbourhood)
        {           
            writer.Write(neighbourhood.Count);
            foreach(var coordinate in neighbourhood)
            {
                writer.WriteIntArray(coordinate.CoordinatesValues);
            }
        }

        private static void WriteVertices(this BinaryWriter writer, VertexSerializationInfo[] vertices)
        {
            writer.Write(vertices.Length);
            foreach(var vertex in vertices)
            {
                writer.Write(vertex.IsObstacle);
                writer.Write(vertex.Cost.CurrentCost);
                writer.WriterNeighborhood(vertex.Neighbourhood.Neighbours);
                writer.WriteIntArray(vertex.Position.CoordinatesValues);
            }
        }
    }
}
