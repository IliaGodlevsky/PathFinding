using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Models.Undefined
{
    public record CoordinateModel
    {
        public IReadOnlyCollection<int> Coordinate { get; set; }
    }
}
