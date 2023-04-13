using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Serialization.Core.Realizations
{
    public sealed class GraphSerializationInfo
    {
        public IReadOnlyList<int> DimensionsSizes { get; }

        public VertexSerializationInfo[] VerticesInfo { get; }

        internal GraphSerializationInfo(IGraph<IVertex> graph)
        {
            DimensionsSizes = graph.DimensionsSizes;
            VerticesInfo = graph
                .Select(vertex => new VertexSerializationInfo(vertex))
                .ToArray();
        }

        internal GraphSerializationInfo(int[] dimensionsSizes,
            VertexSerializationInfo[] info)
        {
            DimensionsSizes = dimensionsSizes;
            VerticesInfo = info;
        }
    }
}