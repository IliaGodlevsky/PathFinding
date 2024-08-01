using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Service.Interface;
using System.Linq;

namespace Pathfinding.Infrastructure.Business.Layers
{
    public sealed class SmoothLayer : ILayer
    {
        private readonly IMeanCost meanCost;

        public SmoothLayer(IMeanCost meanCost)
        {
            this.meanCost = meanCost;
        }

        public void Overlay(IGraph<IVertex> graph)
        {
            var costs = graph.Select(GetAverageCost);
            graph.ApplyCosts(costs);
        }

        private int GetAverageCost(IVertex vertex)
        {
            return (int)vertex.Neighbours
                .Average(neighbour => meanCost.Calculate(neighbour, vertex));
        }
    }
}