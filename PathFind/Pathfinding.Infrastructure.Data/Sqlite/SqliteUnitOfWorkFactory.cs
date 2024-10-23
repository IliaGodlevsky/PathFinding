using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Factories;
using SQLitePCL;

namespace Pathfinding.Infrastructure.Data.Sqlite
{
    public sealed class SqliteUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly string connectionString;

        public SqliteUnitOfWorkFactory(string connectionString)
        {
            Batteries.Init();
            this.connectionString = connectionString;
        }

        public IUnitOfWork Create()
        {
            return new SqliteUnitOfWork(connectionString);
        }
    }
}
