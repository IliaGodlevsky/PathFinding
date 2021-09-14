using Common.Interface;

namespace GraphLib.Interfaces
{
    /// <summary>
    /// An interface, that provides a property for 
    /// all neighbours around some coordinate
    /// </summary>
    public interface INeighboursCoordinates : ICloneable<INeighboursCoordinates>
    {
        /// <summary>
        /// <see cref="ICoordinate"/>
        /// around some coordinate
        /// </summary>
        ICoordinate[] Coordinates { get; }
    }
}
