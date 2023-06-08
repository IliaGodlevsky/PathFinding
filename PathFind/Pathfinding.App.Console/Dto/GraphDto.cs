using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using System;

namespace Pathfinding.App.Console.Dto
{
    internal class GraphDto : IDto
    {
        public Guid Id { get; set; }

        public Graph2D<Vertex> Vertices { get; set; }
    }
}
