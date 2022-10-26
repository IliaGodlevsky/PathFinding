using Common.Extensions.EnumerableExtensions;
using NUnit.Framework;
using Random.Extensions;
using Random.Interface;
using System.Linq;
using ValueRange;
using ValueRange.Extensions;

namespace Random.Tests
{
    [TestFixture]
    public abstract class RandomTests
    {
        private const int Limit = 15000;

        protected abstract IRandom Random { get; }

        [TestCase]
        public void NextUint_ReturnsVariousNumbers()
        {
            var numbers = Random.NextUintArray(Limit);
            var unique = numbers.Distinct().ToReadOnly();

            Assert.IsTrue(numbers.Count == unique.Count);
        }

        [TestCase(-15, 500)]
        [TestCase(-5000, 6000)]
        [TestCase(int.MinValue, int.MaxValue)]
        public void NextInt_VariousRanges_ReturnsValueInRange(int minValue, int maxValue)
        {
            var range = new InclusiveValueRange<int>(maxValue, minValue);

            var numbers = Random.NextIntArray(Limit, range);

            Assert.IsTrue(numbers.All(number => range.Contains(number)));
        }
    }
}
