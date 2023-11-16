using Pathfinding.App.Console.DataAccess.Entities;
using System;

namespace Pathfinding.App.Console.DataAccess
{
    internal interface IHistory
    {
        IStorage<GraphEntity, int> Graphs { get; }

        IStorage<GraphRangeEntity, int> Ranges { get; }

        IStorage<AlgorithmEntity, Guid> Algorithms { get; }

        IStorage<StatisticEntity, int> Statistics { get; }

        IStorage<UsedRangeEntity, int> UsedRange { get; }

        IStorage<PathEntity, int> Paths { get; }

        IStorage<CostsEntity, int> Costs { get; }

        IStorage<VisitedVerticesEntity, int> Visited { get; }

        IStorage<ObstacleVerticesEntity, int> Obstacles { get; }
    }
}
