using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Comparers;
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
            var visited = new HashSet<IVertex>(new VertexEqualityComparer());
            int level = smoothLevel;
            while (level-- > 0)
            {
                foreach (var vertex in graph)
                {
                    visited.Add(vertex);
                    if (vertex.Neighbours.Any(v => !visited.Contains(v)))
                    {
                        double avgCost = vertex.Neighbours
                            .Where(v => !visited.Contains(v))
                            .Average(neighbour => meanCost.Calculate(neighbour, vertex));
                        vertex.Cost = vertex.Cost.SetCost((int)Math.Round(avgCost, 0));
                    }
                }
                visited.Clear();
            }
        }
    }
}
