using Pathfinding.Domain.Interface;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Models.Read
{
    public class PathfindingRangeModel
    {
        public List<ICoordinate> Range { get; set; }
            = new List<ICoordinate>();
    }
}
