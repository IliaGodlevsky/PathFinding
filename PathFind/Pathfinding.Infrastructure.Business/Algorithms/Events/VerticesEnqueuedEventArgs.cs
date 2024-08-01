using Pathfinding.Domain.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Infrastructure.Business.Algorithms.Events
{
    public class VerticesEnqueuedEventArgs : PathfindingEventArgs
    {
        public IReadOnlyList<ICoordinate> Enqueued { get; }

        public VerticesEnqueuedEventArgs(IVertex current, IEnumerable<IVertex> enqueued)
            : base(current)
        {
            Enqueued = enqueued.Select(x => x.Position)
                .ToList()
                .AsReadOnly();
        }
    }
}
