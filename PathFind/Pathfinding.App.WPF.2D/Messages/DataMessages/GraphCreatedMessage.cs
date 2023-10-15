using Pathfinding.App.WPF._2D.Model;
using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.App.WPF._2D.Messages.DataMessages
{
    internal sealed class GraphCreatedMessage
    {
        public IGraph<Vertex> Graph { get; }

        public GraphCreatedMessage(IGraph<Vertex> graph)
        {
            Graph = graph;
        }
    }
}
