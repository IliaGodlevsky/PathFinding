using GraphLib.Extensions;
using GraphLib.Interface;
using GraphLib.Serialization.Extensions;
using System.Collections.Generic;

namespace GraphLib.Serialization.Tests.Objects
{
    internal class TestVertex : IVertex
    {
        public TestVertex()
        {
            this.Initialize();
        }

        public TestVertex(VertexSerializationInfo info) : this()
        {
            this.Initialize(info);
        }

        public bool IsObstacle { get; set; }
        public IVertexCost Cost { get; set; }
        public IList<IVertex> Neighbours { get; set; }
        public ICoordinate Position { get; set; }
        public virtual ICoordinateRadar CoordinateRadar 
            => new TestCoordinateRadar();
    }
}
