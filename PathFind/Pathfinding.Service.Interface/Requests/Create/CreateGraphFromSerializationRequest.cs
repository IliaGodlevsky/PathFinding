using Pathfinding.Service.Interface.Models.Serialization;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Requests.Create
{
    public class CreateGraphFromSerializationRequest
    {
        public string Name { get; set; }

        public IReadOnlyList<int> DimensionSizes { get; set; }

        public IReadOnlyCollection<VertexSerializationModel> Vertices { get; set; }
    }
}
