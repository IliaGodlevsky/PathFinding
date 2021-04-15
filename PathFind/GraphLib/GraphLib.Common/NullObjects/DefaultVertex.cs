using Common.Attributes;
using GraphLib.Interfaces;
using System.Collections.Generic;

namespace GraphLib.Common.NullObjects
{
    [Default]
    public sealed class DefaultVertex : IVertex
    {
        public bool IsObstacle { get => true; set { } }

        public IVertexCost Cost { get => new DefaultCost(); set { } }

        public ICollection<IVertex> Neighbours { get => new List<IVertex>(); set { } }

        public ICoordinate Position { get => new DefaultCoordinate(); set { } }

        public bool Equals(IVertex other) => other is DefaultVertex;
    }
}