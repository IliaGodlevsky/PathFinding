using Common.ValueRanges;
using GraphLib.Base;
using GraphLib.Interfaces;
using GraphLib.Serialization.Extensions;
using System;
using System.Linq;

namespace GraphLib.Serialization
{
    [Serializable]
    public readonly struct GraphSerializationInfo
    {
        public GraphSerializationInfo(IGraph graph)
        {
            DimensionsSizes = graph
                .DimensionsSizes
                .ToArray();

            VerticesInfo = graph.Vertices
                .Select(v => v.GetSerializationInfo())
                .ToArray();

            CostRange = BaseVertexCost.CostRange;
        }

        public int[] DimensionsSizes { get; }
        public VertexSerializationInfo[] VerticesInfo { get; }
        public InclusiveValueRange<int> CostRange { get; }
    }
}
