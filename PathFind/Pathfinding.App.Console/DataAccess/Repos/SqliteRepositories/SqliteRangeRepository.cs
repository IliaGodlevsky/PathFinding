using Dapper;
using Pathfinding.App.Console.DataAccess.Entities;
using System.Collections.Generic;
using System.Data;

namespace Pathfinding.App.Console.DataAccess.Repos.SqliteRepositories
{
    internal sealed class SqliteRangeRepository
        : SqliteRepository<RangeEntity>, IRangeRepository
    {
        public SqliteRangeRepository(IDbConnection connection, 
            IDbTransaction transaction) : base(connection, transaction)
        {
        }

        public RangeEntity AddRange(RangeEntity entity)
        {
            Insert(entity);
            return entity;
        }

        public IEnumerable<RangeEntity> AddRange(IEnumerable<RangeEntity> entities)
        {
            Insert(entities);
            return entities;
        }

        public bool DeleteByGraphId(int graphId)
        {
            string query = $"DELETE FROM {TableName} WHERE {nameof(RangeEntity.GraphId)} = @GraphId";
            var parametres = new { GraphId = graphId };
            connection.Query<RangeEntity>(query, parametres, transaction);
            return true;
        }

        public bool DeleteByVertexId(int vertexId)
        {
            string query = $"DELETE FROM {TableName} WHERE {nameof(RangeEntity.VertexId)} = @VertexId";
            var parametres = new { VertexId = vertexId };
            connection.Query<RangeEntity>(query, parametres, transaction);
            return true;
        }

        public IEnumerable<RangeEntity> GetByGraphId(int graphId)
        {
            string query = $"SELECT * FROM {TableName} WHERE {nameof(RangeEntity.GraphId)} = @GraphId";
            var parametres = new { GraphId = graphId };
            return connection.Query<RangeEntity>(query, parametres, transaction: transaction);
        }

        public RangeEntity GetByVertexId(int vertexId)
        {
            string query = $"SELECT * FROM {TableName} WHERE {nameof(RangeEntity.VertexId)} = @VertexId";
            var parametres = new { VertexId = vertexId };
            return connection.QuerySingle<RangeEntity>(query, parametres, transaction);
        }
    }
}
