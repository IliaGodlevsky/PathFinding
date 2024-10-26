using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Shared.Primitives;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Infrastructure.Business.Layers
{
    public sealed class GraphStateLayer : ILayer
    {
        private readonly IReadOnlyDictionary<Coordinate, int> costs;
        private readonly IReadOnlyDictionary<Coordinate, bool> verticesStates;

        private readonly GraphStateModel state;

        public GraphStateLayer(GraphStateModel state)
        {
            this.state = state;
            costs = state.Costs.ToDictionary(x => x.Position, x => x.Cost);
            verticesStates = state.Obstacles
                .Select(x => (Position: x, State: true))
                .Concat(state.Regulars.Select(x => (Position: x, State: false)))
                .ToDictionary(x => x.Position, x => x.State);
        }

        public void Overlay(IGraph<IVertex> graph)
        {
            foreach (var vertex in graph)
            {
                var cost = costs[vertex.Position];
                vertex.Cost = new VertexCost(cost, default);
                vertex.IsObstacle = verticesStates[vertex.Position];
            }
        }
    }
}
