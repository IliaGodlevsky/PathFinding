using Dapper;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.Entities;
using Shared.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Pathfinding.App.Console.DAL.Repositories.SqliteRepositories
{
    internal sealed class SqliteNeighborsRepository
        : SqliteRepository<NeighborEntity>, INeighborsRepository
    {
        public SqliteNeighborsRepository(IDbConnection connection,
            IDbTransaction transaction) : base(connection, transaction)
        {
        }

        public NeighborEntity AddNeighbour(NeighborEntity neighbour)
        {
            return Insert(neighbour);
        }

        public IEnumerable<NeighborEntity> AddNeighbours(IEnumerable<NeighborEntity> neighbours)
        {
            return Insert(neighbours);
        }

        public bool DeleteByGraphId(int graphId)
        {
            var subQuery = $"SELECT {nameof(VertexEntity.Id)} FROM {DbTables.Vertices} WHERE {nameof(VertexEntity.GraphId)} = @GraphId";
            var query = $"DELETE FROM {TableName} WHERE {nameof(NeighborEntity.VertexId)} IN ({subQuery})";
            var result = connection.Query(query, new { GraphId = graphId }, transaction);
            return true;
        }

        public bool DeleteNeighbour(int vertexId, int neighbourId)
        {
            string query = $"DELETE FROM {TableName} WHERE {nameof(NeighborEntity.VertexId)} = @VertexId " +
                $"AND {nameof(NeighborEntity.NeighborId)} = @NeighborId";
            var parametres = new { VertexId = vertexId, NeighborId = neighbourId };
            connection.Query<AlgorithmEntity>(query, parametres, transaction);
            return true;
        }

        public IReadOnlyDictionary<int, IReadOnlyCollection<NeighborEntity>> GetNeighboursForVertices(IEnumerable<int> verticesIds)
        {
            var ids = string.Join(", ", verticesIds);
            var query = $"SELECT * FROM {TableName} WHERE {nameof(NeighborEntity.VertexId)} IN ({ids})";
            var neighbours = connection.Query<NeighborEntity>(query, transaction: transaction);
            return neighbours
                .GroupBy(x => x.VertexId)
                .ToDictionary(x => x.Key, x => (IReadOnlyCollection<NeighborEntity>)x.ToReadOnly())
                .AsReadOnly();
        }
    }
}
