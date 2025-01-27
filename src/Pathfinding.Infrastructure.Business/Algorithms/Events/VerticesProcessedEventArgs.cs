using Pathfinding.Service.Interface;
using Pathfinding.Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Infrastructure.Business.Algorithms.Events
{
    public class VerticesProcessedEventArgs : EventArgs
    {
        public Coordinate Current { get; }

        public IReadOnlyList<Coordinate> Enqueued { get; }

        public VerticesProcessedEventArgs(IPathfindingVertex current,
            IEnumerable<IPathfindingVertex> enqueued)
        {
            Current = current.Position;
            Enqueued = enqueued.Select(x => x.Position)
                .ToList()
                .AsReadOnly();
        }
    }
}
