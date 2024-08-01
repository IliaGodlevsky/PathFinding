using Pathfinding.Domain.Interface;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Requests.Update
{
    public class UpdateRangeRequest<T>
        where T : IVertex
    {
        public int GraphId { get; set; }

        public List<(int Order, T Vertex)> Vertices { get; set; }
            = new List<(int Order, T Vertex)>();
    }
}
