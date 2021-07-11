using NullObject.Extensions;
using NUnit.Framework;

namespace Algorithm.Common.Tests
{
    [TestFixture]
    public class DefaultAlgorithmTests
    {
        [Test]
        public void FindPath_ReturnsNullGraphPath()
        {
            var algorithm = new NullAlgorithm();

            var path = algorithm.FindPath();

            Assert.IsTrue(path.IsNull());
        }
    }
}