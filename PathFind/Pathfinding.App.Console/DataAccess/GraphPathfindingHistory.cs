using Pathfinding.App.Console.Model.Notes;
using Pathfinding.GraphLib.Core.Interface;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess
{
    internal sealed class GraphPathfindingHistory
    {
        public List<Guid> Algorithms { get; } = new();

        public List<ICoordinate> PathfindingRange { get; set; } = new();

        public Stack<IReadOnlyList<int>> SmoothHistory { get; } = new();

        public Dictionary<Guid, List<ICoordinate>> Visited { get; } = new();

        public Dictionary<Guid, List<ICoordinate>> Paths { get; } = new();

        public Dictionary<Guid, List<ICoordinate>> Obstacles { get; } = new();

        public Dictionary<Guid, List<ICoordinate>> Ranges { get; } = new();

        public Dictionary<Guid, List<int>> Costs { get; } = new();

        public Dictionary<Guid, Statistics> Statistics { get; } = new();
    }
}
