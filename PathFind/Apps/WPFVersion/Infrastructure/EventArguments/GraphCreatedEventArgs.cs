using GraphLib.Realizations.Graphs;
using System;
using WPFVersion.Model;

namespace WPFVersion.Infrastructure.EventArguments
{
    internal class GraphCreatedEventArgs : EventArgs
    {
        public Graph2D<Vertex> Graph { get; }

        public GraphCreatedEventArgs(Graph2D<Vertex> graph)
        {
            Graph = graph;
        }
    }
}
