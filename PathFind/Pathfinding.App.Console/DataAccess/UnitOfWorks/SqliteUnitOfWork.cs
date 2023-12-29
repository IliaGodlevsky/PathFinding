using Microsoft.Data.Sqlite;
using Pathfinding.App.Console.DataAccess.Entities;
using Pathfinding.App.Console.DataAccess.Repos;
using Pathfinding.App.Console.DataAccess.Repos.SqliteRepositories;
using Pathfinding.App.Console.Settings;
using System;
using System.Data;
using System.IO;

namespace Pathfinding.App.Console.DataAccess.UnitOfWorks
{
    internal sealed class SqliteUnitOfWork : IUnitOfWork
    {
        private readonly SqliteConnection connection;
        private IDbTransaction transaction;

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

        public SqliteUnitOfWork()
        {
            connection = new SqliteConnection(GetConnectionString());
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

        private static string GetConnectionString()
        {
            string connectionString = Parametres.Default.SqliteConnectionString;
            string fullPath = Path.Combine(Environment.CurrentDirectory, connectionString);
            return $"Data Source={fullPath};";
        }
    }
}
