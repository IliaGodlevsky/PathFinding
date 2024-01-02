using Dapper;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.Entities;
using System.Collections.Generic;
using System.Data;

namespace Pathfinding.App.Console.DAL.Repositories.SqliteRepositories
{
    internal sealed class SqliteGraphRepository
        : SqliteRepository<GraphEntity>, IGraphParametresRepository
    {
        public SqliteGraphRepository(IDbConnection connection,
            IDbTransaction transaction) : base(connection, transaction)
        {
        }

        public GraphEntity AddGraph(GraphEntity graph)
        {
            Insert(graph);
            return graph;
        }

        public bool DeleteGraph(int graphId)
        {
            string query = $"DELETE FROM {TableName} WHERE {nameof(GraphEntity.Id)} = @Id";
            connection.Query(query, new { Id = graphId }, transaction);
            return true;
        }

        public IEnumerable<GraphEntity> GetAll()
        {
            string query = $"SELECT * FROM {TableName}";
            return connection.Query<GraphEntity>(query, transaction: transaction);
        }

        public GraphEntity GetGraph(int graphId)
        {
            string query = $"SELECT * FROM {TableName} WHERE {nameof(GraphEntity.Id)} = @Id";
            return connection.QuerySingle<GraphEntity>(query, new { Id = graphId }, transaction: transaction);
        }
    }
}
