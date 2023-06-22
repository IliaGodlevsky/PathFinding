using Pathfinding.App.Console.DataAccess.Models;
using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Model
{
    internal class VisualizationSet
    {
        public IEnumerable<ICoordinate> Visited { get; set; }

        public IEnumerable<ICoordinate> Obstacles { get; set; }

        public IEnumerable<ICoordinate> Range { get; set; }

        public IEnumerable<ICoordinate> Path { get; set; }

        public IEnumerable<int> Costs { get; set; }

        public StatisticsModel Statistics { get; set; }
    }
}
