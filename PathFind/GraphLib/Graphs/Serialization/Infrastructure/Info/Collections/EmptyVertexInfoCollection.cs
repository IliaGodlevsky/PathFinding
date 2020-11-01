using GraphLib.Info.Interface;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GraphLib.Info.Containers
{
    [Serializable]
    public sealed class EmptyVertexInfoCollection : IVertexInfoCollection
    {
        public EmptyVertexInfoCollection()
        {
            verticesDto = new VertexInfo[] { };
        }

        public IEnumerable<int> DimensionsSizes => new int[] { };

        public bool IsDefault => true;

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
