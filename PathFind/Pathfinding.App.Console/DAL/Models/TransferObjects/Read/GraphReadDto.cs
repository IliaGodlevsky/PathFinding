using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations;

namespace Pathfinding.App.Console.DAL.Models.TransferObjects.Read
{
    internal class GraphReadDto
    {
        public static readonly GraphReadDto Empty = new()
        {
            Id = 0,
            Graph = Graph<Vertex>.Empty
        };

        public int Id { get; set; }

        public IGraph<Vertex> Graph { get; set; }
    }
}
