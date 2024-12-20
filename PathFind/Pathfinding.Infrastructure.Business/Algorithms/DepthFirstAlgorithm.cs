﻿using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Service.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public sealed class DepthFirstAlgorithm : DepthAlgorithm
    {
        public DepthFirstAlgorithm(IEnumerable<IPathfindingVertex> pathfindingRange)
            : base(pathfindingRange)
        {
        }

        protected override IPathfindingVertex GetVertex(IReadOnlyCollection<IPathfindingVertex> neighbors)
        {
            return neighbors.FirstOrDefault() ?? NullPathfindingVertex.Interface;
        }
    }
}