using Algorithm.Interfaces;
using GraphLib.Interfaces;
using NUnit.Framework;
using Plugins.BaseAlgorithmUnitTest;

namespace Plugins.AStarMOdified.Tests
{
    [TestFixture]
    public class AStarModifiedTests : AlgorithmTest
    {
        protected override IAlgorithm CreateAlgorithm(IGraph graph, IEndPoints endPoints)
        {
            return new AStarModified.AStarModified(graph, endPoints);
        }

        protected override int GetExpectedCost()
        {
            return 229;
        }

        protected override int GetExpectedLength()
        {
            return 79;
        }
    }
}