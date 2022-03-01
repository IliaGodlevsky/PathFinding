using Common.Interface;
using System.Collections.Generic;

namespace GraphLib.Interfaces
{
    public interface INeighborhood : ICloneable<INeighborhood>
    {
        IReadOnlyCollection<ICoordinate> Neighbours { get; }
    }
}
