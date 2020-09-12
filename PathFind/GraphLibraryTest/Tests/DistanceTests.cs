using System;
using GraphLibrary.DistanceCalculator;
using GraphLibrary.Vertex.Interface;
using GraphLibraryTest.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphLibraryTest.Tests
{
    [TestClass]
    public class DistanceTests
    {
        [TestMethod]
        public void EuclidianDistanceTest()
        {
            var vertex1 = new TestVertex();
            var vertex2 = new TestVertex();
            vertex1.Position = new Position(1, 1);
            vertex2.Position = new Position(2, 2);
            var expectedDistance = Math.Sqrt(2);

            var realDistance = 
                Distance.GetEuclideanDistance(vertex1, vertex2);

            Assert.AreEqual(expectedDistance, realDistance);
        }

        [TestMethod]
        public void ChebyshevDistanceTest()
        {
            var vertex1 = new TestVertex();
            var vertex2 = new TestVertex();
            vertex1.Position = new Position(1, 1);
            vertex2.Position = new Position(2, 2);
            var expectedDistance = 1.0;

            var realDistance = 
                Distance.GetChebyshevDistance(vertex1, vertex2);

            Assert.AreEqual(expectedDistance, realDistance);
        }
    }
}
