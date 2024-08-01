using Pathfinding.Service.Interface.Models.Undefined;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Models.Serialization
{
    public class GraphStateSerializationModel
    {
        public IReadOnlyCollection<CoordinateModel> Obstacles { get; set; }

        public IReadOnlyCollection<CoordinateModel> Range { get; set; }

        public IReadOnlyCollection<int> Costs { get; set; }
    }
}
