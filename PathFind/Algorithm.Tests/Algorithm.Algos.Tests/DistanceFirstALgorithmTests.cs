using Algorithm.Algos.Algos;
using Algorithm.Interfaces;
using GraphLib.Interfaces;
using NUnit.Framework;

namespace Algorithm.Algos.Tests
{
    [TestFixture]
    public class DistanceFirstALgorithmTests : AlgorithmTest
    {
        protected override IAlgorithm CreateAlgorithm(IGraph graph, IEndPoints endPoints)
        {
            return new DistanceFirstAlgorithm(graph, endPoints);
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