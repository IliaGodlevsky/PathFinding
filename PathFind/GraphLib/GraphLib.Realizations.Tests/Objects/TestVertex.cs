using GraphLib.Extensions;
using GraphLib.Interface;
using System.Collections.Generic;

namespace GraphLib.Realizations.Tests.Objects
{
    internal sealed class TestVertex : IVertex
    {
        public TestVertex()
        {
            this.Initialize();
        }

        public bool IsObstacle { get; set; }
        public IVertexCost Cost { get; set; }
        public ICollection<IVertex> Neighbours { get; set; }
        public ICoordinate Position { get; set; }
    }
}
