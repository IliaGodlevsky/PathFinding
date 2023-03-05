using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Smoothing.Interface;
using System;
using System.Collections.Generic;
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
            var costs = new List<int>();
            while (level-- > 0)
            {
                foreach (var vertex in graph)
                {
                    int avgCost = GetAverageCost(vertex.Neighbours, vertex);
                    costs.Add(avgCost);
                }
                graph.ApplyCosts(costs);
                costs.Clear();
            }
        }

        private int GetAverageCost(IEnumerable<IVertex> vertices, TVertex vertex)
        {
            var avg = vertices.Average(neighbour => meanCost.Calculate(neighbour, vertex));
            return (int)Math.Round(avg, 0);
        }
    }
}
