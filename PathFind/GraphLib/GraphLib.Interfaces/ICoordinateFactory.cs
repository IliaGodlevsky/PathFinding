using System.Collections.Generic;

namespace GraphLib.Interfaces
{
    public interface ICoordinateFactory
    {
        ICoordinate CreateCoordinate(IEnumerable<int> coordinates);
    }
}
