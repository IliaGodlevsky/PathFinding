using Pathfinding.Shared.Interface;
using Pathfinding.Shared.Random;
using Pathfinding.TestUtils.Attributes;

namespace Pathfinding.Shared.Test.Random
{
    [TestFixture, UnitTest]
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
