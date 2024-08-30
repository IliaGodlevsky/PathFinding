using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Repositories;
using Pathfinding.Infrastructure.Data.InMemory.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Data.InMemory
{
    public sealed class InMemoryUnitOfWork : IUnitOfWork
    {
        public IGraphParametresRepository GraphRepository { get; }

        public IAlgorithmsRepository AlgorithmsRepository { get; }

        public ISubAlgorithmRepository SubAlgorithmRepository { get; }

        public IVerticesRepository VerticesRepository { get; }

        public INeighborsRepository NeighborsRepository { get; }

        public IRangeRepository RangeRepository { get; }

        public IStatisticsRepository StatisticsRepository { get; }

        public IGraphStateRepository GraphStateRepository { get; }

        public IAlgorithmRunRepository RunRepository { get; }

        public InMemoryUnitOfWork()
        {
            GraphRepository = new InMemoryGraphParametresRepository();
            RunRepository = new InMemoryAlgorithmRunRepository();
            AlgorithmsRepository = new InMemoryAlgorithmsRepository();
            GraphStateRepository = new InMemoryGraphStateRepository();
            NeighborsRepository = new InMemoryNeighborsRepository();
            VerticesRepository = new InMemoryVerticesRepository();
            RangeRepository = new InMemoryRangeRepository();
            SubAlgorithmRepository = new InMemorySubAlgorithmRepository();
            StatisticsRepository = new InMemoryStatisicsRepository();
        }

        public void BeginTransaction()
        {

        }

        public void Commit()
        {

        }

        public async Task CommitAsync(CancellationToken token = default)
        {
            await Task.CompletedTask;
        }

        public void Dispose()
        {

        }

        public void Rollback()
        {

        }

        public async Task RollbackAsync(CancellationToken token = default)
        {
            await Task.CompletedTask;
        }
    }
}
