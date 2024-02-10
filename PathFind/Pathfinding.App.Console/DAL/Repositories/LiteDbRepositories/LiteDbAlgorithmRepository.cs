using LiteDB;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DAL.Repositories.LiteDbRepositories
{
    internal sealed class LiteDbAlgorithmRepository : IAlgorithmsRepository
    {
        private readonly ILiteCollection<AlgorithmEntity> collection;

        public LiteDbAlgorithmRepository(ILiteDatabase db)
        {
            collection = db.GetCollection<AlgorithmEntity>(DbTables.Algorithms);
            BsonMapper.Global.Entity<AlgorithmEntity>()
                .Id(x => x.Name, autoId: false);
            if (collection.Count() == 0)
            {
                var algorithms = AlgorithmNames.Algorithms
                    .Select(x => new AlgorithmEntity { Name = x });
                collection.Insert(algorithms);
            }
        }

        public IEnumerable<AlgorithmEntity> GetAll()
        {
            return collection.FindAll();
        }
    }
}
