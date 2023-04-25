using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Smoothing.Interface;
using System.Linq;

namespace Pathfinding.GraphLib.Smoothing
{
    public sealed class SmoothLayer<TGraph, TVertex> : ILayer<TGraph, TVertex>
        where TGraph : IGraph<TVertex>
        where TVertex : IVertex
    {
        private readonly int smoothLevel;
        private readonly IMeanCost meanCost;

        public SmoothLayer(int smoothLevel, IMeanCost meanCost)
        {
            this.smoothLevel = smoothLevel;
            this.meanCost = meanCost;
        }

        public void Overlay(TGraph graph)
        {
            int level = smoothLevel;
            while (level-- > 0)
            {
                var costs = graph.Select(GetAverageCost);
                graph.ApplyCosts(costs);
            }
        }

        private int GetAverageCost(TVertex vertex)
        {
            return (int)vertex.Neighbours
                .Average(neighbour => meanCost.Calculate(neighbour, vertex));
        }
    }
}