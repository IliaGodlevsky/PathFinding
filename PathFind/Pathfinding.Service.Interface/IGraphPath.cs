using Pathfinding.Shared.Primitives;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface
{
    public interface IGraphPath : IReadOnlyCollection<Coordinate>
    {
        double Cost { get; }
    }
}
