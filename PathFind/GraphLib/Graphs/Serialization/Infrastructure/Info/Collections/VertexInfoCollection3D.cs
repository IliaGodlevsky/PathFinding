using GraphLib.Info;
using GraphLib.Info.Interface;
using GraphLib.Vertex.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Graphs.Serialization.Infrastructure.Info.Collections
{
    [Serializable]
    public class VertexInfoCollection3D : IVertexInfoCollection
    {
        public VertexInfoCollection3D(IEnumerable<IVertex> vertices, int width, int length, int height)
        {
            Width = width;
            Length = length;
            Height = height;
            verticesDto = vertices.Select(vertex => vertex.Info).ToArray();
        }

        public int Width { get; private set; }

        public int Length { get; private set; }

        public int Height { get; private set; }

        public IEnumerable<int> DimensionsSizes => new int[] { Width, Length, Height };

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
