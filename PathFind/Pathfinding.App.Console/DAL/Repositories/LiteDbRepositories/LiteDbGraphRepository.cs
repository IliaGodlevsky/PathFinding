using LiteDB;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.Entities;
using Pathfinding.App.Console.Extensions;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Repositories.LiteDbRepositories
{
    internal sealed class LiteDbGraphRepository : IGraphParametresRepository
    {
        private readonly ILiteCollection<GraphEntity> collection;
        private readonly LiteDbNeighborsRepository neighborsRepository;
        private readonly LiteDbAlgorithmRepository algorithmRepository;
        private readonly LiteDbRangeRepository rangeRepository;
        private readonly LiteDbVerticesRepository verticesRepository;

        public LiteDbGraphRepository(ILiteDatabase database)
        {
            collection = database.GetNamedCollection<GraphEntity>();
            neighborsRepository = new(database);
            algorithmRepository = new(database);
            rangeRepository = new(database);
            verticesRepository = new(database);
            collection.EnsureIndex(x => x.Id);
        }

        public GraphEntity AddGraph(GraphEntity graph)
        {
            collection.Insert(graph);
            return graph;
        }

        public bool DeleteGraph(int graphId)
        {
            collection.Delete(graphId);
            verticesRepository.DeleteVerticesByGraphId(graphId);
            neighborsRepository.DeleteByGraphId(graphId);
            rangeRepository.DeleteByGraphId(graphId);
            algorithmRepository.DeleteByGraphId(graphId);
            return true;
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
