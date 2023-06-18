using Pathfinding.App.Console.DataAccess.Models;
using Pathfinding.App.Console.DataAccess.Repos;

namespace Pathfinding.App.Console.DataAccess.UnitOfWorks
{
    internal sealed class UnitOfWork : IUnitOfWork
    {
        public IRepository<GraphModel> GraphRepository { get; } = new Repository<GraphModel>();

        public IRepository<AlgorithmModel> AlgorithmRepository { get; } = new Repository<AlgorithmModel>();

        public IRepository<CostsModel> CostsRepository { get; } = new Repository<CostsModel>();

        public IRepository<CoordinatesModel> VisitedRepository { get; } = new Repository<CoordinatesModel>();

        public IRepository<CoordinatesModel> ObstaclesRepository { get; } = new Repository<CoordinatesModel>();

        public IRepository<CoordinatesModel> RangesRepository { get; } = new Repository<CoordinatesModel>();

        public IRepository<CoordinatesModel> PathsRepository { get; } = new Repository<CoordinatesModel>();

        public IRepository<StatisticsModel> StatisticsRepository { get; } = new Repository<StatisticsModel>();

        public void Commit()
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}
