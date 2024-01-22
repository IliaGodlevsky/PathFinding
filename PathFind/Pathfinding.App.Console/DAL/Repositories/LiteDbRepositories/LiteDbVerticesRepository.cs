using LiteDB;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.Entities;
using Pathfinding.App.Console.Extensions;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Repositories.LiteDbRepositories
{
    internal sealed class LiteDbVerticesRepository : IVerticesRepository
    {
        private readonly ILiteCollection<VertexEntity> collection;

        public LiteDbVerticesRepository(ILiteDatabase db)
        {
            collection = db.GetNamedCollection<VertexEntity>();
            collection.EnsureIndex(x => x.GraphId);
            collection.EnsureIndex(x => x.Id);
        }

        public IEnumerable<VertexEntity> Insert(IEnumerable<VertexEntity> vertices)
        {
            collection.InsertBulk(vertices);
            return vertices;
        }

        public bool DeleteVerticesByGraphId(int graphId)
        {
            return collection.DeleteMany(x => x.GraphId == graphId) > 0;
        }

        public VertexEntity Read(int vertexId)
        {
            return collection.FindById(vertexId);
        }

        public IEnumerable<VertexEntity> GetVerticesByGraphId(int graphId)
        {
            return collection.Find(x => x.GraphId == graphId);
        }

        public bool Update(VertexEntity vertex)
        {
            return collection.Update(vertex);
        }

        public bool UpdateVertices(IEnumerable<VertexEntity> vertices)
        {
            return collection.Update(vertices) > 0;
        }
    }
}
