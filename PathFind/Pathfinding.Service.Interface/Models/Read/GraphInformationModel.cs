using Pathfinding.Domain.Core;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Models.Read
{
    public class GraphInformationModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Neighborhoods Neighborhood { get; set; }

        public SmoothLevels SmoothLevel { get; set; }

        public GraphStatuses Status { get; set; }

        public IReadOnlyList<int> Dimensions { get; set; }

        public int ObstaclesCount { get; set; }
    }
}
