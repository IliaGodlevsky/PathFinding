using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Factory.Interface;
using Shared.Primitives.ValueRange;
using Shared.Random;
using Shared.Random.Extensions;

namespace Pathfinding.GraphLib.Factory.Realizations.Layers
{
    public sealed class VertexCostLayer : ILayer
    {
        private IVertexCostFactory CostFactory { get; }

        private InclusiveValueRange<int> CostRange { get; }

        private IRandom Random { get; }

        public VertexCostLayer(IVertexCostFactory costFactory,
            InclusiveValueRange<int> costRange, IRandom random)
        {
            this.CostFactory = costFactory;
            this.CostRange = costRange;
            this.Random = random;
        }

        public void Overlay(IGraph<IVertex> graph)
        {
            foreach (var vertex in graph)
            {
                var costValue = Random.NextInt(CostRange);
                vertex.Cost = CostFactory.CreateCost(costValue, CostRange);
            }
        }
    }
}
