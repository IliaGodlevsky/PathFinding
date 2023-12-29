using Pathfinding.App.Console.DataAccess.Entities;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess.Repos
{
    internal interface INeighborsRepository
    {
        IEnumerable<NeighborEntity> AddNeighbours(IEnumerable<NeighborEntity> neighbours);

        IReadOnlyDictionary<int, IReadOnlyCollection<NeighborEntity>>
            GetNeighboursForVertices(IEnumerable<int> verticesIds);

        bool DeleteNeighbour(int vertexId, int neighbourId);

        NeighborEntity AddNeighbour(NeighborEntity neighbour);

        bool DeleteByGraphId(int graphId);
    }
}
