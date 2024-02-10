using LiteDB;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.Entities;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Repositories.LiteDbRepositories
{
    internal sealed class LiteDbGraphRepository : IGraphParametresRepository
    {
        private readonly ILiteCollection<GraphEntity> collection;
        private readonly LiteDbNeighborsRepository neighborsRepository;
        private readonly LiteDbAlgorithmRunRepository algorithmRunRepository;
        private readonly LiteDbRangeRepository rangeRepository;
        private readonly LiteDbVerticesRepository verticesRepository;

        public LiteDbGraphRepository(ILiteDatabase database)
        {
            collection = database.GetCollection<GraphEntity>(DbTables.Graphs);
            neighborsRepository = new(database);
            algorithmRunRepository = new(database);
            rangeRepository = new(database);
            verticesRepository = new(database);
        }

        public GraphEntity Insert(GraphEntity graph)
        {
            collection.Insert(graph);
            return graph;
        }

        public bool Delete(int graphId)
        {
            // Order sensitive. Do not change the order of deleting
            // Reason: some repositories need the presence of values in the database
            neighborsRepository.DeleteByGraphId(graphId);
            rangeRepository.DeleteByGraphId(graphId);
            verticesRepository.DeleteVerticesByGraphId(graphId);
            algorithmRunRepository.DeleteByGraphId(graphId);
            collection.Delete(graphId);
            return true;
        }

        public IEnumerable<GraphEntity> GetAll()
        {
            return collection.FindAll();
        }

        public GraphEntity Read(int graphId)
        {
            return collection.FindById(graphId);
        }

        public bool Update(GraphEntity graph)
        {
            return collection.Update(graph);
        }
    }
}
