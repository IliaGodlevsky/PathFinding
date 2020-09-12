using GraphLibrary.DTO;
using GraphLibrary.UnitTests.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphLibrary.UnitTests.Tests
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
                IsObstacle = true
            };

            var dto = new VertexDto(temp);
            var testVertex = new TestVertex(dto);

            Assert.IsTrue(dto.Cost == testVertex.Cost &&
                dto.IsObstacle && testVertex.IsObstacle);
        }

        [TestMethod]
        public void DtoConstructor_NotNullVertex_ConstructCorrectly()
        {
            var vertex = new TestVertex
            {
                Cost = 15,
                IsObstacle = true
            };

            var dto = new VertexDto(vertex);

            Assert.IsTrue(dto.Cost == vertex.Cost &&
                dto.IsObstacle && vertex.IsObstacle);
        }
    }
}
