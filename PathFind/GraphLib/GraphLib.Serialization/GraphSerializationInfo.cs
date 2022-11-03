using Common.Extensions.EnumerableExtensions;
using GraphLib.Base;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;
using ValueRange;

namespace GraphLib.Serialization
{
    public sealed class GraphSerializationInfo
    {
        public IReadOnlyList<int> DimensionsSizes { get; }

        public IReadOnlyCollection<VertexSerializationInfo> VerticesInfo { get; }

        public InclusiveValueRange<int> CostRange { get; }

        public GraphSerializationInfo(IGraph<IVertex> graph)
        {
            DimensionsSizes = graph.DimensionsSizes;
            VerticesInfo = graph
                .Select(vertex => new VertexSerializationInfo(vertex))
                .ToReadOnly();
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