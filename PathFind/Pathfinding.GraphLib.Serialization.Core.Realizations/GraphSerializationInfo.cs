using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Serialization.Core.Realizations
{
    public sealed class GraphSerializationInfo
    {
        public IReadOnlyList<int> DimensionsSizes { get; }

        public IReadOnlyCollection<VertexSerializationInfo> VerticesInfo { get; }

        public GraphSerializationInfo(IGraph<IVertex> graph)
        {
            DimensionsSizes = graph.DimensionsSizes;
            VerticesInfo = graph
                .Select(vertex => new VertexSerializationInfo(vertex))
                .ToReadOnly();
        }

        internal GraphSerializationInfo(int[] dimensionsSizes, IReadOnlyCollection<VertexSerializationInfo> info)
        {
            DimensionsSizes = dimensionsSizes;
            VerticesInfo = info;
        }
    }
}