using Algorithm.AlgorithmCreating;
using Algorithm.Algorithms.Abstractions;
using GraphLib.Graphs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Test
{
    [TestClass]
    public class AlgorithmCreatingTest
    {
        [TestMethod]
        public void Should_ReturnAlgorithm_When_KeyIsValid()
        {
            var keys = AlgorithmFactory.AlgorithmsDescriptions.ToArray();

            var algorithms = GetAlgorithms(keys);

            Assert.AreEqual(keys.Count(), algorithms.Count());
            Assert.IsTrue(algorithms.All(IsNotDefault));
        }

        [TestMethod]
        public void Should_ReturnDefaultAlgorithm_When_KeyIsInValid()
        {
            var keys = new string[] { "A", "B", "C", "D", "E", "F" };

            var algorithms = GetAlgorithms(keys);

            Assert.IsTrue(algorithms.All(IsDefault));
        }

        [Ignore]
        private IEnumerable<IAlgorithm> GetAlgorithms(string[] keys)
        {
            return keys.Select(key => AlgorithmFactory.CreateAlgorithm(key, new NullGraph()));
        }

        private bool IsDefault(IAlgorithm algo) => algo.IsDefault;
        private bool IsNotDefault(IAlgorithm algo) => !IsDefault(algo);
    }
}
