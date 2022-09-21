using System.Collections.Generic;

namespace GraphLib.Interfaces.Factories
{
    public interface ICoordinateFactory
    {
        ICoordinate CreateCoordinate(IReadOnlyList<int> coordinates);
    }
}
