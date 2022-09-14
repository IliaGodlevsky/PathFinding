using Algorithm.Interfaces;
using GraphLib.Interfaces;

namespace Algorithm.Realizations.Heuristic.Distances
{
    public abstract class DistanceFunction : IHeuristic
    {
        public double Calculate(IVertex first, IVertex second)
        {
            double result = default;
            var firstCoordinates = first.Position;
            var secondCoordinates = second.Position;
            for (int i = 0; i < firstCoordinates.Count; i++)
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