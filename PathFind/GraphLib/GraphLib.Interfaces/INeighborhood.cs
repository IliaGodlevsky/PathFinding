using Common.Interface;
using System.Collections.Generic;

namespace GraphLib.Interfaces
{
    /// <summary>
    /// An interface, that provides a property for 
    /// all neighbours around some coordinate
    /// </summary>
    public interface INeighborhood : ICloneable<INeighborhood>
    {
        /// <summary>
        /// <see cref="ICoordinate"/>
        /// around some coordinate
        /// </summary>
        IReadOnlyCollection<ICoordinate> Coordinates { get; }
    }
}
