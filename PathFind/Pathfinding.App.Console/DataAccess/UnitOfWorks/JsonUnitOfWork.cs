using Pathfinding.App.Console.DataAccess.Models;
using Pathfinding.App.Console.DataAccess.Repos;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Interface;

namespace Pathfinding.App.Console.DataAccess.UnitOfWorks
{
    internal sealed class JsonUnitOfWork : IUnitOfWork
    {
        private readonly DataContext context;

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
            DataContext storage) 
        {
            this.context = storage;
            GraphRepository = new GraphJsonRepository(context.Graphs,
                coordinateFactory, vertexFactory, graphFactory);
            AlgorithmRepository = new AlgorithmsJsonRepository(context.Algorithms);
            CostsRepository = new CostsJsonRepository(context.Costs);
            VisitedRepository = new CoordinatesJsonRepository(context.Visited, coordinateFactory);
            ObstaclesRepository = new CoordinatesJsonRepository(context.Obstacles, coordinateFactory);
            RangesRepository = new CoordinatesJsonRepository(context.Ranges, coordinateFactory);
            PathsRepository = new CoordinatesJsonRepository(context.Paths, coordinateFactory);
            StatisticsRepository = new StatisticsJsonRepository(context.Statistics);
            InformationRepository = new GraphInformationJsonRepository(context.Informations);
        }

        public void Commit()
        {
            
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
