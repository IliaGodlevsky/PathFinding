using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Serialization
{
    internal sealed class SerializationInfo
    {
        public Graph2D<Vertex> Graph { get; set; }

        public IEnumerable<ICoordinate> Range { get; set; }

        public IPathfindingHistory History { get; set; }
    }
}
