using GraphLib.Base;
using GraphLib.Interfaces;
using GraphLib.Serialization.Extensions;
using System.Collections.Generic;
using ValueRange;

namespace GraphLib.Serialization
{
    public sealed class GraphSerializationInfo
    {
        public int[] DimensionsSizes { get; }

        public IReadOnlyCollection<VertexSerializationInfo> VerticesInfo { get; }

        public InclusiveValueRange<int> CostRange { get; }

        public GraphSerializationInfo(IGraph graph)
        {
            DimensionsSizes = graph.DimensionsSizes;
            VerticesInfo = graph.GetVerticesSerializationInfo();
            CostRange = BaseVertexCost.CostRange;
        }

        internal GraphSerializationInfo(int[] dimensionsSizes,
            IReadOnlyCollection<VertexSerializationInfo> info, InclusiveValueRange<int> range)
        {
            DimensionsSizes = dimensionsSizes;
            VerticesInfo = info;
            CostRange = range;
        }
    }
}