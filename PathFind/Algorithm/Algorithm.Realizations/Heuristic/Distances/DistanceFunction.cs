using Algorithm.Interfaces;
using Common.Extensions;
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
            foreach(int i in (0, limit))
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