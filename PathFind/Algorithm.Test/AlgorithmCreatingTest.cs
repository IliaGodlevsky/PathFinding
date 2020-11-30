using System.Collections.Generic;
using System.Linq;
using Algorithm.AlgorithmCreating;
using Algorithm.PathFindingAlgorithms.Interface;
using GraphLib.Graphs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Test
{
    [TestClass]
    public class AlgorithmCreatingTest
    {
        [TestMethod]
        public void Should_ReturnAlgorithm_When_KeyIsValid()
        {
            var keys = AlgorithmFactory.GetAlgorithmKeys().ToArray();

            var algorithms = GetAlgorithms(keys);

            Assert.AreEqual(keys.Count(), algorithms.Count());
            Assert.IsTrue(algorithms.All(algo => !algo.IsDefault));
        }

        [TestMethod]
        public void Should_ReturnDefaultAlgorithm_When_KeyIsInValid()
        {
            var keys = new string[] { "A", "B", "C", "D", "E", "F" };

            var algorithms = GetAlgorithms(keys);

            Assert.AreEqual(keys.Count(), algorithms.Count());
            Assert.IsTrue(algorithms.All(algo => algo.IsDefault));
        }

        [Ignore]
        private List<IPathFindingAlgorithm> GetAlgorithms(string[] keys)
        {
            var algorithms = new List<IPathFindingAlgorithm>();

            foreach (var key in keys)
            {
                algorithms.Add(AlgorithmFactory.CreateAlgorithm(key, new DefaultGraph()));
            }

            return algorithms;
        }
    }
}
