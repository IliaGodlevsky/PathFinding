using Pathfinding.App.Console.Model;
using System;

namespace Pathfinding.App.Console.Interface
{
    internal interface IVertexAction
    {
        void Invoke(Vertex vertex);
    }
}
