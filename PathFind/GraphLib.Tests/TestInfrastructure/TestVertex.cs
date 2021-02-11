using GraphLib.Extensions;
using GraphLib.Infrastructure;
using GraphLib.Interface;
using System.Collections.Generic;

namespace GraphLib.Tests.TestInfrastructure
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

        public bool IsDefault => false;

        public void MakeUnweighted()
        {

        }

        public void MakeWeighted()
        {

        }

        public void MarkAsEnd()
        {

        }

        public void MarkAsEnqueued()
        {

        }

        public void MarkAsObstacle()
        {

        }

        public void MarkAsPath()
        {

        }

        public void MarkAsSimpleVertex()
        {

        }

        public void MarkAsStart()
        {

        }

        public void MarkAsVisited()
        {

        }
    }
}
