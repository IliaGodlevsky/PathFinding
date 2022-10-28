using Algorithm.Interfaces;
using GraphLib.Interfaces;
using System;

namespace Algorithm.Realizations.Heuristic.Distances
{
    public abstract class DistanceFunction : IHeuristic
    {
        public double Calculate(IVertex first, IVertex second)
        {
            double result = default;
            var firstCoordinates = first.Position;
            var secondCoordinates = second.Position;
            int limit = Math.Min(firstCoordinates.Count, secondCoordinates.Count);
            for (int i = 0; i < limit; i++)
            {
                double zipped = ZipMethod(firstCoordinates[i], secondCoordinates[i]);
                result = AggregateMethod(result, zipped);
            }
            return ProcessResult(result);
        }

        protected virtual double ProcessResult(double result) => result;

        protected virtual double AggregateMethod(double a, double b) => a + b;

        protected abstract double ZipMethod(int first, int second);
    }
}