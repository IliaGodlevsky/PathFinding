using System.Collections.Generic;

namespace GraphLib.Interface
{
    public interface ICoordinateFactory
    {
        ICoordinate CreateCoordinate(IEnumerable<int> coordinates);
    }
}
