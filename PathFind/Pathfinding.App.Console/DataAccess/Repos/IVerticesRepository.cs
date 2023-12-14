using Pathfinding.App.Console.DataAccess.Entities;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess.Repos
{
    internal interface IVerticesRepository
    {
        IEnumerable<VertexEntity> GetVerticesByGraphId(int graphId);

        IEnumerable<VertexEntity> AddVertices(IEnumerable<VertexEntity> vertices);

        bool DeleteVerticesByGraphId(int graphId);

        bool UpdateVertex(VertexEntity vertex);

        bool UpdateVertices(IEnumerable<VertexEntity> vertices);

        VertexEntity GetVertexById(int vertexId);
    }
}
