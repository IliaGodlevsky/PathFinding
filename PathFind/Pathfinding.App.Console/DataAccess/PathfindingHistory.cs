using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model.Notes;
using Pathfinding.App.Console.DataAccess.Repositories;
using Pathfinding.GraphLib.Core.Interface;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess
{
    internal sealed class PathfindingHistory : IPathfindingHistory
    {
        public IList<Guid> Algorithms { get; } = new List<Guid>();

        public IHistoryVolume<Guid, IReadOnlyList<ICoordinate>> VisitedVertices { get; } = new CoordinatesVolume();

        public IHistoryVolume<Guid, IReadOnlyList<ICoordinate>> PathVertices { get; } = new CoordinatesVolume();

        public IHistoryVolume<Guid, IReadOnlyList<ICoordinate>> ObstacleVertices { get; } = new CoordinatesVolume();

        public IHistoryVolume<Guid, IReadOnlyList<ICoordinate>> RangeVertices { get; } = new CoordinatesVolume();

        public IHistoryVolume<Guid, IReadOnlyList<int>> Costs { get; } = new CostsVolume();

        public IHistoryVolume<Guid, Statistics> Statistics { get; } = new StatisticsVolume();
    }
}
