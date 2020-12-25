using Common.Interfaces;
using System;
using System.Collections.Generic;

namespace GraphLib.Coordinates.Abstractions
{
    /// <summary>
    /// Makes it possible to have coordinates values and 
    /// makes it possible to find objects around
    /// </summary>
    public interface ICoordinate : ICloneable, IDefault
    {
        /// <summary>
        /// An array of coordinate values of the object
        /// </summary>
        IEnumerable<int> CoordinatesValues { get; }

        /// <summary>
        /// Objects of <see cref="ICoordinate"/> around this object
        /// </summary>
        IEnumerable<ICoordinate> Environment { get; }
    }
}
