using Pathfinding.App.Console.DataAccess.Models;
using Pathfinding.App.Console.DataAccess.Repos;
using System;

namespace Pathfinding.App.Console.DataAccess.UnitOfWorks
{
    internal interface IUnitOfWork : IDisposable
    {
        IRepository<GraphModel> GraphRepository { get; }

        IRepository<AlgorithmModel> AlgorithmRepository { get; }

        IRepository<CostsModel> CostsRepository { get; }

        IRepository<CoordinatesModel> VisitedRepository { get; }

        IRepository<CoordinatesModel> ObstaclesRepository { get; }

        IRepository<CoordinatesModel> RangesRepository { get; }

        IRepository<CoordinatesModel> PathsRepository { get; }

        IRepository<StatisticsModel> StatisticsRepository { get; }

        void Commit();
    }
}
