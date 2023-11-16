using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.DataAccess.Entities;
using System;

namespace Pathfinding.App.Console
{
    internal sealed class InMemoryHistory : IHistory
    {
        public IStorage<GraphEntity, int> Graphs { get; } = new GraphInMemoryStorage();

        public IStorage<GraphRangeEntity, int> Ranges { get; } = new GraphRangeInMemoryStorage();

        public IStorage<AlgorithmEntity, Guid> Algorithms { get; } = new AlgorithmInMemoryStorage();

        public IStorage<StatisticEntity, int> Statistics { get; } = new StatisticsInMemoryStorage();

        public IStorage<UsedRangeEntity, int> UsedRange { get; } = new UsedRangeInMemoryStorage();

        public IStorage<PathEntity, int> Paths { get; } = new PathInMemoryStorage();

        public IStorage<CostsEntity, int> Costs { get; } = new CostsInMemoryStorage();

        public IStorage<VisitedVerticesEntity, int> Visited { get; } = new VisitedVerticesInMemoryStorage();

        public IStorage<ObstacleVerticesEntity, int> Obstacles { get; } = new ObstaclesInMemoryStorage();
    }
}
