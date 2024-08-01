using LiteDB;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Data.LiteDb.Repositories
{
    public sealed class LiteDbVerticesRepository : IVerticesRepository
    {
        private readonly ILiteCollection<Vertex> collection;

        public LiteDbVerticesRepository(ILiteDatabase db)
        {
            collection = db.GetCollection<Vertex>(DbTables.Vertices);
            collection.EnsureIndex(x => x.Id);
        }

        public async Task<IEnumerable<Vertex>> CreateAsync(IEnumerable<Vertex> vertices, CancellationToken token = default)
        {
            return await Task.Run(() =>
            {
                collection.InsertBulk(vertices);
                return vertices;
            }, token);
        }

        public async Task<bool> DeleteVerticesByGraphIdAsync(int graphId, CancellationToken token = default)
        {
            return await Task.Run(() => collection.DeleteMany(x => x.GraphId == graphId) > 0, token);
        }

        public async Task<Vertex> ReadAsync(int vertexId, CancellationToken token = default)
        {
            return await Task.Run(() => collection.FindById(vertexId), token);
        }

        public async Task<IEnumerable<Vertex>> ReadVerticesByGraphIdAsync(int graphId, CancellationToken token = default)
        {
            return await Task.Run(() =>
            {
                return collection.Query()
                    .Where(x => x.GraphId == graphId)
                    .OrderBy(x => x.Order)
                    .ToEnumerable();
            }, token);
        }

        public Task<bool> UpdateVerticesAsync(IEnumerable<Vertex> vertices, CancellationToken token = default)
        {
            return Task.Run(() => collection.Update(vertices) > 0, token);
        }
    }
}
