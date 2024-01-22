using Pathfinding.App.Console.DAL.Models.Entities;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Interface
{
    internal interface INeighborsRepository
    {
        IEnumerable<NeighborEntity> Insert(IEnumerable<NeighborEntity> neighbours);

        IReadOnlyDictionary<int, IReadOnlyCollection<NeighborEntity>>
            GetNeighboursForVertices(IEnumerable<int> verticesIds);

        bool DeleteNeighbour(int vertexId, int neighbourId);

        bool DeleteByGraphId(int graphId);
    }
}
