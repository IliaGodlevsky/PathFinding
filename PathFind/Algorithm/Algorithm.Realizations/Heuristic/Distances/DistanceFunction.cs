using Algorithm.Interfaces;
using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace Algorithm.Realizations.Heuristic.Distances
{
    public abstract class DistanceFunction : IHeuristic
    {
        public double Calculate(IVertex first, IVertex second)
        {
            double result = default;
            var firstCoordinates = first.GetCoordinates();
            var secondCoordinates = second.GetCoordinates();
            for (int i = 0; i < firstCoordinates.Length; i++)
            {
                double zipped = ZipMethod(firstCoordinates[i], secondCoordinates[i]);
                result = Operation(result, zipped);
            }
            return ProcessResult(result);
        }

        protected virtual double ProcessResult(double result) => result;

        protected virtual double Operation(double a, double b) => a + b;

        protected abstract double ZipMethod(int first, int second);
    }
}