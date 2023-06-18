using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess.Models
{
    internal class GraphModel : IIdentityItem<Guid>
    {
        public Guid Id { get; set; }

        public IEnumerable<Vertex> Range { get; set; }

        public Graph2D<Vertex> Graph { get; set; }
    }
}
