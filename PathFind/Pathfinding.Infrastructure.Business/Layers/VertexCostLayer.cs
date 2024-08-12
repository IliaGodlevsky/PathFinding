using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Service.Interface;
using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Interface;
using Pathfinding.Shared.Primitives;

namespace Pathfinding.Infrastructure.Business.Layers
{
    public sealed class VertexCostLayer : ILayer
    {
        private InclusiveValueRange<int> CostRange { get; }

        private IRandom Random { get; }

        public VertexCostLayer(InclusiveValueRange<int> costRange, IRandom random)
        {
            CostRange = costRange;
            Random = random;
        }

        public void Overlay(IGraph<IVertex> graph)
        {
            foreach (var vertex in graph)
            {
                var costValue = Random.NextInt(CostRange);
                vertex.Cost = new VertexCost(costValue, CostRange);
            }
        }
    }
}
