using LiteDB;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.Entities;

namespace Pathfinding.App.Console.DAL.Repositories.LiteDbRepositories
{
    internal sealed class LiteDbGraphStateRepository : IGraphStateRepository
    {
        private readonly ILiteCollection<GraphStateEntity> collection;

        public LiteDbGraphStateRepository(ILiteDatabase db)
        {
            collection = db.GetCollection<GraphStateEntity>(DbTables.GraphStates);
        }

        public GraphStateEntity GetByRunId(int runId)
        {
            return collection.FindOne(x => x.AlgorithmRunId == runId);
        }

        public GraphStateEntity Insert(GraphStateEntity entity)
        {
            collection.Insert(entity);
            return entity;
        }
    }
}
