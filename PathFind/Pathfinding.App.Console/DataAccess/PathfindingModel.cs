using Pathfinding.App.Console.Model.Notes;
using Pathfinding.GraphLib.Core.Interface;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess
{
    internal class PathfindingModel
    {
        Guid AlgorithmId { get; set; }

        public Statistics Statistics { get; }

        public IReadOnlyCollection<ICoordinate> Path { get; }

        public IReadOnlyCollection<ICoordinate> Range { get; }

        public IReadOnlyCollection<ICoordinate> Visited { get; }

        public IReadOnlyCollection<ICoordinate> Obstacles { get; }

        public IReadOnlyCollection<int> Costs { get; }
    }
}
