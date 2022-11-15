using Pathfinding.App.WPF._2D.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using System;

namespace Pathfinding.App.WPF._2D.Infrastructure.EventArguments
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
