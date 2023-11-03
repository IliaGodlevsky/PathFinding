using Pathfinding.App.Console.Model;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Interface
{
    internal interface IVisualizedVertices
    {
        event Action<int, Vertex> VertexVisualized;

        bool Contains(int id, Vertex vertex);

        bool Add(int id, Vertex vertex);

        void Remove(int id, Vertex vertex);

        void Clear(int id);

        IReadOnlyCollection<Vertex> GetVertices(int id);
    }
}
