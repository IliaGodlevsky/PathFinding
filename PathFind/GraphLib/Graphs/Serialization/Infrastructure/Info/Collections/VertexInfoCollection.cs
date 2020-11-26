using GraphLib.Extensions;
using GraphLib.Info;
using GraphLib.Vertex.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Graphs.Serialization.Infrastructure.Info.Collections
{
    [Serializable]
    public sealed class VertexInfoCollection : IEnumerable<VertexInfo>
    {
        public VertexInfoCollection(IEnumerable<IVertex> vertices,
            params int[] dimensionsSizes)
        {
            DimensionsSizes = dimensionsSizes.ToArray();
            verticesDto = vertices.Select(vertex => vertex.GetInfo()).ToArray();
        }

        public IEnumerable<int> DimensionsSizes { get; private set; }


        public IEnumerator<VertexInfo> GetEnumerator()
        {
            return verticesDto.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return verticesDto.GetEnumerator();
        }

        private readonly IEnumerable<VertexInfo> verticesDto;
    }
}
