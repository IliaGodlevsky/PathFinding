using System;
using System.Collections.Generic;
using System.Linq;
using GraphLib.Exceptions;
using GraphLib.Interfaces;

namespace Algorithm.Realizations.Heuristic
{
    public abstract class BaseHeuristic
    {
        public double Calculate(IVertex first, IVertex second)
        {
            #region InvariantsObservance
            if (first == null || second == null)
            {
                throw new ArgumentNullException();
            }

            if (first.Position == null || second.Position == null)
            {
                throw new ArgumentException("Vertex coordinate was set to null");
            }

            if (first.Position.CoordinatesValues.Count() != second.Position.CoordinatesValues.Count())
            {
                string message = "Can't calculate distance between vertices with different coordinates count";
                throw new WrongNumberOfDimensionsException(message);
            }
            #endregion

            var firstCoordinateValues = first.Position.CoordinatesValues;
            var secondCoordinateValues = second.Position.CoordinatesValues;
            var zippedArray = firstCoordinateValues.Zip(secondCoordinateValues, ZipMethod);
            return Aggregate(zippedArray);
        }

        protected abstract double Aggregate(IEnumerable<double> collection);

        protected abstract double ZipMethod(int first, int second);
    }
}