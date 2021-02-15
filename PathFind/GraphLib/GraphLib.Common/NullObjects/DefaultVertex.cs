using GraphLib.Interface;
using System.Collections.Generic;

namespace GraphLib.Common.NullObjects
{
    public sealed class DefaultVertex : IVertex
    {
        public bool IsObstacle { get => true; set { } }

        public IVertexCost Cost { get => new DefaultCost(); set { } }

        public IList<IVertex> Neighbours { get => new List<IVertex>(); set { } }

        public ICoordinate Position { get => new DefaultCoordinate(); set { } }

        public bool IsDefault => true;
    }
}
