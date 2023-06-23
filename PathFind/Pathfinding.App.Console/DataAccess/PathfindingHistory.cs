using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess
{
    internal sealed class PathfindingHistory
    {
        public Dictionary<Graph2D<Vertex>, GraphPathfindingHistory> History { get; } = new();
    }
}
