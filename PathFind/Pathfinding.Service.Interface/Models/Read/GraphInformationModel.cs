using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Models.Read
{
    public class GraphInformationModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsReadOnly { get; set; }

        public string Neighborhood { get; set; }

        public string SmoothLevel { get; set; }

        public IReadOnlyList<int> Dimensions { get; set; }

        public int ObstaclesCount { get; set; }
    }
}
