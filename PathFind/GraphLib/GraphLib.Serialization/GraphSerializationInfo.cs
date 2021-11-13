using GraphLib.Base;
using GraphLib.Interfaces;
using GraphLib.Serialization.Extensions;
using System;
using System.Linq;
using ValueRange;

namespace GraphLib.Serialization
{
    [Serializable]
    public sealed class GraphSerializationInfo
    {
        public GraphSerializationInfo(IGraph graph)
        {
            DimensionsSizes = graph
                .DimensionsSizes
                .ToArray();

            VerticesInfo = graph.Vertices
                .Select(VertexExtension.ToSerializationInfo)
                .ToArray();

            CostRange = BaseVertexCost.CostRange;
        }

        public int[] DimensionsSizes { get; }
        public VertexSerializationInfo[] VerticesInfo { get; }
        public InclusiveValueRange<int> CostRange { get; }
    }
}
