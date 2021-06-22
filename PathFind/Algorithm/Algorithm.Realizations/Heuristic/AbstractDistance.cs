using GraphLib.Exceptions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Realizations.Heuristic
{
    public abstract class AbstractDistance
    {
        public double CalculateDistance(IVertex first, IVertex second)
        {
            #region Invariants Observance
            if (first == null || second == null)
            {
                throw new ArgumentNullException();
            }

            if (first.Position == null || second.Position == null)
            {
                throw new ArgumentException();
            }

            if (!first.HaveEqualDimensionsNumber(second))
            {
                string message = "Can't calculate distance between " +
                    "vertices with different coordinates count";
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