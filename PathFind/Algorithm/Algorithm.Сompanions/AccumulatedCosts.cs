using Algorithm.Сompanions.Interface;
using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Algorithm.Сompanions
{
    public sealed class AccumulatedCosts : IAccumulatedCosts
    {
        public AccumulatedCosts(IEnumerable<IVertex> graph, double startCost)
        {
            accumulatedCosts = new ConcurrentDictionary<ICoordinate, double>();
            this.startCost = startCost;
            graph.ForEach(SetAccumulatedCostToStartCost);
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

        private void SetAccumulatedCostToStartCost(IVertex vertex)
        {
            if (!vertex.IsNullObject())
            {
                accumulatedCosts[vertex.Position] = startCost;
            }
        }

        private readonly IDictionary<ICoordinate, double> accumulatedCosts;
        private readonly double startCost;
    }
}