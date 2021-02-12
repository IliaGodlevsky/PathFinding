using GraphLib.Interface;
using GraphLib.VertexCost;
using System.Collections.Generic;

namespace GraphLib.NullObjects
{
    public sealed class DefaultVertex : IVertex
    {
        public bool IsObstacle { get => true; set { } }

        public IVertexCost Cost { get => new Cost(default); set { } }

        public IList<IVertex> Neighbours { get => new List<IVertex>(); set { } }

        public ICoordinate Position { get => new DefaultCoordinate(); set { } }

        public bool IsDefault => true;
    }
}
