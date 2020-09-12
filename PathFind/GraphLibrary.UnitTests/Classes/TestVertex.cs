using GraphLibrary.DTO;
using GraphLibrary.Extensions.CustomTypeExtensions;
using GraphLibrary.Vertex.Interface;
using System.Collections.Generic;

namespace GraphLibrary.UnitTests.Classes
{
    public class TestVertex : IVertex
    {
        public TestVertex() => this.Initialize();

        public TestVertex(VertexDto dto) : this() => this.Initialize(dto);

        public bool IsEnd { get; set; }
        public bool IsObstacle { get; set; }
        public bool IsStart { get; set; }
        public bool IsVisited { get; set; }
        public int Cost { get; set; }
        public List<IVertex> Neighbours { get; set; }
        public IVertex ParentVertex { get; set; }
        public double AccumulatedCost { get; set; }
        public Position Position { get; set; }
        public VertexDto Dto => new VertexDto(this);

        public void MarkAsEnd() { }

        public void MarkAsSimpleVertex() { }

        public void MarkAsObstacle()
        {
            this.WashVertex();
        }

        public void MarkAsPath() { }

        public void MarkAsStart() { }

        public void MarkAsVisited() { }
    }
}
