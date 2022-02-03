using GraphLib.Interfaces;
using System;

namespace GraphLib.Infrastructure
{
    public class CostChangedEventArgs : EventArgs
    {
        public IVertexCost Cost { get; }
        public IVertex Vertex { get; }

        public CostChangedEventArgs(IVertexCost cost, IVertex vertex)
        {
            Cost = cost;
            Vertex = vertex;
        }
    }
}
