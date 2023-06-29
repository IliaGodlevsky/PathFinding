using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Model.VertexActions
{
    internal sealed class ActiveVertex
    {
        public Vertex Current { get; set; }

        public IList<ICoordinate> Availiable { get; } = new List<ICoordinate>();
    }
}
