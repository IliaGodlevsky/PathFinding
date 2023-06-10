using Pathfinding.App.Console.Model.Notes;
using Pathfinding.GraphLib.Core.Interface;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Model
{
    internal interface IUnitOfWork
    {
        IList<Guid> Keys { get; }

        IRepository<IReadOnlyList<ICoordinate>> VisitedRepository { get; }

        IRepository<IReadOnlyList<ICoordinate>> PathRepository { get; }

        IRepository<IReadOnlyList<ICoordinate>> ObstacleRepository { get; }

        IRepository<IReadOnlyList<ICoordinate>> RangeRepository { get; }

        IRepository<IReadOnlyList<int>> CostRepository { get; }

        IRepository<StatisticsNote> StatisticsRepository { get; }
    }
}
