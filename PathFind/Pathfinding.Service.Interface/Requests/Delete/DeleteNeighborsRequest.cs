using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Requests.Delete
{
    public class DeleteNeighborsRequest<T>
    {
        public IReadOnlyDictionary<T, IReadOnlyCollection<T>> Neighbors { get; set; }
    }
}
