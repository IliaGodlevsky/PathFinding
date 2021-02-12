using GraphLib.Extensions;
using GraphLib.Interface;
using System.Collections.Generic;

namespace Algorithm.Tests.TestsInfrastructure.Objects
{
    internal sealed class TestVertex : IVertex
    {
        public TestVertex()
        {
            this.Initialize();
        }

        public bool IsObstacle { get; set; }
        public IVertexCost Cost { get; set; }
        public IList<IVertex> Neighbours { get; set; }
        public ICoordinate Position { get; set; }
        public bool IsDefault => false;
    }
}
