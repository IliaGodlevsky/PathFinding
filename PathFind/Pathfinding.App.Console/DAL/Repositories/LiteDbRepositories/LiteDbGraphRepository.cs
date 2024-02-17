using LiteDB;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.Entities;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Repositories.LiteDbRepositories
{
    internal sealed class LiteDbGraphRepository(ILiteDatabase database) : IGraphParametresRepository
    {
        private readonly ILiteCollection<GraphEntity> collection = database.GetCollection<GraphEntity>(DbTables.Graphs);
        private readonly LiteDbNeighborsRepository neighborsRepository = new(database);
        private readonly LiteDbAlgorithmRunRepository algorithmRunRepository = new(database);
        private readonly LiteDbRangeRepository rangeRepository = new(database);
        private readonly LiteDbVerticesRepository verticesRepository = new(database);

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
            return collection.Delete(graphId);
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

        public int GetCount()
        {
            return collection.Count();
        }
    }
}
