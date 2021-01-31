using GraphLib.Coordinates.Abstractions;
using System.Collections.Generic;

namespace GraphLib.Coordinates.Infrastructure.Factories.Interfaces
{
    public interface ICoordinateFactory
    {
        ICoordinate CreateCoordinate(IEnumerable<int> coordinates);
    }
}
