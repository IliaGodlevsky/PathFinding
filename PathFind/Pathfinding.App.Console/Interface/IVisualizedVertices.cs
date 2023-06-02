using Pathfinding.App.Console.Model;
using System;

namespace Pathfinding.App.Console.Interface
{
    internal interface IVisualizedVertices
    {
        event Action<Vertex> VertexVisualized;

        bool Contains(Vertex vertex);

        void Visualize(Vertex vertex);

        void Remove(Vertex vertex);
    }
}
