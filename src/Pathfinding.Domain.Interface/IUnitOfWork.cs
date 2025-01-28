using Pathfinding.Domain.Interface.Repositories;

namespace Pathfinding.Domain.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IGraphParametresRepository GraphRepository { get; }

        IVerticesRepository VerticesRepository { get; }

        IRangeRepository RangeRepository { get; }

        IStatisticsRepository StatisticsRepository { get; }

        void BeginTransaction();

        void Rollback();

        void Commit();

        Task RollbackAsync(CancellationToken token = default);

        Task CommitAsync(CancellationToken token = default);
    }
}
