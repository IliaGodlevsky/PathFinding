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
                    double avgCost = vertex.Neighbours
                        .Average(neighbour => meanCost.Calculate(neighbour, vertex));
                    costs.Add((int)Math.Round(avgCost, 0));
                }
                graph.ApplyCosts(costs);
                costs.Clear();
            }
        }
    }
}
