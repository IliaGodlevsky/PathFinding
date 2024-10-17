using Pathfinding.ConsoleApp.Model;
using System;
using System.Collections.Generic;

namespace Pathfinding.ConsoleApp.Messages.ViewModel
{
    internal sealed class QueryPathfindingRangeMessage
    {
        public IReadOnlyCollection<GraphVertexModel> PathfindingRange { get; set; } = Array.Empty<GraphVertexModel>();
    }
}
