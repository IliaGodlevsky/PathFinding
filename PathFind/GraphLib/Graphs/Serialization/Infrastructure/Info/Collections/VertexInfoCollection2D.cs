using GraphLib.Graphs.Serialization.Infrastructure.Info.Collections.Interface;
using GraphLib.Vertex.Interface;
using System;
using System.Collections.Generic;

namespace GraphLib.Graphs.Serialization.Infrastructure.Info.Collections
{
    [Serializable]
    public class VertexInfoCollection2D : BaseVertexInfoCollection
    {
        public VertexInfoCollection2D(IEnumerable<IVertex> vertices,
            int width, int length) : base(vertices)
        {
            Width = width;
            Length = length;
        }

        public int Width { get; private set; }

        public int Length { get; private set; }

        public override IEnumerable<int> DimensionsSizes => new int[] { Width, Length };
    }
}
