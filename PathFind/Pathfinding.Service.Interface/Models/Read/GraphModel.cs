using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface.Models.Undefined;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Models.Read
{
    public record class GraphModel<T> : IGraphLayersInfo
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

        //public IReadOnlyCollection<VertexAssembleModel> Vertices { get; set; }

        public IGraph<T> Graph { get; set; }
    }
}
