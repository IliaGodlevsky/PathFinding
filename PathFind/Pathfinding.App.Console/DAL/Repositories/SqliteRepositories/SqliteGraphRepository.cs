using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.Entities;
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
            return Insert(graph);
        }

        public bool DeleteGraph(int graphId)
        {
            return Delete(graphId);
        }

        public GraphEntity GetGraph(int graphId)
        {
            return Read(graphId);
        }
    }
}
