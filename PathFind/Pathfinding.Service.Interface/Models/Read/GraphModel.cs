using Pathfinding.Domain.Core;
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
            Graph = null
        };

        public int Id { get; set; }

        public string Name { get; set; }

        public SmoothLevels SmoothLevel { get; set; }

        public Neighborhoods Neighborhood { get; set; }

        public GraphStatuses Status { get; set; }

        public IGraph<T> Graph { get; set; }
    }
}
