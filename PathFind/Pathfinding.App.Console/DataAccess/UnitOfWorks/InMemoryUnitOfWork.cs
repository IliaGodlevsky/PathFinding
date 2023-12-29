using Pathfinding.App.Console.DataAccess.Entities;
using Pathfinding.App.Console.DataAccess.Repos;
using Pathfinding.App.Console.DataAccess.Repos.InMemoryRepositories;

namespace Pathfinding.App.Console.DataAccess.UnitOfWorks
{
    internal sealed class InMemoryUnitOfWork : IUnitOfWork
    {
        public IGraphParametresRepository GraphRepository { get; }

        public IAlgorithmsRepository AlgorithmsRepository { get; }

        public IVerticesRepository VerticesRepository { get; }

        public IRangeRepository RangeRepository { get; }

        public INeighborsRepository NeighborsRepository { get; }

        public InMemoryUnitOfWork()
        {
            GraphRepository = new InMemoryGraphRepository();
            AlgorithmsRepository = new InMemoryAlgorithmRepository();
            var vertexRepository = new InMemoryVertexRepository();
            VerticesRepository = vertexRepository;
            NeighborsRepository = new InMemoryNeighborsRepository(vertexRepository.Repos);
            RangeRepository = new InMemoryRangeRepository();
        }

        public void BeginTransaction()
        {
            
        }

        public void CommitTransaction()
        {
            
        }

        public void Dispose()
        {
            
        }

        public void RollbackTransaction()
        {
            
        }

        public void SaveChanges()
        {
            
        }
    }
}
