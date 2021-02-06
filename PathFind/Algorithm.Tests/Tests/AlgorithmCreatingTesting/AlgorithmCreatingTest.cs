using Algorithm.AlgorithmCreating;
using NUnit.Framework;
using System.Collections;

namespace Algorithm.Tests.Tests.AlgorithmCreatingTesting
{
    [TestFixture]
    internal class AlgorithmCreatingTest
    {
        [Test, TestCaseSource(nameof(AlgorithmKeys))]
        public void CreateAlgorithm_ValidKey_ReturnsAlgorithm(string key)
        {
            var algorithm = AlgorithmFactory.GetAlgorithm(key);

            Assert.IsTrue(!algorithm.IsDefault);
        }

        [Test, TestCaseSource(nameof(FakeAlgorithmKeys))]
        public void CreateAlgorithm_InvalidKey_ReturnsDefaultAlgorithm(string key)
        {
            var algorithm = AlgorithmFactory.GetAlgorithm(key);

            Assert.IsTrue(algorithm.IsDefault);
        }

        private static readonly IEnumerable AlgorithmKeys = AlgorithmFactory.AlgorithmsDescriptions;
        private static readonly IEnumerable FakeAlgorithmKeys = new string[] 
        { 
            "Key","Double key","Tripple key", "Monster key", "Mega key", 
            "Ultra key", "Wicked key","Rampage key", "GodlikeKey"
        };
    }
}
