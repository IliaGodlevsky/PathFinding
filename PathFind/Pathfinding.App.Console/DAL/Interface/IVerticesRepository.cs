using Pathfinding.App.Console.DAL.Models.Entities;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Interface
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
