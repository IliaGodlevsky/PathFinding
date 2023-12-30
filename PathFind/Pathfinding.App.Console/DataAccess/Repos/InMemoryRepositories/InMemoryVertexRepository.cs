using Pathfinding.App.Console.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DataAccess.Repos.InMemoryRepositories
{
    internal sealed class InMemoryVertexRepository
        : InMemoryRepository<VertexEntity>, IVerticesRepository
    {
        public IEnumerable<VertexEntity> AddVertices(IEnumerable<VertexEntity> vertices)
        {
            foreach (var vertex in vertices)
            {
                Create(vertex);
            }
            return vertices;
        }

        public bool DeleteVerticesByGraphId(int graphId)
        {
            var vertices = Repository.Values.Where(x => x.GraphId == graphId);
            foreach (var vertex in vertices)
            {
                Delete(vertex.Id);
            }
            return true;
        }

        public VertexEntity GetVertexById(int vertexId)
        {
            return Read(vertexId);
        }

        public IEnumerable<VertexEntity> GetVerticesByGraphId(int graphId)
        {
            return Repository.Values.Where(x => x.GraphId == graphId);
        }

        public bool UpdateVertex(VertexEntity vertex)
        {
            return Update(vertex);
        }

        public bool UpdateVertices(IEnumerable<VertexEntity> vertices)
        {
            foreach (var vertex in vertices)
            {
                Update(vertex);
            }
            return true;
        }
    }
}
