﻿using LiteDB;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Data.LiteDb.Repositories
{
    public sealed class LiteDbSubAlgorithmRepository : ISubAlgorithmRepository
    {
        private readonly ILiteCollection<SubAlgorithm> collection;

        public LiteDbSubAlgorithmRepository(ILiteDatabase db)
        {
            collection = db.GetCollection<SubAlgorithm>(DbTables.SubAlgorithms);
        }

        public async Task<IEnumerable<SubAlgorithm>> CreateAsync(IEnumerable<SubAlgorithm> entities, CancellationToken token = default)
        {
            return await Task.Run(() =>
            {
                collection.InsertBulk(entities);
                return entities;
            }, token);
        }

        public async Task<IEnumerable<SubAlgorithm>> ReadByAlgorithmRunIdAsync(int runId, CancellationToken token = default)
        {
            return await Task.Run(() =>
            {
                return collection.Query()
                .Where(x => x.AlgorithmRunId == runId)
                .OrderBy(x => x.Order)
                .ToEnumerable();
            }, token);
        }
    }
}
