using Pathfinding.Service.Interface.Models.Undefined;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Models.Serialization
{
    public class PathfindingHistorySerializationModel
    {
        public GraphSerializationModel Graph { get; set; }

        public IReadOnlyCollection<AlgorithmRunHistorySerializationModel> Algorithms { get; set; }

        public IReadOnlyCollection<CoordinateModel> Range { get; set; }
    }
}
