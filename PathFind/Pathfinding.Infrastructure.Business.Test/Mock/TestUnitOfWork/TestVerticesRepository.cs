﻿#nullable disable
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;
using Pathfinding.Shared.Extensions;

namespace Pathfinding.Infrastructure.Business.Test.Mock.TestUnitOfWork
{
    internal sealed class TestVerticesRepository : IVerticesRepository
    {
        private int id = 0;

        private readonly HashSet<Vertex> set = new(EntityComparer<int>.Instance);

        public async Task<IEnumerable<Vertex>> CreateAsync(IEnumerable<Vertex> vertices, CancellationToken token = default)
        {
            var result = vertices
                .ForEach(x => x.Id = ++id)
                .ForWhole(set.AddRange)
                .ToList();
            return await Task.FromResult(result);
        }

        public async Task<bool> DeleteVerticesByGraphIdAsync(int graphId, CancellationToken token = default)
        {
            var result = set.RemoveWhere(x => x.GraphId == graphId);
            return await Task.FromResult(result > 0);
        }

        public async Task<Vertex> ReadAsync(int vertexId, CancellationToken token = default)
        {
            var vertex = new Vertex() { Id = vertexId };
            set.TryGetValue(vertex, out var result);
            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<Vertex>> ReadVerticesByGraphIdAsync(int graphId, CancellationToken token = default)
        {
            var vertices = set.Where(x => x.GraphId == graphId).ToList();
            return await Task.FromResult(vertices);
        }

        public Task<bool> UpdateVerticesAsync(IEnumerable<Vertex> vertices, CancellationToken token = default)
        {
            foreach (var vertex in vertices)
            {
                if (set.TryGetValue(vertex, out var result))
                {
                    set.Remove(result);
                    set.Add(vertex);
                }
            }
            return Task.FromResult(true);
        }
    }
}
