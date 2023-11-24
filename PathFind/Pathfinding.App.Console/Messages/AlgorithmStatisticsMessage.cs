using Pathfinding.App.Console.Model.Notes;
using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class AlgorithmStatisticsMessage
    {
        public int GraphId { get; set; }

        public Statistics Statistics { get; set; }

        public IReadOnlyCollection<ICoordinate> Path { get; set; }

        public IReadOnlyCollection<ICoordinate> Visited { get; set; }

        public IReadOnlyCollection<ICoordinate> Obstacles { get; set; }

        public IReadOnlyCollection<ICoordinate> Range { get; set; }

        public int[] Costs { get; set; }
    }
}
