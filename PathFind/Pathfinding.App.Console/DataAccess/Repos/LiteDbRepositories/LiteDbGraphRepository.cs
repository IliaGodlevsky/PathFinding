using LiteDB;
using Pathfinding.App.Console.DataAccess.Entities;
using Pathfinding.App.Console.Extensions;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess.Repos.LiteDbRepositories
{
    internal sealed class LiteDbGraphRepository : IGraphRepository
    {
        private readonly ILiteCollection<GraphEntity> collection;

        public LiteDbGraphRepository(ILiteDatabase database)
        {
            collection = database.GetNamedCollection<GraphEntity>();
            collection.EnsureIndex(x => x.Id);
        }

        public GraphEntity AddGraph(GraphEntity graph)
        {
            collection.Insert(graph);
            return graph;
        }

        public bool DeleteGraph(int graphId)
        {
            return collection.Delete(graphId);
        }

        public IEnumerable<GraphEntity> GetAll()
        {
            return collection.FindAll();
        }

        public GraphEntity GetGraph(int graphId)
        {
            return collection.FindById(graphId);
        }

        public bool Update(GraphEntity graph)
        {
            return collection.Update(graph);
        }
    }
}
