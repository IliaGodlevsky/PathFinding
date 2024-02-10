using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.App.Console.Messaging.Messages
{
    internal sealed class GraphMessage
    {
        public GraphReadDto Graph { get; set; }

        public GraphMessage(IGraph<Vertex> graph, int id)
            : this(new() { Id = id, Graph = graph })
        {
        }

        public GraphMessage(GraphReadDto dto)
        {
            Graph = dto;
        }
    }
}
