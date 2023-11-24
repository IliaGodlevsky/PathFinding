using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class GraphMessage
    {
        public IGraph<Vertex> Graph { get; }

        public GraphMessage(IGraph<Vertex> graph)
        {
            Graph = graph;
        }
    }
}
