using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Pathfinding.Infrastructure.Data.Pathfinding
{
    [DebuggerDisplay("Null")]
    public sealed class NullPathfindingVertex : Singleton<NullPathfindingVertex, IPathfindingVertex>,
        IPathfindingVertex
    {
        public bool IsObstacle => true;

        public IVertexCost Cost => NullCost.Interface;

        public IReadOnlyCollection<IPathfindingVertex> Neighbors => Array.Empty<NullPathfindingVertex>();

        public Coordinate Position => Coordinate.Empty;

        private NullPathfindingVertex()
        {

        }
    }
}