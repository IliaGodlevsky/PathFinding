﻿using Dapper;
using Microsoft.Data.Sqlite;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Data.Sqlite.Repositories
{
    internal sealed class SqliteGraphRepository : SqliteRepository, IGraphParametresRepository
    {
        protected override string CreateTableScript { get; } = @$"
            CREATE TABLE IF NOT EXISTS {DbTables.Graphs} (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Neighborhood INTEGER NOT NULL,
                SmoothLevel INTEGER NOT NULL,
                Status INTEGER NOT NULL,
                Dimensions TEXT NOT NULL
            );
            CREATE INDEX IF NOT EXISTS idx_graph_id ON {DbTables.Graphs}(Id);";

        public SqliteGraphRepository(SqliteConnection connection,
            SqliteTransaction transaction)
            : base(connection, transaction)
        {
        }

        public async Task<Graph> CreateAsync(Graph graph, CancellationToken token = default)
        {
            const string query = @$"
                INSERT INTO {DbTables.Graphs} (Name, Neighborhood, SmoothLevel, Status, Dimensions)
                VALUES (@Name, @Neighborhood, @SmoothLevel, @Status, @Dimensions);
                SELECT last_insert_rowid();";

            var id = await connection.ExecuteScalarAsync<int>(
                new CommandDefinition(query, graph, transaction, cancellationToken: token))
                .ConfigureAwait(false);
            graph.Id = id;
            return graph;
        }

        public async Task<bool> DeleteAsync(int graphId, CancellationToken token = default)
        {
            const string query = $"DELETE FROM {DbTables.Graphs} WHERE Id = @Id";

            var affectedRows = await connection.ExecuteAsync(
                new CommandDefinition(query, new { Id = graphId }, transaction, cancellationToken: token))
                .ConfigureAwait(false);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(IEnumerable<int> graphIds, CancellationToken token = default)
        {
            const string query = $"DELETE FROM {DbTables.Graphs} WHERE Id IN @Ids";

            var affectedRows = await connection.ExecuteAsync(
                new CommandDefinition(query, new { Ids = graphIds.ToArray() }, transaction, cancellationToken: token))
                .ConfigureAwait(false);
            return affectedRows > 0;
        }

        public async Task<IEnumerable<Graph>> GetAll(CancellationToken token = default)
        {
            const string query = $"SELECT * FROM {DbTables.Graphs}";

            return await connection.QueryAsync<Graph>(
                new CommandDefinition(query, transaction: transaction, cancellationToken: token))
                .ConfigureAwait(false);
        }

        public async Task<Graph> ReadAsync(int graphId, CancellationToken token = default)
        {
            const string query = $"SELECT * FROM {DbTables.Graphs} WHERE Id = @Id";

            return await connection.QuerySingleOrDefaultAsync<Graph>(
                new CommandDefinition(query, new { Id = graphId }, transaction, cancellationToken: token))
                .ConfigureAwait(false);
        }

        public async Task<bool> UpdateAsync(Graph graph, CancellationToken token = default)
        {
            const string query = @$"
                UPDATE {DbTables.Graphs}
                SET Name = @Name,
                    Neighborhood = @Neighborhood,
                    SmoothLevel = @SmoothLevel,
                    Status = @Status,
                    Dimensions = @Dimensions
                WHERE Id = @Id";

            var affectedRows = await connection.ExecuteAsync(
                new CommandDefinition(query, graph, transaction, cancellationToken: token))
                .ConfigureAwait(false);

            return affectedRows > 0;
        }

        public async Task<IReadOnlyDictionary<int, int>> ReadObstaclesCountAsync(IEnumerable<int> graphIds, CancellationToken token = default)
        {
            const string query = $@"
                SELECT GraphId, COUNT(*) AS ObstacleCount 
                FROM {DbTables.Vertices}
                WHERE GraphId IN @GraphIds AND IsObstacle = 1
                GROUP BY GraphId";

            var result = await connection.QueryAsync<(int GraphId, int ObstacleCount)>(
                new CommandDefinition(query, new { GraphIds = graphIds.ToArray() }, transaction, cancellationToken: token))
                .ConfigureAwait(false);

            return result.ToDictionary(x => x.GraphId, x => x.ObstacleCount);
        }
    }
}
