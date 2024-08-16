using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Service.Interface;
using Pathfinding.Shared.Primitives;
using System;

namespace Pathfinding.Infrastructure.Business.Layers
{
    public sealed class VertexCostLayer : ILayer
    {
        private readonly Func<InclusiveValueRange<int>, int> generator;
        private readonly InclusiveValueRange<int> costRange;

        public VertexCostLayer(InclusiveValueRange<int> costRange, 
            Func<InclusiveValueRange<int>, int> generator)
        {
            this.costRange = costRange;
            this.generator = generator;
        }

        public void Overlay(IGraph<IVertex> graph)
        {
            foreach (var vertex in graph)
            {
                var costValue = generator(costRange);
                vertex.Cost = new VertexCost(costValue, costRange);
            }
        }
    }
}
