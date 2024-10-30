using Dapper;
using Microsoft.Data.Sqlite;

namespace Pathfinding.Infrastructure.Data.Sqlite.Repositories
{
    internal abstract class SqliteRepository
    {
        protected readonly SqliteConnection connection;
        protected readonly SqliteTransaction transaction;

        protected abstract string CreateTableScript { get; }

        protected SqliteRepository(SqliteConnection connection,
            SqliteTransaction transaction)
        {
            this.connection = connection;
            this.transaction = transaction;
            connection.Execute(CreateTableScript);
        }
    }
}
