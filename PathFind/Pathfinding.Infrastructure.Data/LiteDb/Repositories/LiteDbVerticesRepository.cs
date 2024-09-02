﻿using LiteDB;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Data.LiteDb.Repositories
{
    internal sealed class LiteDbVerticesRepository : IVerticesRepository
    {
        private readonly ILiteCollection<Vertex> collection;

        public LiteDbVerticesRepository(ILiteDatabase db)
        {
            collection = db.GetCollection<Vertex>(DbTables.Vertices);
            collection.EnsureIndex(x => x.Id);
        }

        public async Task<IEnumerable<Vertex>> CreateAsync(IEnumerable<Vertex> vertices, CancellationToken token = default)
        {
            collection.InsertBulk(vertices);
            return await Task.FromResult(vertices);
        }

        public async Task<bool> DeleteVerticesByGraphIdAsync(int graphId, CancellationToken token = default)
        {
            return await Task.FromResult(collection.DeleteMany(x => x.GraphId == graphId) > 0);
        }

        public async Task<Vertex> ReadAsync(int vertexId, CancellationToken token = default)
        {
            return await Task.FromResult(collection.FindById(vertexId));
        }

        public async Task<IEnumerable<Vertex>> ReadVerticesByGraphIdAsync(int graphId, CancellationToken token = default)
        {
            await Task.CompletedTask;
            return collection.Query()
                .Where(x => x.GraphId == graphId)
                .ToEnumerable();
        }

        public async Task<bool> UpdateVerticesAsync(IEnumerable<Vertex> vertices, CancellationToken token = default)
        {
            return await Task.FromResult(collection.Update(vertices) > 0);
        }
    }
}
