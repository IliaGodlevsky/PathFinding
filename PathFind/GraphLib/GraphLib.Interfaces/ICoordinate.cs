using Common.Interface;

namespace GraphLib.Interfaces
{
    /// <summary>
    /// Makes it possible to have coordinates values
    /// </summary>
    public interface ICoordinate : ICloneable<ICoordinate>
    {
        /// <summary>
        /// An array of coordinates values of the object
        /// </summary>
        int[] CoordinatesValues { get; }
    }
}
