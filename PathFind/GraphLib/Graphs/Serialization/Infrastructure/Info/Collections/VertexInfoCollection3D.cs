using GraphLib.Graphs.Serialization.Infrastructure.Info.Collections.Interface;
using GraphLib.Vertex.Interface;
using System;
using System.Collections.Generic;

namespace GraphLib.Graphs.Serialization.Infrastructure.Info.Collections
{
    [Serializable]
    public class VertexInfoCollection3D : BaseVertexInfoCollection
    {
        public VertexInfoCollection3D(IEnumerable<IVertex> vertices, 
            int width, int length, int height) : base(vertices)
        {
            Width = width;
            Length = length;
            Height = height;
        }

        public int Width { get; private set; }

        public int Length { get; private set; }

        public int Height { get; private set; }

        public override IEnumerable<int> DimensionsSizes => new int[] { Width, Length, Height };
    }
}
