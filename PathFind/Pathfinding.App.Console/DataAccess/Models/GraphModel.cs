using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess.Models
{
    internal class GraphModel : IIdentityItem<long>
    {
        public long Id { get; set; }

        public IEnumerable<ICoordinate> Range { get; set; } = Array.Empty<ICoordinate>();

        public Graph2D<Vertex> Graph { get; set; } = Graph2D<Vertex>.Empty;
    }
}
