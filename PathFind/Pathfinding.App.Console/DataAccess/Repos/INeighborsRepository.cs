using Pathfinding.App.Console.DataAccess.Entities;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess.Repos
{
    internal interface INeighborsRepository
    {
        IEnumerable<NeighbourEntity> AddNeighbours(IEnumerable<NeighbourEntity> neighbours);

        IReadOnlyDictionary<int, IReadOnlyCollection<NeighbourEntity>> 
            GetNeighboursForVertices(IEnumerable<int> verticesIds);

        bool DeleteNeighbour(int vertexId, int neighbourId);

        NeighbourEntity AddNeighbour(NeighbourEntity neighbour);

        bool DeleteByGraphId(int graphId);
    }
}
