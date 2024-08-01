using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Models.Undefined
{
    public class VisitedVerticesModel
    {
        public CoordinateModel Current { get; set; }

        public IReadOnlyCollection<CoordinateModel> Enqueued { get; set; }
    }
}
