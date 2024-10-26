using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface;
using System.Runtime.CompilerServices;

namespace Pathfinding.Infrastructure.Business.Algorithms.Heuristics
{
    public sealed class WeightedHeuristic : IHeuristic
    {
        private readonly IHeuristic heuristic;
        private readonly double weight;

        public WeightedHeuristic(IHeuristic heuristic, double weight)
        {
            this.heuristic = heuristic;
            this.weight = weight;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate(IVertex first, IVertex second)
        {
            return heuristic.Calculate(first, second) * weight;
        }
    }
}
