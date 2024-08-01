using Pathfinding.Service.Interface.Models.Serialization;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Requests.Create
{
    public class CreateGraphsFromSerializationRequest
    {
        public List<GraphSerializationModel> Graphs { get; set; }
    }
}
