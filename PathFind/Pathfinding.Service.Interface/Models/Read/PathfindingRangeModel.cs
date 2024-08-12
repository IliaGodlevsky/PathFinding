using Pathfinding.Shared.Primitives;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Models.Read
{
    public class PathfindingRangeModel
    {
        public List<Coordinate> Range { get; set; }
            = new List<Coordinate>();
    }
}
