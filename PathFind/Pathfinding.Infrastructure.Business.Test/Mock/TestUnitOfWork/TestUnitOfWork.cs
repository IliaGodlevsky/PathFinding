using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Repositories;

namespace Pathfinding.Infrastructure.Business.Test.Mock.TestUnitOfWork
{
    internal sealed class TestUnitOfWork : IUnitOfWork
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

        public TestUnitOfWork()
        {
            GraphRepository = new TestGraphParametresRepository();
            RunRepository = new TestAlgorithmRunRepository();
            AlgorithmsRepository = new TestAlgorithmsRepository();
            GraphStateRepository = new TestGraphStateRepository();
            NeighborsRepository = new TestNeighborsRepository();
            VerticesRepository = new TestVerticesRepository();
            RangeRepository = new TestRangeRepository();
            SubAlgorithmRepository = new TestSubAlgorithmRepository();
            StatisticsRepository = new TestStatisicsRepository();
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
