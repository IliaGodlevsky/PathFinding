using Algorithm.Сompanions.Interface;
using GraphLib.Interfaces;
using GraphLib.Utility;
using System.Collections.Generic;

namespace Algorithm.Сompanions
{
    public sealed class Costs : ICosts
    {
        private readonly Dictionary<ICoordinate, double> costs;

        public Costs()
        {
            costs = new Dictionary<ICoordinate, double>(new CoordinateEqualityComparer());
        }

        public void Reevaluate(IVertex vertex, double accumulatedCost)
        {
            costs[vertex.Position] = accumulatedCost;
        }

        public double GetCost(IVertex vertex)
        {
            if (costs.TryGetValue(vertex.Position, out double cost))
            {
                return cost;
            }

            throw new KeyNotFoundException();
        }

        public void Clear()
        {
            costs.Clear();
        }

        public bool Contains(IVertex vertex)
        {
            return costs.ContainsKey(vertex.Position);
        }
    }
}