using GraphLib.Coordinates.Abstractions;
using GraphLib.Extensions;
using GraphLib.Info;
using GraphLib.Vertex.Cost;
using GraphLib.Vertex.Interface;
using System.Collections.Generic;

namespace Algorithm.NUnitTests
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

        public bool IsEnd { get; set; }
        public bool IsObstacle { get; set; }
        public bool IsStart { get; set; }
        public bool IsVisited { get; set; }
        public VertexCost Cost { get; set; }
        public IList<IVertex> Neighbours { get; set; }
        public IVertex ParentVertex { get; set; }
        public double AccumulatedCost { get; set; }
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
