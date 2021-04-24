using GraphLib.Base;

namespace GraphLib.Serialization.Tests.Objects
{
    internal sealed class NotSerializableCoordinate : BaseCoordinate
    {
        public NotSerializableCoordinate(params int[] coordinates) :
            base(coordinates.Length, coordinates)
        {
        }
    }
}