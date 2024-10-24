using Dapper;
using Microsoft.Data.Sqlite;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Data.Sqlite.Repositories
{
    internal sealed class SqliteSubAlgorithmsRepository : SqliteRepository, ISubAlgorithmRepository
    {
        protected override string CreateTableScript { get; } = @$"
            CREATE TABLE IF NOT EXISTS {DbTables.SubAlgorithms} (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                AlgorithmRunId INTEGER NOT NULL,
                ""Order"" INTEGER NOT NULL,
                Visited BLOB NOT NULL,
                Path BLOB NOT NULL,
                FOREIGN KEY (AlgorithmRunId) REFERENCES {DbTables.AlgorithmRuns}(Id) ON DELETE CASCADE
            );";

        public SqliteSubAlgorithmsRepository(SqliteConnection connection, 
            SqliteTransaction transaction) : base(connection, transaction)
        {
        }

        public async Task<IEnumerable<SubAlgorithm>> CreateAsync(IEnumerable<SubAlgorithm> entities, CancellationToken token = default)
        {
            const string query = @$"
                INSERT INTO {DbTables.SubAlgorithms} (AlgorithmRunId, ""Order"", Visited, Path)
                VALUES (@AlgorithmRunId, @Order, @Visited, @Path);
                SELECT last_insert_rowid();";

            foreach (var entity in entities)
            {
                var id = await connection.ExecuteScalarAsync<int>(
                    new CommandDefinition(query, entity, transaction, cancellationToken: token));
                entity.Id = id;
            }

            return entities;
        }

        public async Task<IEnumerable<SubAlgorithm>> ReadByAlgorithmRunIdAsync(int runId, CancellationToken token = default)
        {
            const string query = $"SELECT * FROM {DbTables.SubAlgorithms} WHERE AlgorithmRunId = @AlgorithmRunId ORDER BY \"Order\"";

            return await connection.QueryAsync<SubAlgorithm>(
                new CommandDefinition(query, new { AlgorithmRunId = runId }, transaction, cancellationToken: token));
        }
    }
}
