using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Repositories;
using Pathfinding.Infrastructure.Data.InMemory.Repositories;

namespace Pathfinding.Infrastructure.Data.InMemory
{
    public sealed class InMemoryUnitOfWork : IUnitOfWork
    {
        public IGraphParametresRepository GraphRepository { get; }

        public IVerticesRepository VerticesRepository { get; }

        public IRangeRepository RangeRepository { get; }

        public IStatisticsRepository StatisticsRepository { get; }

        public InMemoryUnitOfWork()
        {
            var vertices = new InMemoryVerticesRepository();
            var range = new InMemoryRangeRepository();
            var statistics = new InMemoryStatisicsRepository();
            VerticesRepository = vertices;
            RangeRepository = range;
            StatisticsRepository = statistics;
            GraphRepository = new InMemoryGraphParametresRepository(range, vertices, statistics);
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
