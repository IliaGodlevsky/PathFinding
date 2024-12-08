using Pathfinding.Shared.Primitives;
using System;
using System.Collections.Generic;

namespace Pathfinding.ConsoleApp.Messages.ViewModel
{
    internal sealed class QueryPathfindingRangeMessage
    {
        public IReadOnlyCollection<Coordinate> PathfindingRange { get; set; }
            = Array.Empty<Coordinate>();
    }
}
