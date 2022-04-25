using System.Collections.Generic;

namespace GraphLib.Interfaces
{
    public interface INeighborhood
    {
        IReadOnlyCollection<ICoordinate> Neighbours { get; }
    }
}
