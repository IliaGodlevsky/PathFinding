﻿using LiteDB;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Data.LiteDb.Repositories
{
    internal sealed class LiteDbGraphRepository : IGraphParametresRepository
    {
        private readonly ILiteCollection<Graph> collection;
        private readonly LiteDbAlgorithmRunRepository algorithmRunRepository;
        private readonly LiteDbRangeRepository rangeRepository;
        private readonly LiteDbVerticesRepository verticesRepository;

        public LiteDbGraphRepository(ILiteDatabase db)
        {
            collection = db.GetCollection<Graph>(DbTables.Graphs);
            algorithmRunRepository = new LiteDbAlgorithmRunRepository(db);
            rangeRepository = new LiteDbRangeRepository(db);
            verticesRepository = new LiteDbVerticesRepository(db);
        }

        public async Task<Graph> CreateAsync(Graph graph, CancellationToken token = default)
        {
            collection.Insert(graph);
            return await Task.FromResult(graph);
        }

        public async Task<bool> DeleteAsync(int graphId, CancellationToken token = default)
        {
            // Order sensitive. Do not change the order of deleting
            // Reason: some repositories need the presence of values in the database
            await rangeRepository.DeleteByGraphIdAsync(graphId, token);
            await verticesRepository.DeleteVerticesByGraphIdAsync(graphId, token);
            await algorithmRunRepository.DeleteByGraphIdAsync(graphId, token);
            return collection.Delete(graphId);
        }

        public async Task<bool> DeleteAsync(IEnumerable<int> graphIds,
            CancellationToken token = default)
        {
            foreach (var id in graphIds)
            {
                await DeleteAsync(id, token);
            }
            return true;
        }

        public async Task<IEnumerable<Graph>> GetAll(CancellationToken token = default)
        {
            return await Task.FromResult(collection.FindAll());
        }

        public async Task<Graph> ReadAsync(int graphId, CancellationToken token = default)
        {
            return await Task.FromResult(collection.FindById(graphId));
        }

        public async Task<int> ReadCountAsync(CancellationToken token = default)
        {
            return await Task.FromResult(collection.Count());
        }

        public async Task<bool> UpdateAsync(Graph graph, CancellationToken token = default)
        {
            return await Task.FromResult(collection.Update(graph));
        }
    }
}
