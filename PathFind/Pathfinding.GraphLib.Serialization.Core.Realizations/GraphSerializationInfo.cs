using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Shared.Extensions;
using Shared.Primitives.ValueRange;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Serialization.Core.Realizations
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
            CostRange = VertexCost.CostRange;
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