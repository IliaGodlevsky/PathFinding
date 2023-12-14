using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Smoothing.Interface;
using System.Linq;

namespace Pathfinding.GraphLib.Smoothing
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