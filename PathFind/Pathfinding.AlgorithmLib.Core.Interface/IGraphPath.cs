using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Core.Interface
{
    public interface IGraphPath : IReadOnlyCollection<ICoordinate>
    {
        double Cost { get; }
    }
}
