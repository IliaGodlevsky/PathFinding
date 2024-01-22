using Dapper;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.Entities;
using System.Collections.Generic;
using System.Data;

namespace Pathfinding.App.Console.DAL.Repositories.SqliteRepositories
{
    internal sealed class SqliteAlgorithmRepository
        : SqliteRepository<AlgorithmEntity>, IAlgorithmsRepository
    {
        public SqliteAlgorithmRepository(IDbConnection connection,
            IDbTransaction transaction) : base(connection, transaction)
        {

        }

        public bool DeleteByGraphId(int graphId)
        {
            string query = $"DELETE FROM {TableName} WHERE {nameof(AlgorithmEntity.GraphId)} = @GraphId";
            connection.Query<AlgorithmEntity>(query, new { GraphId = graphId }, transaction);
            return true;
        }

        public IEnumerable<AlgorithmEntity> GetByGraphId(int graphId)
        {
            var query = $"SELECT * FROM {TableName} WHERE {nameof(AlgorithmEntity.GraphId)} = @GraphId";
            return connection.Query<AlgorithmEntity>(query, new { GraphId = graphId }, transaction);
        }
    }
}
