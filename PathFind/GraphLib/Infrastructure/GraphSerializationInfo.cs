using GraphLib.Extensions;
using GraphLib.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Infrastructure
{
    [Serializable]
    public sealed class GraphSerializationInfo : IEnumerable<VertexSerializationInfo>
    {
        public GraphSerializationInfo(IEnumerable<IVertex> vertices,
            params int[] dimensionsSizes)
        {
            DimensionsSizes = dimensionsSizes.ToArray();
            verticesDto = vertices.Select(vertex => vertex.GetSerializationInfo()).ToArray();
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
