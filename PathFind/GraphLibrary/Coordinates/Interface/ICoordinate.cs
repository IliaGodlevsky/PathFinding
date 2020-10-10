using System.Collections.Generic;

namespace GraphLibrary.Coordinates.Interface
{
    public interface ICoordinate
    {
        IEnumerable<int> Coordinates { get; }
        IEnumerable<ICoordinate> Environment { get; }
    }
}
