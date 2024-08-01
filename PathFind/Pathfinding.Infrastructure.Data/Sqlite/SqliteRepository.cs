using Microsoft.Data.Sqlite;
using Pathfinding.Domain.Core;

namespace Pathfinding.Infrastructure.Data.Sqlite
{
    internal abstract class SqliteRepository<TEntity, TId>
        where TEntity : class, IEntity<TId>
    {
        protected readonly SqliteConnection connection;

        protected SqliteRepository(SqliteConnection connection)
        {
            this.connection = connection;
        }
    }
}
