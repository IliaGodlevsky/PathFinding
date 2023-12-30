using LiteDB;
using Pathfinding.App.Console.DataAccess.Repos;
using Pathfinding.App.Console.DataAccess.Repos.LiteDbRepositories;
using Pathfinding.App.Console.Settings;
using System;
using System.IO;

namespace Pathfinding.App.Console.DataAccess.UnitOfWorks
{
    internal sealed class LiteDbUnitOfWork : IUnitOfWork
    {
        private static readonly string ConnectionString;

        private readonly ILiteDatabase database;

        public IGraphParametresRepository GraphRepository { get; }

        public IAlgorithmsRepository AlgorithmsRepository { get; }

        public IVerticesRepository VerticesRepository { get; }

        public IRangeRepository RangeRepository { get; }

        public INeighborsRepository NeighborsRepository { get; }

        public LiteDbUnitOfWork()
        {
            database = new LiteDatabase(ConnectionString);
            GraphRepository = new LiteDbGraphRepository(database);
            AlgorithmsRepository = new LiteDbAlgorithmRepository(database);
            VerticesRepository = new LiteDbVerticesRepository(database);
            NeighborsRepository = new LiteDbNeighborsRepository(database);
            RangeRepository = new LiteDbRangeRepository(database);
        }

        static LiteDbUnitOfWork()
        {
            ConnectionString = GetConnectionString();
        }

        public void BeginTransaction()
        {
            database.BeginTrans();
        }

        public void CommitTransaction()
        {
            database.Commit();
        }

        public void RollbackTransaction()
        {
            database.Rollback();
        }

        public void SaveChanges()
        {
            
        }

        public void Dispose()
        {
            database.Dispose();
        }

        private static string GetConnectionString()
        {
            string connectionString = Parametres.Default.LiteDbConnectionString;
            return Path.Combine(Environment.CurrentDirectory, connectionString);
        }
    }
}
