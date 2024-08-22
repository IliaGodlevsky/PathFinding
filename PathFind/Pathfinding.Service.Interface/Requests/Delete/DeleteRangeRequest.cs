using Pathfinding.Domain.Interface;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Requests.Delete
{
    public class DeleteRangeRequest<T>
        where T : IVertex
    {
        public List<T> Vertices { get; set; }
    }
}
