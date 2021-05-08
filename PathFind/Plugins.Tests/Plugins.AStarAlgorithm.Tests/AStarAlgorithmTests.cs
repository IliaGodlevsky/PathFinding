using Algorithm.Interfaces;
using GraphLib.Interfaces;
using NUnit.Framework;
using Plugins.BaseAlgorithmUnitTest;

namespace Plugins.AStarALgorithm.Tests
{
    [TestFixture]
    public class AStarAlgorithmTests : AlgorithmTest
    {
        protected override IAlgorithm CreateAlgorithm(IGraph graph, IEndPoints endPoints)
        {
            return new AStarAlgorithm.AStarAlgorithm(graph, endPoints);
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