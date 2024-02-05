using LiteDB;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.Entities;

namespace Pathfinding.App.Console.DAL.Repositories.LiteDbRepositories
{
    internal sealed class LiteDbSubAlgorithmRepository : ISubAlgorithmRepository
    {
        private readonly ILiteCollection<SubAlgorithmEntity> collection;

        public LiteDbSubAlgorithmRepository(ILiteDatabase db)
        {
            collection = db.GetCollection<SubAlgorithmEntity>(DbTables.SubAlgorithms);
            collection.EnsureIndex(x => x.AlgorithmId);
        }
    }
}
