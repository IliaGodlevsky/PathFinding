using Pathfinding.App.Console.Model;
using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface.Models.Read;

namespace Pathfinding.App.Console.Messaging.Messages
{
    internal sealed class GraphMessage
    {
        public GraphModel<Vertex> Graph { get; set; }

        public GraphMessage(IGraph<Vertex> graph, int id, string name)
            : this(new() { Id = id, Graph = graph, Name = name })
        {
        }

        public GraphMessage(GraphModel<Vertex> graph)
        {
            Graph = graph;
        }
    }
}
