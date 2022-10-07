using GraphLib.Interfaces;
using GraphLib.Realizations.Graphs;
using System;

namespace WPFVersion.Infrastructure.EventArguments
{
    internal class GraphCreatedEventArgs : EventArgs
    {
        public Graph2D Graph { get; }

        public GraphCreatedEventArgs(Graph2D graph)
        {
            Graph = graph;
        }
    }
}
