﻿using LiteDB;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Data.LiteDb.Repositories
{
    internal sealed class LiteDbStatisticsRepository : IStatisticsRepository
    {
        private readonly ILiteCollection<Statistics> collection;

        public LiteDbStatisticsRepository(ILiteDatabase db)
        {
            collection = db.GetCollection<Statistics>(DbTables.Statistics);
        }

        public async Task<Statistics> CreateAsync(Statistics entity, CancellationToken token = default)
        {
            collection.Insert(entity);
            return await Task.FromResult(entity);
        }

        public async Task<IEnumerable<Statistics>> CreateAsync(IEnumerable<Statistics> statistics, CancellationToken token = default)
        {
            collection.InsertBulk(statistics);
            return await Task.FromResult(statistics);
        }

        public async Task<bool> DeleteByGraphId(int graphId, CancellationToken token = default)
        {
            var deletedCount = collection.DeleteMany(x => x.GraphId == graphId);
            return await Task.FromResult(deletedCount > 0);
        }

        public async Task<bool> DeleteByIdsAsync(IEnumerable<int> ids, CancellationToken token = default)
        {
            var deletedCount = collection.DeleteMany(x => ids.Contains(x.Id));
            return await Task.FromResult(deletedCount > 0);
        }

        public async Task<IEnumerable<Statistics>> ReadByGraphIdAsync(int graphId, CancellationToken token = default)
        {
            var results = collection.Find(x => x.GraphId == graphId).ToList();
            return await Task.FromResult(results);
        }

        public async Task<Statistics> ReadByIdAsync(int id, CancellationToken token = default)
        {
            var result = collection.FindById(id);
            return await Task.FromResult(result);
        }

        public async Task<int> ReadStatisticsCountAsync(int graphId, CancellationToken token = default)
        {
            var count = collection.Count(x => x.GraphId == graphId);
            return await Task.FromResult(count);
        }
    }
}
