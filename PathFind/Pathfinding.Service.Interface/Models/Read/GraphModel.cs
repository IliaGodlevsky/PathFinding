using Pathfinding.Domain.Interface;

namespace Pathfinding.Service.Interface.Models.Read
{
    public record class GraphModel<T>
        where T : IVertex
    {
        public static readonly GraphModel<T> Empty = new GraphModel<T>()
        {
            Id = 0,
            Name = string.Empty,
            SmoothLevel = string.Empty,
            Neighborhood = string.Empty,
            Graph = null
        };

        public int Id { get; set; }

        public string Name { get; set; }

        public string SmoothLevel { get; set; }

        public string Neighborhood { get; set; }

        public IGraph<T> Graph { get; set; }
    }
}
