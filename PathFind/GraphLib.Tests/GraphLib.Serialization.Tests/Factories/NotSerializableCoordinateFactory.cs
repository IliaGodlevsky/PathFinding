using GraphLib.Interfaces;
using GraphLib.Serialization.Tests.Objects;

namespace GraphLib.Serialization.Tests.Factories
{
    internal sealed class NotSerializableCoordinateFactory : ICoordinateFactory
    {
        public ICoordinate CreateCoordinate(int[] coordinates)
        {
            return new NotSerializableCoordinate(coordinates);
        }
    }
}