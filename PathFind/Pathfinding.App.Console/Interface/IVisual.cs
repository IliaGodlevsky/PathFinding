using Pathfinding.App.Console.Model;
using System;

namespace Pathfinding.App.Console.Interface
{
    internal interface IVisual
    {
        event Action<Vertex> Visualized;

        bool Contains(Vertex vertex);

        void Visualize(Vertex vertex);

        void Remove(Vertex vertex);
    }
}
