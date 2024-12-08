using Pathfinding.Domain.Interface;
using Pathfinding.Shared.Primitives;
using System;

namespace Pathfinding.Infrastructure.Business.Layers
{
    public sealed class VertexCostLayer : ILayer
    {
        private readonly Func<InclusiveValueRange<int>, IVertexCost> generator;
        private readonly InclusiveValueRange<int> costRange;

        public VertexCostLayer(InclusiveValueRange<int> costRange,
            Func<InclusiveValueRange<int>, IVertexCost> generator)
        {
            this.costRange = costRange;
            this.generator = generator;
        }

        public void Overlay(IGraph<IVertex> graph)
        {
            foreach (var vertex in graph)
            {
                vertex.Cost = generator(costRange);
            }
        }
    }
}
