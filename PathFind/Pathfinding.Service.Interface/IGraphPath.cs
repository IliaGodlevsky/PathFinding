using Pathfinding.Domain.Interface;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface
{
    public interface IGraphPath : IReadOnlyCollection<ICoordinate>
    {
        double Cost { get; }
    }
}
