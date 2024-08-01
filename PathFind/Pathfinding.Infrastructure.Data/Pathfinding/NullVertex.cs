using Pathfinding.Domain.Interface;
using Shared.Primitives.Single;
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

        public ICollection<IVertex> Neighbours { get; } = Array.Empty<IVertex>();

        public ICoordinate Position => NullCoordinate.Interface;

        private NullVertex()
        {

        }

        public bool Equals(IVertex other)
        {
            return other is NullVertex;
        }
    }
}