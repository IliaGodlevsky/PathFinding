using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class GraphMessage
    {
        public IGraph<Vertex> Graph { get; }

        public int Id { get; }

        public GraphMessage(IGraph<Vertex> graph, int id)
        {
            Graph = graph;
            Id = id;
        }
    }
}
