using Pathfinding.App.Console.DAL.Models.Entities;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Interface
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
