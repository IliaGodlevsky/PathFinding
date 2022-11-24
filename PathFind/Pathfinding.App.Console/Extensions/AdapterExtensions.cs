using Pathfinding.App.Console.Model;
using Shared.Extensions;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Extensions
{
    internal static class AdapterExtensions
    {
        public static void IncludeInRange(this ConsolePathfindingRange range, IEnumerable<Vertex> vertices)
        {
            vertices.ForEach(vertex => range.IncludeInPathfindingRange(vertex));
        }

        public static void MarkAsIntermediateToReplace(this ConsolePathfindingRange adapter, IEnumerable<Vertex> vertices)
        {
            vertices.ForEach(vertex => adapter.MarkAsIntermediateToReplace(vertex));
        }
    }
}
