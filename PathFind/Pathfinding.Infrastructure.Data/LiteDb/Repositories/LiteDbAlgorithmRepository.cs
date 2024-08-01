using LiteDB;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Data.LiteDb.Repositories
{
    public sealed class LiteDbAlgorithmsRepository : IAlgorithmsRepository
    {
        private readonly ILiteCollection<Algorithm> collection;

        public LiteDbAlgorithmsRepository(ILiteDatabase db)
        {
            collection = db.GetCollection<Algorithm>(DbTables.Algorithms);
            if (collection.Count() == 0)
            {
                BsonMapper.Global.Entity<Algorithm>().Id(x => x.Name, autoId: false);
                collection.Insert(AlgorithmNames.All.Select(x => new Algorithm { Name = x }));
            }
        }

        public async Task<IEnumerable<Algorithm>> GetAllAsync(CancellationToken token = default)
        {
            return await Task.Run(() => collection.FindAll(), token);
        }
    }
}
