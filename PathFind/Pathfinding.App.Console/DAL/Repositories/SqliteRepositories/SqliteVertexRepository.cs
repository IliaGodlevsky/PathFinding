using Dapper;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.Entities;
using Shared.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Pathfinding.App.Console.DAL.Repositories.SqliteRepositories
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
            return Insert(vertices);
        }

        public bool DeleteVerticesByGraphId(int graphId)
        {
            string query = $"DELETE FROM {TableName} WHERE {nameof(VertexEntity.GraphId)} = @GraphId";
            var parametres = new { GraphId = graphId };
            connection.Query(query, parametres, transaction);
            string selectQuery = $"SELECT {Id} FROM {TableName} WHERE {nameof(VertexEntity.GraphId)} = @GraphId";
            var present = connection.Query<VertexEntity>(selectQuery, parametres, transaction).ToReadOnly();
            return present.Count == 0;
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
            foreach (var vertex in vertices)
            {
                Update(vertex);
            }

            return true;
        }
    }
}
