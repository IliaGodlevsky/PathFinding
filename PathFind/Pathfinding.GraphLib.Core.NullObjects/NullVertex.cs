using Pathfinding.GraphLib.Core.Interface;
using Shared.Primitives.Single;
using System.Collections.Generic;
using System.Diagnostics;

namespace Pathfinding.GraphLib.Core.NullObjects
{
    [DebuggerDisplay("Null")]
    public sealed class NullVertex : Singleton<NullVertex, IVertex>, IVertex
    {
        public bool IsObstacle { get => true; set { } }

        public IVertexCost Cost { get => NullCost.Interface; set { } }

        public IList<IVertex> Neighbours { get => new List<IVertex>(); set { } }

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