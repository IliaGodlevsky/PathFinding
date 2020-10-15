using GraphLibrary.DTO.Interface;
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
            verticesDto = new VertexDto[] { };
        }

        public IEnumerable<int> DimensionsSizes => new int[] { };

        public IEnumerator<VertexDto> GetEnumerator()
        {
            return verticesDto.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return verticesDto.GetEnumerator();
        }

        private static NullVertexDtoContainer instance = null;
        private readonly IEnumerable<VertexDto> verticesDto;
    }
}
