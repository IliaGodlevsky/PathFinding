using Algorithm.Сompanions.Interface;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;

namespace Algorithm.Сompanions
{
    public sealed class AccumulatedCosts : IAccumulatedCosts
    {
        public AccumulatedCosts(IEnumerable<IVertex> graph, double startCost)
        {
            accumulatedCosts = new Dictionary<ICoordinate, double>();
            graph.ForEach(vertex => Reevaluate(vertex, startCost));
        }

        public AccumulatedCosts(IEnumerable<IVertex> graph, Func<IVertex, double> function)
        {
            accumulatedCosts = new Dictionary<ICoordinate, double>();
            graph.ForEach(vertex => Reevaluate(vertex, function(vertex)));
        }

        public void Reevaluate(IVertex vertex, double accumulatedCost)
        {
            accumulatedCosts[vertex.Position] = accumulatedCost;
        }

        public double GetAccumulatedCost(IVertex vertex)
        {
            if (accumulatedCosts.TryGetValue(vertex.Position, out var cost))
            {
                return cost;
            }

            string message = $"Could not find cost of {vertex.GetInforamtion()}";
            throw new KeyNotFoundException(message);
        }

        public int Compare(IVertex vertex, double accumulatedCost)
        {
            var vertexAccumulatedCost = GetAccumulatedCost(vertex);
            return vertexAccumulatedCost.CompareTo(accumulatedCost);
        }

        public void Clear()
        {
            accumulatedCosts.Clear();
        }

        private readonly IDictionary<ICoordinate, double> accumulatedCosts;
    }
}