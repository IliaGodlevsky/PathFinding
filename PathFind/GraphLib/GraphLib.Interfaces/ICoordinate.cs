using Common.Interfaces;
using System.Collections.Generic;

namespace GraphLib.Interface
{
    /// <summary>
    /// Makes it possible to have coordinates values
    /// </summary>
    public interface ICoordinate : IDefault
    {
        /// <summary>
        /// An array of coordinates values of the object
        /// </summary>
        IEnumerable<int> CoordinatesValues { get; }
    }
}
