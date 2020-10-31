using GraphLib.Info.Interface;
using GraphLib.Vertex.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Info.Containers
{
    [Serializable]
    public class VertexInfoCollection2D : IVertexInfoCollection
    {
        public VertexInfoCollection2D(IEnumerable<IVertex> vertices, int width, int height)
        {
            Width = width;
            Height = height;
            verticesDto = vertices.Select(vertex => vertex.Info).ToArray();
        }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public IEnumerable<int> DimensionsSizes => new int[] { Width, Height };

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
