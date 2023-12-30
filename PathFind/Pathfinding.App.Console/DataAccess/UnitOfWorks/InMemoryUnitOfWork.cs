using Pathfinding.App.Console.DataAccess.Entities;
using Pathfinding.App.Console.DataAccess.Repos;
using Pathfinding.App.Console.DataAccess.Repos.InMemoryRepositories;
using Shared.Extensions;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess.UnitOfWorks
{
    internal sealed class InMemoryUnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<int, GraphEntity> graphsSnapshot = new();
        private readonly Dictionary<int, AlgorithmEntity> algorithmsSnapshot = new();
        private readonly Dictionary<int, VertexEntity> verticesSnapshot = new();
        private readonly Dictionary<int, RangeEntity> rangesSnapshot = new();
        private readonly Dictionary<int, NeighborEntity> neighborsSnapshot = new();

        IGraphParametresRepository IUnitOfWork.GraphRepository => GraphRepository;

        IAlgorithmsRepository IUnitOfWork.AlgorithmsRepository => AlgorithmsRepository;

        IVerticesRepository IUnitOfWork.VerticesRepository => VerticesRepository;

        IRangeRepository IUnitOfWork.RangeRepository => RangeRepository;

        INeighborsRepository IUnitOfWork.NeighborsRepository => NeighborsRepository;

        public InMemoryGraphRepository GraphRepository { get; }

        public InMemoryAlgorithmRepository AlgorithmsRepository { get; }

        public InMemoryVertexRepository VerticesRepository { get; }

        public InMemoryRangeRepository RangeRepository { get; }

        public InMemoryNeighborsRepository NeighborsRepository { get; }

        public InMemoryUnitOfWork()
        {
            GraphRepository = new InMemoryGraphRepository();
            AlgorithmsRepository = new InMemoryAlgorithmRepository();
            var vertexRepository = new InMemoryVertexRepository();
            VerticesRepository = vertexRepository;
            NeighborsRepository = new InMemoryNeighborsRepository(vertexRepository.Repository);
            RangeRepository = new InMemoryRangeRepository();
        }

        public void BeginTransaction()
        {
            graphsSnapshot.AddRange(GraphRepository.GetSnapshot());
            algorithmsSnapshot.AddRange(AlgorithmsRepository.GetSnapshot());
            verticesSnapshot.AddRange(VerticesRepository.GetSnapshot());
            rangesSnapshot.AddRange(RangeRepository.GetSnapshot());
            neighborsSnapshot.AddRange(NeighborsRepository.GetSnapshot());
        }

        public void CommitTransaction()
        {
            graphsSnapshot.Clear();
            algorithmsSnapshot.Clear();
            verticesSnapshot.Clear();
            rangesSnapshot.Clear();
            neighborsSnapshot.Clear();
        }

        public void Dispose()
        {
        }

        public void RollbackTransaction()
        {
            GraphRepository.Repository.Clear();
            GraphRepository.Repository.AddRange(graphsSnapshot);
            AlgorithmsRepository.Repository.Clear();
            AlgorithmsRepository.Repository.AddRange(algorithmsSnapshot);
            VerticesRepository.Repository.Clear();
            VerticesRepository.Repository.AddRange(verticesSnapshot);
            RangeRepository.Repository.Clear();
            RangeRepository.Repository.AddRange(rangesSnapshot);
            NeighborsRepository.Repository.Clear();
            NeighborsRepository.Repository.AddRange(neighborsSnapshot);
        }

        public void SaveChanges()
        {
            
        }
    }
}
