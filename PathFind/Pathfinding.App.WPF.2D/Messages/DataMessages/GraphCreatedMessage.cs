using Pathfinding.App.WPF._2D.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;

namespace Pathfinding.App.WPF._2D.Messages.DataMessages
{
    internal sealed class GraphCreatedMessage
    {
        public Graph2D<Vertex> Graph { get; }

        public GraphCreatedMessage(Graph2D<Vertex> graph)
        {
            Graph = graph;
        }
    }
}
