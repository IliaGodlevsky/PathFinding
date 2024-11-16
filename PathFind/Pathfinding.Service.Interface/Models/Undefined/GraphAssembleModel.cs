using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Models.Undefined
{
    public class GraphAssembleModel
    {
        public IReadOnlyList<int> Dimensions { get; set; }

        public IReadOnlyCollection<VertexAssembleModel> Vertices { get; set; }
    }
}
