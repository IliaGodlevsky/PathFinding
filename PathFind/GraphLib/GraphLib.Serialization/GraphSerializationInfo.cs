using GraphLib.Base;
using GraphLib.Interfaces;
using GraphLib.Serialization.Extensions;
using System;
using ValueRange;

namespace GraphLib.Serialization
{
    [Serializable]
    public sealed class GraphSerializationInfo
    {
        public GraphSerializationInfo(IGraph graph)
        {
            DimensionsSizes = graph.DimensionsSizes;
            VerticesInfo = graph.GetVerticesSerializationInfo();
            CostRange = BaseVertexCost.CostRange;
        }

        public int[] DimensionsSizes { get; }
        public VertexSerializationInfo[] VerticesInfo { get; }
        public InclusiveValueRange<int> CostRange { get; }
    }
}
