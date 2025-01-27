using Pathfinding.Service.Interface;
using System.Runtime.CompilerServices;

namespace Pathfinding.Infrastructure.Business.Algorithms.Heuristics
{
    public sealed class WeightedHeuristic : IHeuristic
    {
        private readonly IHeuristic heuristic;
        private readonly double weight;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public WeightedHeuristic(IHeuristic heuristic, double weight)
        {
            this.heuristic = heuristic;
            this.weight = weight;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate(IPathfindingVertex first, IPathfindingVertex second)
        {
            return heuristic.Calculate(first, second) * weight;
        }
    }
}
