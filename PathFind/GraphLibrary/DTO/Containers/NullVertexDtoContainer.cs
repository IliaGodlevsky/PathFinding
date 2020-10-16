using GraphLibrary.DTO.Interface;
using GraphLibrary.Vertex.Interface;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GraphLibrary.DTO.Containers
{
    [Serializable]
    public sealed class NullVertexDtoContainer : IVertexDtoContainer
    {
        public static NullVertexDtoContainer Instance
        {
            get
            {
                if (instance == null)
                    instance = new NullVertexDtoContainer();
                return instance;
            }
        }

        private NullVertexDtoContainer()
        {
            verticesDto = new Dto<IVertex>[] { };
        }

        public IEnumerable<int> DimensionsSizes => new int[] { };

        public IEnumerator<Dto<IVertex>> GetEnumerator()
        {
            return verticesDto.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return verticesDto.GetEnumerator();
        }

        private static NullVertexDtoContainer instance = null;
        private readonly IEnumerable<Dto<IVertex>> verticesDto;
    }
}
