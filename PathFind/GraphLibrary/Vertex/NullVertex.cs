using GraphLibrary.Coordinates;
using GraphLibrary.DTO;
using GraphLibrary.Vertex.Interface;
using System.Collections.Generic;

namespace GraphLibrary.Vertex
{
    public sealed class NullVertex : IVertex
    {
        private static NullVertex instance = null;

        public static NullVertex Instance
        {
            get
            {
                if (instance == null)
                    instance = new NullVertex();
                return instance;
            }
        }

        private NullVertex()
        {

        }

        public bool IsEnd { get => true; set => _ = true; }
        public bool IsObstacle { get => false; set => _ = false; }
        public bool IsStart { get => true; set => _ = true; }
        public bool IsVisited { get => false; set => _ = false; }
        public int Cost { get => 0; set => _ = 0; }
        public IList<IVertex> Neighbours { get => new List<IVertex>(); set => _ = new List<IVertex>(); }
        public IVertex ParentVertex { get => Instance; set => _ = Instance; }
        public double AccumulatedCost { get => double.PositiveInfinity; set => _ = double.PositiveInfinity; }
        public Position Position { get => new Position(0,0); set => _ = new Position(0, 0); }
        public VertexDto Dto => new VertexDto(this);
        public void MarkAsEnd() { }
        public void MarkAsObstacle() { }
        public void MarkAsPath() { }
        public void MarkAsSimpleVertex() { }
        public void MarkAsStart() { }
        public void MarkAsVisited() { }
        public void MarkAsEnqueued() { }
    }
}
