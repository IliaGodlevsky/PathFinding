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
    internal sealed class SqliteRangeRepository : SqliteRepository, IRangeRepository
    {
        protected override string CreateTableScript { get; } = @$"
            CREATE TABLE IF NOT EXISTS {DbTables.Ranges} (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                IsSource BOOLEAN NOT NULL,
                IsTarget BOOLEAN NOT NULL,
                GraphId INTEGER NOT NULL,
                VertexId INTEGER NOT NULL,
                ""Order"" INTEGER NOT NULL,
                FOREIGN KEY (GraphId) REFERENCES {DbTables.Graphs}(Id) ON DELETE CASCADE
            );
            CREATE INDEX IF NOT EXISTS idx_range_vertexid ON {DbTables.Ranges}(VertexId);
            CREATE INDEX IF NOT EXISTS idx_range_graphid ON {DbTables.Ranges}(GraphId);";

        public SqliteRangeRepository(SqliteConnection connection,
            SqliteTransaction transaction)
            : base(connection, transaction)
        {
        }

        public async Task<IEnumerable<PathfindingRange>> CreateAsync(IEnumerable<PathfindingRange> entities, CancellationToken token = default)
        {
            const string query = @$"
                INSERT INTO {DbTables.Ranges} (IsSource, IsTarget, GraphId, VertexId, ""Order"")
                VALUES (@IsSource, @IsTarget, @GraphId, @VertexId, @Order);
                SELECT last_insert_rowid();";

            foreach (var entity in entities)
            {
                var id = await connection.ExecuteScalarAsync<int>(
                    new CommandDefinition(query, entity, transaction, cancellationToken: token))
                    .ConfigureAwait(false);
                entity.Id = id;
            }

            return entities;
        }

        public async Task<bool> DeleteByGraphIdAsync(int graphId, CancellationToken token = default)
        {
            const string query = $"DELETE FROM {DbTables.Ranges} WHERE GraphId = @GraphId";

            var affectedRows = await connection.ExecuteAsync(
                new CommandDefinition(query, new { GraphId = graphId }, transaction, cancellationToken: token))
                .ConfigureAwait(false);

            return affectedRows > 0;
        }

        public async Task<bool> DeleteByVerticesIdsAsync(IEnumerable<int> verticesIds, CancellationToken token = default)
        {
            const string query = $"DELETE FROM {DbTables.Ranges} WHERE VertexId IN @VerticesIds";

            var affectedRows = await connection.ExecuteAsync(
                new CommandDefinition(query, new { VerticesIds = verticesIds.ToArray() }, transaction, cancellationToken: token))
                .ConfigureAwait(false);

            return affectedRows > 0;
        }

        public async Task<IEnumerable<PathfindingRange>> ReadByGraphIdAsync(int graphId, CancellationToken token = default)
        {
            const string query = $"SELECT * FROM {DbTables.Ranges} WHERE GraphId = @GraphId ORDER BY \"Order\"";

            return await connection.QueryAsync<PathfindingRange>(
                new CommandDefinition(query, new { GraphId = graphId }, transaction, cancellationToken: token))
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<PathfindingRange>> UpsertAsync(IEnumerable<PathfindingRange> entities, CancellationToken token = default)
        {
            const string updateQuery = @$"
                UPDATE {DbTables.Ranges}
                SET IsSource = @IsSource,
                    IsTarget = @IsTarget,
                    GraphId = @GraphId,
                    VertexId = @VertexId,
                    ""Order"" = @Order
                WHERE Id = @Id";

            await connection.ExecuteAsync(
                new CommandDefinition(updateQuery, entities.Where(e => e.Id > 0).ToArray(), transaction, cancellationToken: token))
                .ConfigureAwait(false);

            const string insertQuery = @$"
                INSERT INTO {DbTables.Ranges} 
                    (IsSource, IsTarget, GraphId, VertexId, ""Order"")
                    VALUES (@IsSource, @IsTarget, @GraphId, @VertexId, @Order); 
                    SELECT last_insert_rowid();";

            foreach (var entity in entities.Where(e => e.Id == 0))
            {
                var newId = await connection.ExecuteScalarAsync<int>(
                    new CommandDefinition(insertQuery, entity, transaction, cancellationToken: token))
                    .ConfigureAwait(false);

                entity.Id = newId;
            }
            return entities;
        }
    }
}
