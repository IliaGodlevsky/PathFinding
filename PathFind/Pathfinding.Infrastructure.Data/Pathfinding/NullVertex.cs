using Pathfinding.Domain.Interface;
using Pathfinding.Shared;
using Pathfinding.Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Pathfinding.Infrastructure.Data.Pathfinding
{
    [DebuggerDisplay("Null")]
    public sealed class NullVertex : Singleton<NullVertex, IVertex>, IVertex
    {
        public bool IsObstacle { get => true; set { } }

        public IVertexCost Cost { get => NullCost.Interface; set { } }

        public ICollection<IVertex> Neighbours { get; } = BlackHole<IVertex>.Interface;

        public Coordinate Position => Coordinate.Empty;

        private NullVertex()
        {

        }

        public bool Equals(IVertex other)
        {
            return other is NullVertex;
        }
    }
}