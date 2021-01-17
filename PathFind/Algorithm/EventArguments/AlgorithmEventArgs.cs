using GraphLib.Graphs;
using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex;
using GraphLib.Vertex.Interface;
using System;

namespace Algorithm.EventArguments
{
    public class AlgorithmEventArgs : EventArgs
    {
        public AlgorithmEventArgs()
        {
            Vertex = new DefaultVertex();
            Graph = new NullGraph();
        }

        public AlgorithmEventArgs(IGraph graph, IVertex vertex = null)
        {
            Vertex = vertex ?? new DefaultVertex();
            Graph = graph;
        }

        public IVertex Vertex { get; private set; }

        public IGraph Graph { get; private set; }
    }
}
