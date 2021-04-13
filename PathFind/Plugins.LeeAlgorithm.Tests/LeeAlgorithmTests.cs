using GraphLib.Interface;
using Moq;
using NUnit.Framework;

namespace Plugins.LeeAlgorithm.Tests
{
    [TestFixture]
    public class LeeAlgorithmTests
    {
        private Mock<IGraph> graph2DMock;

        public LeeAlgorithmTests()
        {
            graph2DMock = new Mock<IGraph>();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}