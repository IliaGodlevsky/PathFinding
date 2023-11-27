using Pathfinding.App.Console.Model.Notes;
using Pathfinding.GraphLib.Core.Interface;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess
{
    internal sealed class GraphPathfindingHistory
    {
        public HashSet<int> Algorithms { get; } = new();

        public Dictionary<int, List<ICoordinate>> Visited { get; } = new();

        public Dictionary<int, List<ICoordinate>> Paths { get; } = new();

        public Dictionary<int, List<ICoordinate>> Obstacles { get; } = new();

        public Dictionary<int, List<ICoordinate>> Ranges { get; } = new();

        public Dictionary<int, List<int>> Costs { get; } = new();

        public Dictionary<int, Statistics> Statistics { get; } = new();
    }
}
