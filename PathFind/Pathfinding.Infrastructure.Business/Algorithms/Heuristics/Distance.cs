using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Shared.Extensions;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Pathfinding.Infrastructure.Business.Algorithms.Heuristics
{
    public abstract class Distance : IHeuristic
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate(IVertex first, IVertex second)
        {
            double result = first.Position.CoordinatesValues
                .Zip(second.Position.CoordinatesValues, Zip)
                .AggregateOrDefault(Aggregate);
            return ProcessResult(result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual double ProcessResult(double result) => result;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual double Aggregate(double a, double b) => a + b;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract double Zip(int first, int second);
    }
}