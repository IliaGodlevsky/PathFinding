using Algorithm.Algos.Algos;
using Algorithm.Interfaces;
using GraphLib.Interfaces;
using NUnit.Framework;

namespace Algorithm.Algos.Tests
{
    [TestFixture]
    public class DistanceFirstALgorithmTests : AlgorithmTest
    {
        protected override IAlgorithm<IGraphPath> CreateAlgorithm(IPathfindingRange endPoints)
        {
            return new DistanceFirstAlgorithm(endPoints);
        }

        protected override int GetExpectedCost()
        {
            return 106;
        }

        protected override int GetExpectedLength()
        {
            return 24;
        }
    }
}