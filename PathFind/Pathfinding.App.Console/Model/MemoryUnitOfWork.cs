using Pathfinding.App.Console.Model.Notes;
using Pathfinding.App.Console.Model.Repositories;
using Pathfinding.GraphLib.Core.Interface;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Model
{
    internal sealed class MemoryUnitOfWork : IUnitOfWork
    {
        public IList<Guid> Keys { get; } = new List<Guid>();

        public IRepository<IReadOnlyList<ICoordinate>> VisitedRepository { get; } = new CoordinatesRepository();

        public IRepository<IReadOnlyList<ICoordinate>> PathRepository { get; } = new CoordinatesRepository();

        public IRepository<IReadOnlyList<ICoordinate>> ObstacleRepository { get; } = new CoordinatesRepository();

        public IRepository<IReadOnlyList<ICoordinate>> RangeRepository { get; } = new CoordinatesRepository();

        public IRepository<IReadOnlyList<int>> CostRepository { get; } = new CostRepository();

        public IRepository<StatisticsNote> StatisticsRepository { get; } = new StatisticsRepository();
    }
}
