using Pathfinding.Domain.Interface;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Requests.Create
{
    public class CreateNeighborsRequest<T>
        where T : IVertex
    {
        public IReadOnlyDictionary<T, IReadOnlyCollection<T>> Neighbors { get; set; }
    }
}
