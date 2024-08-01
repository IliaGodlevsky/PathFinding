using Pathfinding.Service.Interface.Models.Undefined;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Models.Serialization
{
    public class VertexSerializationModel
    {
        public CoordinateModel Position { get; set; }

        public VertexCostModel Cost { get; set; }

        public bool IsObstacle { get; set; }

        public IReadOnlyCollection<CoordinateModel> Neighbors { get; set; }
    }
}
