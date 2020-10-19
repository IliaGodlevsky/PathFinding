using GraphLibrary.Coordinates;
using GraphLibrary.DTO;
using GraphLibrary.UnitTests.Classes;
using GraphLibrary.Vertex.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphLibrary.UnitTests.Tests.DTOTests
{
    [TestClass]
    public class DtoTests
    {
        [TestMethod]
        public void VertexConstructor_NotNullDto_ConstructCorrectly()
        {
            var temp = new TestVertex
            {
                Cost = 15,
                IsObstacle = true,
                Position = new Coordinate2D(4, 5)
            };

            dynamic dto = new Dto<IVertex>(temp);
            var testVertex = new TestVertex(dto);

            Assert.IsTrue(dto.Cost == testVertex.Cost 
                && dto.IsObstacle && testVertex.IsObstacle 
                && dto.Position.Equals(testVertex.Position));
        }

        [TestMethod]
        public void DtoConstructor_NotNullVertex_ConstructCorrectly()
        {
            var vertex = new TestVertex
            {
                Cost = 15,
                IsObstacle = true,
                Position = new Coordinate2D(4, 5)
            };

            dynamic dto = new Dto<IVertex>(vertex);

            Assert.IsTrue(dto.Cost == vertex.Cost
                && dto.IsObstacle && vertex.IsObstacle
                && dto.Position.Equals(vertex.Position));
        }
    }
}
