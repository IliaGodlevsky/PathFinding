using Algorithm.Interfaces;
using GraphLib.Interfaces;
using NUnit.Framework;
using Plugins.BaseAlgorithmUnitTest;

namespace Plugins.DistanceFirstALgorithm.Tests
{
    [TestFixture]
    public class DistanceFirstALgorithmTests : AlgorithmTest
    {
        protected override IAlgorithm CreateAlgorithm(IGraph graph, IEndPoints endPoints)
        {
            return new DistanceFirstAlgorithm.DistanceFirstAlgorithm(graph, endPoints);
        }

        protected override int GetExpectedCost()
        {
            return 349;
        }

        protected override int GetExpectedLength()
        {
            return 79;
        }
    }
}