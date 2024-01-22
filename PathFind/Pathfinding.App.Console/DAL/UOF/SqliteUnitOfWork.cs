using Microsoft.Data.Sqlite;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Repositories.SqliteRepositories;
using Pathfinding.App.Console.Settings;
using System;
using System.IO;

namespace Pathfinding.App.Console.DAL.UOF
{
    internal sealed class SqliteUnitOfWork : IUnitOfWork
    {
        private readonly SqliteConnection connection;
        private SqliteTransaction transaction;

        public IGraphParametresRepository GraphRepository
            => new SqliteGraphRepository(connection, transaction);

        public IAlgorithmsRepository AlgorithmsRepository
            => new SqliteAlgorithmRepository(connection, transaction);

        public IVerticesRepository VerticesRepository
            => new SqliteVertexRepository(connection, transaction);

        public IRangeRepository RangeRepository
            => new SqliteRangeRepository(connection, transaction);

        public INeighborsRepository NeighborsRepository
            => new SqliteNeighborsRepository(connection, transaction);

        public SqliteUnitOfWork(string connectionString)
        {
            connection = new(connectionString);
            connection.Open();
            transaction = connection.BeginTransaction();
        }

        public void BeginTransaction()
        {
            transaction ??= connection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (transaction is not null)
            {
                transaction.Commit();
                transaction.Dispose();
                transaction = null;
            }
        }

        public void Dispose()
        {
            transaction?.Dispose();
            connection.Dispose();
        }

        public void RollbackTransaction()
        {
            if (transaction is not null)
            {
                transaction.Rollback();
                transaction.Dispose();
                transaction = null;
            }
        }

        public void SaveChanges()
        {

        }
    }
}
