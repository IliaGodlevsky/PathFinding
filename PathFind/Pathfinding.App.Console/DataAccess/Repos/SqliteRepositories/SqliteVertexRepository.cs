using Dapper;
using Pathfinding.App.Console.DataAccess.Entities;
using System.Collections.Generic;
using System.Data;

namespace Pathfinding.App.Console.DataAccess.Repos.SqliteRepositories
{
    internal sealed class SqliteVertexRepository
        : SqliteRepository<VertexEntity>, IVerticesRepository
    {
        public SqliteVertexRepository(IDbConnection connection, 
            IDbTransaction transaction) : base(connection, transaction)
        {
        }

        public IEnumerable<VertexEntity> AddVertices(IEnumerable<VertexEntity> vertices)
        {
            Insert(vertices);
            return vertices;
        }

        public bool DeleteVerticesByGraphId(int graphId)
        {
            string query = $"DELETE FROM {TableName} WHERE {nameof(VertexEntity.GraphId)} = @GraphId";
            var parametres = new { GraphId = graphId };
            connection.Query(query, parametres, transaction);
            return true;
        }

        public VertexEntity GetVertexById(int vertexId)
        {
            string query = $"SELECT * FROM {TableName} WHERE {nameof(VertexEntity.Id)} = @VertexId";
            var parametres = new { VertexId = vertexId };
            return connection.QuerySingle<VertexEntity>(query, parametres, transaction);
        }

        public IEnumerable<VertexEntity> GetVerticesByGraphId(int graphId)
        {
            string query = $"SELECT * FROM {TableName} WHERE {nameof(VertexEntity.GraphId)} = @GraphId";
            var parametres = new { GraphId = graphId };
            return connection.Query<VertexEntity>(query, parametres, transaction);
        }

        public bool UpdateVertex(VertexEntity vertex)
        {
            return Update(vertex);
        }

        public bool UpdateVertices(IEnumerable<VertexEntity> vertices)
        {
            foreach(var vertex in vertices)
            {
                Update(vertex);
            }

            return true;
        }
    }
}
