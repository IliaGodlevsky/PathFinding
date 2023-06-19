using JsonFlatFileDataStore;
using Pathfinding.App.Console.DataAccess.Models;
using Pathfinding.App.Console.DataAccess.Repos;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Interface;

namespace Pathfinding.App.Console.DataAccess.UnitOfWorks
{
    internal sealed class JsonUnitOfWork : IUnitOfWork
    {
        private readonly DataStore storage;

        public IRepository<GraphModel> GraphRepository { get; }

        public IRepository<AlgorithmModel> AlgorithmRepository { get; }

        public IRepository<CostsModel> CostsRepository { get; }

        public IRepository<CoordinatesModel> VisitedRepository { get; }

        public IRepository<CoordinatesModel> ObstaclesRepository { get; }

        public IRepository<CoordinatesModel> RangesRepository { get; }

        public IRepository<CoordinatesModel> PathsRepository { get; }

        public IRepository<StatisticsModel> StatisticsRepository { get; }

        public IRepository<GraphInformationModel> InformationRepository { get; }

        public JsonUnitOfWork(IVertexFactory<Vertex> vertexFactory, 
            IGraphFactory<Graph2D<Vertex>, Vertex> graphFactory,
            ICoordinateFactory coordinateFactory,
            DataStore storage) 
        {
            this.storage = storage;
            GraphRepository = new GraphJsonRepository(storage,
                coordinateFactory, vertexFactory, graphFactory);
            AlgorithmRepository = new AlgorithmsJsonRepository(storage);
            CostsRepository = new CostsJsonRepository(storage);
            VisitedRepository = new CoordinatesJsonRepository(storage, "visited", coordinateFactory);
            ObstaclesRepository = new CoordinatesJsonRepository(storage,"obstacles", coordinateFactory);
            RangesRepository = new CoordinatesJsonRepository(storage, "ranges", coordinateFactory);
            PathsRepository = new CoordinatesJsonRepository(storage, "paths", coordinateFactory);
            StatisticsRepository = new StatisticsJsonRepository(storage);
            InformationRepository = new GraphInformationJsonRepository(storage);
        }

        public void Commit()
        {
            
        }

        public void Dispose()
        {
            storage.Dispose();
        }
    }
}
