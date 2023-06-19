using Pathfinding.App.Console.DataAccess.Models;
using Pathfinding.App.Console.DataAccess.Repos;

namespace Pathfinding.App.Console.DataAccess.UnitOfWorks
{
    internal sealed class ProxyUnitOfWork : IUnitOfWork
    {
        private readonly IUnitOfWork unitOfWork;

        public IRepository<GraphInformationModel> InformationRepository { get; }

        public IRepository<GraphModel> GraphRepository { get; }

        public IRepository<AlgorithmModel> AlgorithmRepository { get; }

        public IRepository<CostsModel> CostsRepository { get; }

        public IRepository<CoordinatesModel> VisitedRepository { get; }

        public IRepository<CoordinatesModel> ObstaclesRepository { get; }

        public IRepository<CoordinatesModel> RangesRepository { get; }

        public IRepository<CoordinatesModel> PathsRepository { get; }

        public IRepository<StatisticsModel> StatisticsRepository { get; }

        public ProxyUnitOfWork(IUnitOfWork unitOfWork) 
        { 
            this.unitOfWork = unitOfWork;
            InformationRepository = new ProxyRepository<GraphInformationModel>(unitOfWork.InformationRepository);
            GraphRepository = new ProxyRepository<GraphModel>(unitOfWork.GraphRepository);
            AlgorithmRepository = new ProxyRepository<AlgorithmModel>(unitOfWork.AlgorithmRepository);
            CostsRepository = new ProxyRepository<CostsModel>(unitOfWork.CostsRepository);
            VisitedRepository = new ProxyRepository<CoordinatesModel>(unitOfWork.VisitedRepository);
            ObstaclesRepository = new ProxyRepository<CoordinatesModel>(unitOfWork.ObstaclesRepository);
            PathsRepository = new ProxyRepository<CoordinatesModel>(unitOfWork.PathsRepository);
            RangesRepository = new ProxyRepository<CoordinatesModel>(unitOfWork.RangesRepository);
            StatisticsRepository = new ProxyRepository<StatisticsModel>(unitOfWork.StatisticsRepository);

        }

        public void Commit()
        {
           unitOfWork.Commit();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
