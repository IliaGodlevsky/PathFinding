using GraphLib.Extensions;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Realizations.Heuristic.Distances
{
    public abstract class Distance
    {
        public double Calculate(IVertex first, IVertex second)
        {
            var firstCoordinates = first.GetCoordinates();
            var secondCoordinates = second.GetCoordinates();
            var zipped = firstCoordinates.Zip(secondCoordinates, ZipMethod);
            return Aggregate(zipped);
        }

        protected abstract double Aggregate(IEnumerable<double> collection);

        protected abstract double ZipMethod(int first, int second);
    }
}