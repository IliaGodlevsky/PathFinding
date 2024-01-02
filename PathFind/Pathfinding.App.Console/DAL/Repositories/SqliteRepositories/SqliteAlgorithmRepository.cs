using Dapper;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Pathfinding.App.Console.DAL.Repositories.SqliteRepositories
{
    internal sealed class SqliteAlgorithmRepository
        : SqliteRepository<AlgorithmEntity>, IAlgorithmsRepository
    {
        public SqliteAlgorithmRepository(IDbConnection connection,
            IDbTransaction transaction) : base(connection, transaction)
        {

        }

        public int AddMany(IEnumerable<AlgorithmEntity> entity)
        {
            var array = entity.ToArray();
            Insert(entity);
            return array.Length;
        }

        public void AddOne(AlgorithmEntity entity)
        {
            Insert(entity);
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
