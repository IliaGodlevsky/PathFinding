using Dapper;
using Microsoft.Data.Sqlite;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Data.Sqlite.Repositories
{
    internal sealed class SqliteStatisticsRepository : SqliteRepository, IStatisticsRepository
    {
        protected override string CreateTableScript { get; } = $@"
            CREATE TABLE IF NOT EXISTS {DbTables.Statistics} (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                GraphId INTEGER NOT NULL,
                AlgorithmName TEXT NOT NULL,
                Heuristics TEXT,
                Weight REAL,
                StepRule TEXT,
                ResultStatus TEXT NOT NULL DEFAULT '',
                Elapsed REAL NOT NULL,
                Steps INTEGER NOT NULL,
                Cost REAL NOT NULL,
                Visited INTEGER NOT NULL,
                Spread TEXT,
                FOREIGN KEY (GraphId) REFERENCES {DbTables.Graphs}(Id) ON DELETE CASCADE
            );
            CREATE INDEX IF NOT EXISTS idx_statistics_id ON {DbTables.Statistics}(Id);
            CREATE INDEX IF NOT EXISTS idx_statistics_graphid ON {DbTables.Statistics}(GraphId);";

        public SqliteStatisticsRepository(SqliteConnection connection,
            SqliteTransaction transaction) : base(connection, transaction)
        {
        }

        public async Task<Statistics> CreateAsync(Statistics entity, CancellationToken token = default)
        {
            const string query = @$"
                INSERT INTO {DbTables.Statistics} (GraphId, AlgorithmName, Heuristics, StepRule, ResultStatus, Elapsed, Steps, Cost, Visited, Weight)
                VALUES (@GraphId, @AlgorithmName, @Heuristics, @StepRule, @ResultStatus, @Elapsed, @Steps, @Cost, @Visited, @Weight);
                SELECT last_insert_rowid();";

            var id = await connection.ExecuteScalarAsync<int>(
                new CommandDefinition(query, entity, transaction, cancellationToken: token));

            entity.Id = id;
            return entity;
        }

        public async Task<IEnumerable<Statistics>> CreateAsync(IEnumerable<Statistics> statistics, CancellationToken token = default)
        {
            const string query = @$"
                INSERT INTO {DbTables.Statistics} (GraphId, AlgorithmName, Heuristics, StepRule, ResultStatus, Elapsed, Steps, Cost, Visited, Weight)
                VALUES (@GraphId, @AlgorithmName, @Heuristics, @StepRule, @ResultStatus, @Elapsed, @Steps, @Cost, @Visited, @Weight);
                SELECT last_insert_rowid();";

            foreach (var entity in statistics)
            {
                entity.Id = await connection.ExecuteScalarAsync<int>(
                    new CommandDefinition(query, entity, transaction, cancellationToken: token));
            }

            return statistics;
        }

        public async Task<IEnumerable<Statistics>> ReadByGraphIdAsync(int graphId, CancellationToken token = default)
        {
            const string query = $"SELECT * FROM {DbTables.Statistics} WHERE GraphId = @GraphId";

            return await connection.QueryAsync<Statistics>(
                new CommandDefinition(query, new { GraphId = graphId }, transaction, cancellationToken: token)
            );
        }

        public async Task<int> ReadStatisticsCountAsync(int graphId, CancellationToken token = default)
        {
            const string query = $"SELECT COUNT(*) FROM {DbTables.Statistics} WHERE GraphId = @GraphId";

            return await connection.ExecuteScalarAsync<int>(
                new CommandDefinition(query, new { GraphId = graphId }, transaction, cancellationToken: token)
            );
        }

        public async Task<bool> DeleteByGraphId(int graphId, CancellationToken token = default)
        {
            const string query = $"DELETE FROM {DbTables.Statistics} WHERE GraphId = @GraphId";

            var rowsAffected = await connection.ExecuteAsync(
                new CommandDefinition(query, new { GraphId = graphId }, transaction, cancellationToken: token)
            );

            return rowsAffected > 0;
        }

        public async Task<bool> DeleteByIdsAsync(IEnumerable<int> ids, CancellationToken token = default)
        {
            const string query = $"DELETE FROM {DbTables.Statistics} WHERE Id IN @Ids";

            var rowsAffected = await connection.ExecuteAsync(
                new CommandDefinition(query, new { Ids = ids.ToArray() }, transaction, cancellationToken: token)
            );

            return rowsAffected > 0;
        }

        public async Task<Statistics> ReadByIdAsync(int id, CancellationToken token = default)
        {
            const string query = $"SELECT * FROM {DbTables.Statistics} WHERE Id = @Id";

            var statistics = await connection.QuerySingleOrDefaultAsync<Statistics>(
                new CommandDefinition(query, new { Id = id }, transaction, cancellationToken: token));

            return statistics;
        }
    }
}
