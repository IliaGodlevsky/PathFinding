using Pathfinding.App.Console.Model;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Interface
{
    internal interface IVisualizedVertices : IEnumerable<Vertex>
    {
        event Action<Vertex> VertexVisualized;

        bool Contains(Vertex vertex);

        void Visualize(Vertex vertex);

        void Remove(Vertex vertex);
    }
}
