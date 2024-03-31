using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations;

namespace Pathfinding.App.Console.DAL.Models.TransferObjects.Read
{
    internal record GraphReadDto<T>
        where T : IVertex
    {
        public static readonly GraphReadDto<T> Empty = new()
        {
            Id = 0,
            Name = "Default",
            Graph = Graph<T>.Empty
        };

        public string Name { get; set; }

        public int Id { get; set; }

        public IGraph<T> Graph { get; set; }
    }
}
