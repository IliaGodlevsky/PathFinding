using GraphLibrary.Coordinates;
using GraphLibrary.Coordinates.Interface;
using GraphLibrary.Info;
using GraphLibrary.Vertex.Cost;
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

        public bool IsEnd { get => true; set => _ = value; }
        public bool IsObstacle { get => false; set => _ = value; }
        public bool IsStart { get => true; set => _ = value; }
        public bool IsVisited { get => false; set => _ = value; }
        public VertexCost Cost { get => new VertexCost(0); set => _ = (VertexCost)value.Clone(); }
        public IList<IVertex> Neighbours { get => new List<IVertex>(); set => _ = value; }
        public IVertex ParentVertex { get => Instance; set => _ = value; }
        public double AccumulatedCost { get => double.PositiveInfinity; set => _ = value; }
        public ICoordinate Position { get => NullCoordinate.Instance; set => _ = value; }
        public VertexInfo Info => new VertexInfo(this);
        public void MarkAsEnd() { }
        public void MarkAsObstacle() { }
        public void MarkAsPath() { }
        public void MarkAsSimpleVertex() { }
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
