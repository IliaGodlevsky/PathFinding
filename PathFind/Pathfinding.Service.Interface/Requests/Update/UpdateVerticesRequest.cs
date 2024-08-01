using Pathfinding.Domain.Interface;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Requests.Update
{
    public class UpdateVerticesRequest<T>
        where T : IVertex
    {
        public int GraphId { get; set; }

        public List<T> Vertices { get; set; } = new List<T>();
    }
}
