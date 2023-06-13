using Pathfinding.App.Console.Model.Notes;
using Pathfinding.GraphLib.Core.Interface;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Interface
{
    internal interface IPathfindingHistory
    {
        IList<Guid> Algorithms { get; }

        IHistoryVolume<Guid, IReadOnlyList<ICoordinate>> VisitedVertices { get; }

        IHistoryVolume<Guid, IReadOnlyList<ICoordinate>> PathVertices { get; }

        IHistoryVolume<Guid, IReadOnlyList<ICoordinate>> ObstacleVertices { get; }

        IHistoryVolume<Guid, IReadOnlyList<ICoordinate>> RangeVertices { get; }

        IHistoryVolume<Guid, IReadOnlyList<int>> Costs { get; }

        IHistoryVolume<Guid, StatisticsNote> Statistics { get; }
    }
}
