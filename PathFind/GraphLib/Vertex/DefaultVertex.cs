using GraphLib.Coordinates.Interface;
using GraphLib.Coordinates;
using GraphLib.Info;
using GraphLib.Vertex.Cost;
using GraphLib.Vertex.Interface;
using System.Collections.Generic;

namespace GraphLib.Vertex
{
    public sealed class DefaultVertex : IVertex
    {
        public DefaultVertex()
        {

        }

        public bool IsEnd { get => true; set => _ = value; }
        public bool IsObstacle { get => true; set => _ = value; }
        public bool IsStart { get => true; set => _ = value; }
        public bool IsVisited { get => true; set => _ = value; }
        public VertexCost Cost { get => new VertexCost(default); set => _ = value; }
        public IList<IVertex> Neighbours { get => new List<IVertex>(); set => _ = value; }
        public IVertex ParentVertex { get => new DefaultVertex(); set => _ = value; }
        public double AccumulatedCost { get => double.PositiveInfinity; set => _ = value; }
        public ICoordinate Position { get => new DefaultCoordinate(); set => _ = value; }
        public VertexInfo Info => new VertexInfo(this);

        public bool IsDefault => true;

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
