using GraphLib.Info;
using GraphLib.Vertex.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Graphs.Serialization.Infrastructure.Info.Collections.Interface
{
    [Serializable]
    public abstract class BaseVertexInfoCollection : IVertexInfoCollection
    {
        public BaseVertexInfoCollection(IEnumerable<IVertex> vertices)
        {
            verticesDto = vertices.Select(vertex => vertex.Info).ToArray();
        }

        public abstract IEnumerable<int> DimensionsSizes { get; }

        public bool IsDefault => false;

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
