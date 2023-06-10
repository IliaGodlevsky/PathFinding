using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Model
{
    internal sealed class SerializationInfo
    {
        public Graph2D<Vertex> Graph { get; set; }

        public IEnumerable<ICoordinate> Range { get; set; }

        public IUnitOfWork UnitOfWork { get; set; }
    }
}
