using GraphLibrary.Coordinates;
using GraphLibrary.Coordinates.Interface;
using GraphLibrary.Extensions.CustomTypeExtensions;
using GraphLibrary.Extensions.SystemTypeExtensions;
using GraphLibrary.Info;
using GraphLibrary.Vertex.Cost;
using GraphLibrary.Vertex.Interface;
using System.Collections.Generic;

namespace GraphLibrary.UnitTests.Classes
{
    public class TestVertex : IVertex
    {
        public TestVertex() => this.Initialize();

        public TestVertex(Info<IVertex> dto) : this() => this.InitializeByInfo(dto);

        public bool IsEnd { get; set; }
        public bool IsObstacle { get; set; }
        public bool IsStart { get; set; }
        public bool IsVisited { get; set; }
        public VertexCost Cost { get; set; }
        public IVertex ParentVertex { get; set; }
        public double AccumulatedCost { get; set; }
        public ICoordinate Position { get; set; }
        public Info<IVertex> Info => new Info<IVertex>(this);
        public IList<IVertex> Neighbours { get; set; }
        public void MarkAsEnd() { }
        public void MarkAsSimpleVertex() { }
        public void MarkAsObstacle()
        {
            this.WashVertex();
        }
        public void MarkAsPath() { }
        public void MarkAsStart() { }
        public void MarkAsVisited() { }
        public void MarkAsEnqueued() { }

        public void MakeUnweighted()
        {
            
        }

        public void MakeWeighted()
        {
            
        }
    }
}
