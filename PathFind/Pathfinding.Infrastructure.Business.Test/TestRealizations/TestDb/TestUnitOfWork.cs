using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Repositories;
using Pathfinding.Infrastructure.Business.Test.TestRealizations.TestDb.Repositories;

namespace Pathfinding.Infrastructure.Business.Test.TestRealizations.TestDb
{
    public sealed class TestUnitOfWork : IUnitOfWork
    {
        public IGraphParametresRepository GraphRepository { get; }

        public ISubAlgorithmRepository SubAlgorithmRepository { get; }

        public IVerticesRepository VerticesRepository { get; }

        public INeighborsRepository NeighborsRepository { get; }

        public IRangeRepository RangeRepository { get; }

        public IStatisticsRepository StatisticsRepository { get; }

        public IGraphStateRepository GraphStateRepository { get; }

        public IAlgorithmRunRepository RunRepository { get; }

        public TestUnitOfWork()
        {
            GraphStateRepository = new TestGraphStateRepository();
            NeighborsRepository = new TestNeighborsRepository();
            VerticesRepository = new TestVerticesRepository();
            RangeRepository = new TestRangeRepository();
            SubAlgorithmRepository = new TestSubAlgorithmRepository();
            StatisticsRepository = new TestStatisicsRepository();
            RunRepository = new TestAlgorithmRunRepository();
            GraphRepository = new TestGraphParametresRepository();
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
