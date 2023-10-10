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

        public ICoordinate Position => NullCoordinate.Interface;

        public IDictionary<IVertex, IVertexCost> Neighbours { get; set; }

        private NullVertex()
        {
            Neighbours = new Dictionary<IVertex, IVertexCost>();
        }

        public bool Equals(IVertex other)
        {
            return other is NullVertex;
        }
    }
}