﻿using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Shared.Extensions;
using Shared.Primitives.ValueRange;
using System.Collections.Generic;
using System.IO;

namespace Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions
{
    internal static class BinaryWriterExtensions
    {
        public static void WriteGraph(this BinaryWriter writer,
            GraphSerializationInfo info)
        {
            writer.WriteIntArray(info.DimensionsSizes);
            writer.WriteVertices(info.VerticesInfo);
        }

        private static void WriteIntArray(this BinaryWriter writer,
            IReadOnlyCollection<int> array)
        {
            writer.Write(array.Count);
            array.ForEach(writer.Write);
        }

        private static void WriterNeighborhood(this BinaryWriter writer,
            ICoordinate[] neighbourhood)
        {
            writer.Write(neighbourhood.Length);
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
                writer.WriterNeighborhood(vertex.Neighbourhood);
                writer.WriteIntArray(vertex.Position);
            }
        }
    }
}
