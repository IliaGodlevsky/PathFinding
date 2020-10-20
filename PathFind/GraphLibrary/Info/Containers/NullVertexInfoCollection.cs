using GraphLibrary.Info.Interface;
using GraphLibrary.Vertex.Interface;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GraphLibrary.Info.Containers
{
    [Serializable]
    public sealed class NullVertexInfoCollection : IVertexInfoCollection
    {
        public static NullVertexInfoCollection Instance
        {
            get
            {
                if (instance == null)
                    instance = new NullVertexInfoCollection();
                return instance;
            }
        }

        private NullVertexInfoCollection()
        {
            verticesDto = new Info<IVertex>[] { };
        }

        public IEnumerable<int> DimensionsSizes => new int[] { };

        public IEnumerator<Info<IVertex>> GetEnumerator()
        {
            return verticesDto.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return verticesDto.GetEnumerator();
        }

        private static NullVertexInfoCollection instance = null;
        private readonly IEnumerable<Info<IVertex>> verticesDto;
    }
}
