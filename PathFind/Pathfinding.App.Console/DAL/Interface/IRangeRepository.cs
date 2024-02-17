﻿using Pathfinding.App.Console.DAL.Models.Entities;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Interface
{
    internal interface IRangeRepository
    {
        RangeEntity Insert(RangeEntity entity);

        IEnumerable<RangeEntity> Insert(IEnumerable<RangeEntity> entities);

        bool DeleteByVertexId(int vertexId);

        IEnumerable<RangeEntity> GetByGraphId(int graphId);

        IEnumerable<RangeEntity> GetByVerticesIds(IEnumerable<int> verticesIds);

        bool Update(RangeEntity entity);

        bool Update(IEnumerable<RangeEntity> entities);

        bool DeleteByGraphId(int graphId);
    }
}
