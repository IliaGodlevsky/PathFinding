using GraphLib.Extensions;
using GraphLib.Interface;
using GraphLib.Serialization.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Serialization
{
    [Serializable]
    public sealed class GraphSerializationInfo : IEnumerable<VertexSerializationInfo>
    {
        public GraphSerializationInfo(IGraph graph)
        {
            DimensionsSizes = graph.DimensionsSizes.ToArray();

            verticesDto = graph.Vertices
                .Select(vertex => vertex.GetSerializationInfo())
                .ToArray();
        }

        public IEnumerable<int> DimensionsSizes { get; private set; }


        public IEnumerator<VertexSerializationInfo> GetEnumerator()
        {
            return verticesDto.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return verticesDto.GetEnumerator();
        }

        private readonly IEnumerable<VertexSerializationInfo> verticesDto;
    }
}
