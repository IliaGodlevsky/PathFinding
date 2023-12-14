using Pathfinding.App.Console.DataAccess.Entities;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess.Repos
{
    internal interface IRangeRepository
    {
        RangeEntity AddRange(RangeEntity entity);

        IEnumerable<RangeEntity> AddRange(IEnumerable<RangeEntity> entities);

        bool DeleteByVertexId(int vertexId);

        RangeEntity GetByVertexId(int vertexId);

        IEnumerable<RangeEntity> GetByGraphId(int graphId);

        bool Update(RangeEntity entity);

        bool DeleteByGraphId(int graphId);
    }
}
