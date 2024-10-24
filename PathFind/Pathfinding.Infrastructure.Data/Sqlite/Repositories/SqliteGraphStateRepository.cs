using Dapper;
using Microsoft.Data.Sqlite;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Data.Sqlite.Repositories
{
    internal sealed class SqliteGraphStateRepository : SqliteRepository, IGraphStateRepository
    {
        protected override string CreateTableScript { get; } 
            = @$"CREATE TABLE IF NOT EXISTS {DbTables.GraphStates} (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                AlgorithmRunId INTEGER NOT NULL,
                Costs BLOB NOT NULL,
                Obstacles BLOB NOT NULL,
                Regulars BLOB NOT NULL,
                Range BLOB NOT NULL,
                FOREIGN KEY (AlgorithmRunId) REFERENCES {DbTables.AlgorithmRuns}(Id) ON DELETE CASCADE
           );";

        public SqliteGraphStateRepository(SqliteConnection connection,
            SqliteTransaction transaction) 
            : base(connection, transaction)
        {
        }

        public async Task<GraphState> CreateAsync(GraphState entity, CancellationToken token = default)
        {
            const string query = @$"
                INSERT INTO {DbTables.GraphStates} (AlgorithmRunId, Costs, Obstacles, Regulars, Range)
                VALUES (@AlgorithmRunId, @Costs, @Obstacles, @Regulars, @Range);
                SELECT last_insert_rowid();";

            var id = await connection.ExecuteScalarAsync<int>(
                new CommandDefinition(query, entity, transaction, cancellationToken: token));
            entity.Id = id;
            return entity;
        }

        public async Task<GraphState> ReadByRunIdAsync(int runId, CancellationToken token = default)
        {
            const string query = $"SELECT * FROM {DbTables.GraphStates} WHERE AlgorithmRunId = @AlgorithmRunId";

            return await connection.QuerySingleOrDefaultAsync<GraphState>(
                new CommandDefinition(query, new { AlgorithmRunId = runId }, transaction, cancellationToken: token));
        }
    }
}
