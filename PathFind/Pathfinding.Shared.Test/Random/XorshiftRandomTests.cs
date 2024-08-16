using Pathfinding.Shared.Interface;
using Pathfinding.Shared.Random;

namespace Pathfinding.Shared.Test.Random
{
    [TestFixture]
    public class XorshiftRandomTests : RandomTests
    {
        protected override IRandom Random { get; set; }

        [SetUp]
        public void Setup()
        {
            Random = new XorshiftRandom();
        }

        [TestCaseSource(typeof(RandomTestDataProviders), nameof(RandomTestDataProviders.XorshiftRandomDataProvider))]
        public override void GetNextUInt_ShouldReturnValuesInTolerance(int limit, double tolerance)
        {
            base.GetNextUInt_ShouldReturnValuesInTolerance(limit, tolerance);
        }
    }
}
