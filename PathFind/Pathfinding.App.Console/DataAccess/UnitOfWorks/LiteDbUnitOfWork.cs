using LiteDB;
using Pathfinding.App.Console.DataAccess.Repos;
using Pathfinding.App.Console.DataAccess.Repos.LiteDbRepositories;

namespace Pathfinding.App.Console.DataAccess.UnitOfWorks
{
    internal sealed class LiteDbUnitOfWork : IUnitOfWork
    {
        private readonly ILiteDatabase database;

        public IGraphRepository GraphRepository { get; }

        public IAlgorithmsRepository AlgorithmsRepository { get; }

        public IVerticesRepository VerticesRepository { get; }

        public INeighborsRepository NeighborsRepository { get; }

        public IRangeRepository RangeRepository { get; }

        public LiteDbUnitOfWork()
        {
            database = new LiteDatabase("graph.db");
            GraphRepository = new LiteDbGraphRepository(database);
            AlgorithmsRepository = new LiteDbAlgorithmRepository(database);
            VerticesRepository = new LiteDbVerticesRepository(database);
            NeighborsRepository = new LiteDbNeighborRepository(database);
            RangeRepository = new LiteDbRangeRepository(database);
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
    }
}
