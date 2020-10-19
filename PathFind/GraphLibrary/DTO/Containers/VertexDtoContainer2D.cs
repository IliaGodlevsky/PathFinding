using GraphLibrary.DTO.Interface;
using GraphLibrary.Vertex.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GraphLibrary.DTO
{
    [Serializable]
    public class VertexDtoContainer2D : IVertexDtoContainer
    {
        public VertexDtoContainer2D(IEnumerable<IVertex> vertices, int width, int height)
        {
            Width = width;
            Height = height;
            verticesDto = vertices.AsParallel().Select(vertex => vertex.Dto).ToArray();
        }
       
        public int Width { get; private set; }
        public int Height { get; private set; }

        public IEnumerable<int> DimensionsSizes => new int[] { Width, Height };

        public IEnumerator<Dto<IVertex>> GetEnumerator()
        {
            return verticesDto.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return verticesDto.GetEnumerator();
        }

        private readonly IEnumerable<Dto<IVertex>> verticesDto;
    }
}
