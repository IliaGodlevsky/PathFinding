using Pathfinding.Service.Interface.Models.Undefined;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Models.Serialization
{
    public class SubAlgorithmSerializationModel
    {
        public int Order { get; set; }

        public IReadOnlyCollection<CoordinateModel> Path { get; set; }

        public IReadOnlyCollection<VisitedVerticesModel> Visited { get; set; }
    }
}
