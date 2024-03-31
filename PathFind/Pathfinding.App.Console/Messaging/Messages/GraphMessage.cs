using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.App.Console.Messaging.Messages
{
    internal sealed class GraphMessage
    {
        public GraphReadDto<Vertex> Graph { get; set; }

        public GraphMessage(IGraph<Vertex> graph, int id, string name)
            : this(new() { Id = id, Graph = graph, Name = name })
        {
        }

        public GraphMessage(GraphReadDto<Vertex> dto)
        {
            Graph = dto;
        }
    }
}
