using Common.Interface;

namespace GraphLib.Interfaces
{
    public interface ICoordinate : ICloneable<ICoordinate>
    {
        int[] CoordinatesValues { get; }
    }
}
