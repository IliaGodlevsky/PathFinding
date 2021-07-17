using GraphLib.Exceptions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Realizations.Heuristic
{
    /// <summary>
    /// A base class for all distance classes.
    /// Contains methods for calculating distance
    /// between two <see cref="IVertex"/> classes.
    /// This is an abstract class
    /// </summary>
    public abstract class Distance
    {
        /// <summary>
        /// Calculates distance between 
        /// two <see cref="IVertex"/> vertices
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>Distance between 
        /// two vertices</returns>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="WrongNumberOfDimensionsException"/>
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
            #endregion

            return Aggregate(first.GetCoordinates().Zip(second.GetCoordinates(), ZipMethod));
        }

        /// <summary>
        /// An aggregate function for 
        /// zipped array of vertices' coordinates
        /// </summary>
        /// <param name="collection"></param>
        /// <returns>An aggregation result of aggregating 
        /// the <paramref name="collection"/></returns>
        protected abstract double Aggregate(IEnumerable<double> collection);

        /// <summary>
        /// A method for zipping sequences 
        /// of vertices' coordinates
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        protected abstract double ZipMethod(int first, int second);
    }
}