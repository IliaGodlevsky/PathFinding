using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Сompanions
{
    public sealed class AccumulatedCosts : IAccumulatedCosts
    {
        public AccumulatedCosts(IGraph graph, double startCost)
        {
            accumulatedCosts = new Dictionary<ICoordinate, double>();
            this.startCost = startCost;
            graph
                .Vertices
                .Where(vertex => !vertex.IsObstacle)
                .ForEach(SetAccumulatedCostToStartCost);
        }

        public void Reevaluate(IVertex vertex, double accumulatedCost)
        {
            accumulatedCosts[vertex.Position] = accumulatedCost;
        }

        public double GetAccumulatedCost(IVertex vertex)
        {
            if (accumulatedCosts.TryGetValue(vertex.Position, out double cost))
            {
                return cost;
            }

            throw new KeyNotFoundException($"Could not find cost of {vertex.GetInforamtion()}");
        }

        public int Compare(IVertex vertex, double accumulatedCost)
        {
            var comparer = Comparer<double>.Default;
            var vertexAccumulatedCost = GetAccumulatedCost(vertex);
            return comparer.Compare(vertexAccumulatedCost, accumulatedCost);
        }

        private void SetAccumulatedCostToStartCost(IVertex vertex)
        {
            accumulatedCosts[vertex.Position] = startCost;
        }

        private readonly Dictionary<ICoordinate, double> accumulatedCosts;
        private readonly double startCost;
    }
}