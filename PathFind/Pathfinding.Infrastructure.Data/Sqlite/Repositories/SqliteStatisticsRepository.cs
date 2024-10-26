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
                AlgorithmRunId INTEGER NOT NULL,
                Heuristics TEXT,
                Weight REAL,
                StepRule TEXT,
                ResultStatus TEXT NOT NULL DEFAULT '',
                Elapsed REAL NOT NULL,
                Steps INTEGER NOT NULL,
                Cost REAL NOT NULL,
                Visited INTEGER NOT NULL,
                Spread TEXT,
                FOREIGN KEY (AlgorithmRunId) REFERENCES {DbTables.AlgorithmRuns}(Id) ON DELETE CASCADE
            );";

        public SqliteStatisticsRepository(SqliteConnection connection, 
            SqliteTransaction transaction) : base(connection, transaction)
        {
        }

        public async Task<Statistics> CreateAsync(Statistics entity, CancellationToken token = default)
        {
            const string query = @$"
                INSERT INTO {DbTables.Statistics} (AlgorithmRunId, Heuristics, StepRule, ResultStatus, Elapsed, Steps, Cost, Visited, Weight)
                VALUES (@AlgorithmRunId, @Heuristics, @StepRule, @ResultStatus, @Elapsed, @Steps, @Cost, @Visited, @Weight);
                SELECT last_insert_rowid();";

            var id = await connection.ExecuteScalarAsync<int>(
                new CommandDefinition(query, entity, transaction, cancellationToken: token));

            entity.Id = id;
            return entity;
        }

        public async Task<Statistics> ReadByAlgorithmRunIdAsync(int runId, CancellationToken token = default)
        {
            const string query = $"SELECT * FROM {DbTables.Statistics} WHERE AlgorithmRunId = @AlgorithmRunId";

            var statistics = await connection.QuerySingleOrDefaultAsync<Statistics>(
                new CommandDefinition(query, new { AlgorithmRunId = runId }, transaction, cancellationToken: token));

            return statistics;
        }

        public async Task<IEnumerable<Statistics>> ReadByRunIdsAsync(IEnumerable<int> runIds, CancellationToken token = default)
        {
            const string query = $"SELECT * FROM {DbTables.Statistics} WHERE AlgorithmRunId IN @RunIds";

            var statisticsList = await connection.QueryAsync<Statistics>(
                new CommandDefinition(query, new { RunIds = runIds.ToArray() }, transaction, cancellationToken: token)
            );

            return statisticsList;
        }
    }
}
