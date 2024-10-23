using Dapper;
using Microsoft.Data.Sqlite;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Data.Sqlite.Repositories
{
    internal sealed class SqliteAlgorithmRunRepository : SqliteRepository, IAlgorithmRunRepository
    {
        protected override string CreateTableScript { get; } 
            = @$"CREATE TABLE IF NOT EXISTS {DbTables.AlgorithmRuns} (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                GraphId INTEGER NOT NULL,
                AlgorithmId TEXT NOT NULL,
                FOREIGN KEY (GraphId) REFERENCES {DbTables.Graphs}(Id) ON DELETE CASCADE);";

        public SqliteAlgorithmRunRepository(SqliteConnection connection,
            SqliteTransaction transaction) 
            : base(connection, transaction)
        {
        }

        public async Task<AlgorithmRun> CreateAsync(AlgorithmRun entity, CancellationToken token = default)
        {
            const string query = @$"
                INSERT INTO {DbTables.AlgorithmRuns} (GraphId, AlgorithmId)
                VALUES (@GraphId, @AlgorithmId);
                SELECT last_insert_rowid();";

            var command = new CommandDefinition(query, entity, transaction, cancellationToken: token);
            var id = await connection.ExecuteScalarAsync<int>(command);
            entity.Id = id;
            return entity;
        }

        public async Task<bool> DeleteByGraphIdAsync(int graphId, CancellationToken token = default)
        {
            const string query = $"DELETE FROM {DbTables.AlgorithmRuns} WHERE GraphId = @GraphId";

            var affectedRows = await connection.ExecuteAsync(
                new CommandDefinition(query, new { GraphId = graphId }, transaction, cancellationToken: token));
            return affectedRows > 0;
        }

        public async Task<bool> DeleteByRunIdsAsync(IEnumerable<int> runIds, CancellationToken token = default)
        {
            const string query = $"DELETE FROM {DbTables.AlgorithmRuns} WHERE Id IN @RunIds";

            var affectedRows = await connection.ExecuteAsync(
                new CommandDefinition(query, new { RunIds = runIds }, transaction, cancellationToken: token));
            return affectedRows > 0;
        }

        public async Task<AlgorithmRun> ReadAsync(int id, CancellationToken token = default)
        {
            const string query = $"SELECT * FROM {DbTables.AlgorithmRuns} WHERE Id = @Id";

            return await connection.QuerySingleOrDefaultAsync<AlgorithmRun>(
                new CommandDefinition(query, new { Id = id }, transaction, cancellationToken: token));
        }

        public async Task<IEnumerable<AlgorithmRun>> ReadByGraphIdAsync(int graphId, CancellationToken token = default)
        {
            const string query = $"SELECT * FROM {DbTables.AlgorithmRuns} WHERE GraphId = @GraphId";

            return await connection.QueryAsync<AlgorithmRun>(
                new CommandDefinition(query, new { GraphId = graphId }, transaction, cancellationToken: token));
        }
    }
}
