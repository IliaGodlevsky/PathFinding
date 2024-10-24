﻿using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Shared.Extensions;
using System;
using System.Linq;

namespace Pathfinding.Infrastructure.Business.Layers
{
    public sealed class SmoothLayer : ILayer
    {
        public void Overlay(IGraph<IVertex> graph)
        {
            var costs = graph.Select(GetAverageCost);
            foreach (var (Vertex, Price) in graph.Zip(costs, (v, p) => (Vertex: v, Price: p)))
            {
                var range = Vertex.Cost.CostRange;
                int cost = range.ReturnInRange(Price);
                Vertex.Cost.CurrentCost = cost;
            }
        }

        private int GetAverageCost(IVertex vertex)
        {
            return (int)vertex.Neighbours
                .Average(neighbour => CalculateMeanCost(neighbour, vertex));
        }

        private double CalculateMeanCost(IVertex neighbor, IVertex vertex)
        {
            int neighbourCost = neighbor.Cost.CurrentCost;
            int vertexCost = vertex.Cost.CurrentCost;
            double averageCost = (vertexCost + neighbourCost) / 2;
            double roundAverageCost = Math.Ceiling(averageCost);
            return Convert.ToInt32(roundAverageCost);
        }
    }
}