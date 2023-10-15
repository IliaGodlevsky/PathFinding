using Pathfinding.App.WPF._2D.Model;
using Pathfinding.GraphLib.Core.Interface;
using System;

namespace Pathfinding.App.WPF._2D.Infrastructure.EventArguments
{
    internal class GraphCreatedEventArgs : EventArgs
    {
        public IGraph<Vertex> Graph { get; }

        public GraphCreatedEventArgs(IGraph<Vertex> graph)
        {
            Graph = graph;
        }
    }
}
