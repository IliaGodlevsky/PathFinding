using Algorithm.AlgorithmCreating;
using NUnit.Framework;
using System.Collections;

namespace Algorithm.Tests.AlgorithmCreatingTesting
{
    [TestFixture]
    internal class AlgorithmCreatingTest
    {
        [Test, TestCaseSource(nameof(AlgorithmKeys))]
        public void CreateAlgorithm_ValidKey_ReturnsAlgorithm(string key)
        {
            var algorithm = AlgorithmFactory.CreateAlgorithm(key);

            Assert.IsTrue(!algorithm.IsDefault);
        }

        [TestCase("Algorithm A")]
        [TestCase("A*")]
        [TestCase("Pathfinding algorithm")]
        public void CreateAlgorithm_InvalidKey_ReturnsDefaultAlgorithm(string key)
        {
            var algorithm = AlgorithmFactory.CreateAlgorithm(key);

            Assert.IsTrue(algorithm.IsDefault);
        }

        private static readonly IEnumerable AlgorithmKeys = AlgorithmFactory.AlgorithmsDescriptions;
    }
}
