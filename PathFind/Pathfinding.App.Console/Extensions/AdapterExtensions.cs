using Pathfinding.App.Console.Model;
using Shared.Extensions;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Extensions
{
    internal static class AdapterExtensions
    {
        public static void IncludeInRange(this ConsolePathfindingRangeAdapter adapter, IEnumerable<Vertex> vertices)
        {
            vertices.ForEach(vertex => adapter.IncludeInRange(vertex));
        }

        public static void MarkAsIntermediateToReplace(this ConsolePathfindingRangeAdapter adapter, IEnumerable<Vertex> vertices)
        {
            vertices.ForEach(vertex => adapter.MarkAsIntermediateToReplace(vertex));
        }
    }
}
