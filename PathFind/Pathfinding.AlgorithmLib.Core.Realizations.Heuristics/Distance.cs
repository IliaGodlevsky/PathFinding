using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface;
using System;
using System.Runtime.CompilerServices;

namespace Pathfinding.AlgorithmLib.Core.Realizations.Heuristics
{
    public abstract class Distance : IHeuristic
    {
        public double Calculate(IVertex first, IVertex second)
        {
            double result = default;
            var firstCoordinates = first.Position;
            var secondCoordinates = second.Position;
            int limit = Math.Min(firstCoordinates.Count, secondCoordinates.Count);
            for (int i = 0; i < limit; i++)
            {
                double zipped = Zip(firstCoordinates[i], secondCoordinates[i]);
                result = Aggregate(result, zipped);
            }
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