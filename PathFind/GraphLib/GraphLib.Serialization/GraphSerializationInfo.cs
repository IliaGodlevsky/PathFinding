using GraphLib.Interfaces;
using GraphLib.Serialization.Extensions;
using System;
using System.Linq;

namespace GraphLib.Serialization
{
    [Serializable]
    public sealed class GraphSerializationInfo
    {
        public GraphSerializationInfo(IGraph graph)
        {
            DimensionsSizes = graph.DimensionsSizes.ToArray();

            VerticesInfo = graph.Vertices
                .Select(SerializationInfo)
                .ToArray();
        }

        public int[] DimensionsSizes { get; }
        public VertexSerializationInfo[] VerticesInfo { get; }

        private VertexSerializationInfo SerializationInfo(IVertex vertex)
        {
            return vertex.GetSerializationInfo();
        }
    }
}
