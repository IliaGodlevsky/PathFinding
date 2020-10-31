using GraphLib.Coordinates;
using GraphLib.Info;
using GraphLib.UnitTests.Classes;
using GraphLib.Vertex.Cost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphLib.UnitTests.Tests.DTOTests
{
    [TestClass]
    public class DtoTests
    {
        [TestMethod]
        public void VertexConstructor_NotNullDto_ConstructCorrectly()
        {
            var temp = new TestVertex
            {
                Cost = new VertexCost(15),
                IsObstacle = true,
                Position = new Coordinate2D(4, 5)
            };

            var dto = new VertexInfo(temp);
            var testVertex = new TestVertex(dto);

            Assert.IsTrue((int)dto.Cost == (int)testVertex.Cost 
                && dto.IsObstacle && testVertex.IsObstacle 
                && dto.Position.Equals(testVertex.Position));
        }

        [TestMethod]
        public void DtoConstructor_NotNullVertex_ConstructCorrectly()
        {
            var vertex = new TestVertex
            {
                Cost = new VertexCost(15),
                IsObstacle = true,
                Position = new Coordinate2D(4, 5)
            };

            var dto = new VertexInfo(vertex);

            Assert.IsTrue((int)dto.Cost == (int)vertex.Cost
                && dto.IsObstacle && vertex.IsObstacle
                && dto.Position.Equals(vertex.Position));
        }
    }
}
