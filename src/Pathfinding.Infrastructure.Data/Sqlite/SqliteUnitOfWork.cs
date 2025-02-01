using Microsoft.Data.Sqlite;
using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Repositories;
using Pathfinding.Infrastructure.Data.Sqlite.Repositories;

namespace Pathfinding.Infrastructure.Data.Sqlite
{
    public sealed class SqliteUnitOfWork : IUnitOfWork
    {
        private readonly SqliteConnection connection;
        private SqliteTransaction transaction;

        public IGraphParametresRepository GraphRepository
            => new SqliteGraphRepository(connection, transaction);

        public IVerticesRepository VerticesRepository
            => new SqliteVerticesRepository(connection, transaction);

        public IRangeRepository RangeRepository
            => new SqliteRangeRepository(connection, transaction);

        public IStatisticsRepository StatisticsRepository
            => new SqliteStatisticsRepository(connection, transaction);

        public SqliteUnitOfWork(string connectionString)
        {
            connection = new SqliteConnection(connectionString);
            connection.Open();
            transaction = connection.BeginTransaction();
            _ = new SqliteVerticesRepository(connection, transaction);
        }

        public void BeginTransaction()
        {
            transaction ??= connection.BeginTransaction();
        }

        public void Commit()
        {
            if (transaction is not null)
            {
                transaction.Commit();
                transaction.Dispose();
                transaction = null;
            }
        }

        public async Task CommitAsync(CancellationToken token = default)
        {
            Commit();
            await Task.CompletedTask;
        }

        public void Dispose()
        {
            transaction?.Dispose();
            connection.Dispose();
        }

        public void Rollback()
        {
            if (transaction is not null)
            {
                transaction.Rollback();
                transaction.Dispose();
                transaction = null;
            }
        }

        public async Task RollbackAsync(CancellationToken token = default)
        {
            Rollback();
            await Task.CompletedTask;
        }
    }
}
