using NUnit.Framework;
using Random.Extensions;
using Random.Interface;
using Random.Realizations;
using System.Linq;
using ValueRange;
using ValueRange.Extensions;

namespace Common.Tests
{
    [TestFixture]
    public abstract class RandomTests
    {
        [TestCase(int.MaxValue, int.MinValue)]
        [TestCase(-5, -10)]
        [TestCase(14, 0)]
        [TestCase(144, 12)]
        [TestCase(0, -13)]
        [TestCase(int.MaxValue, int.MaxValue - 1)]
        public void Next_VariousRanges_ReturnsNumbersInRange(int maxValue, int minValue)
        {
            var range = new InclusiveValueRange<int>(maxValue, minValue);
            int limit = 200;
            var random = GetRandom();

            var numbers = GetNumbers(random, limit, range);

            Assert.IsTrue(numbers.All(value => range.Contains(value)));
        }

        [Test]
        public void Next_MaxRange_ReturnsVariousNumbers()
        {
            var range = new InclusiveValueRange<int>(int.MaxValue, int.MinValue);
            int limit = 5000;
            var random = GetRandom();

            var numbers = GetNumbers(random, limit, range);
            var unique = numbers.Distinct().ToArray();

            Assert.IsTrue(numbers.Length == unique.Length);
        }

        private int[] GetNumbers(IRandom random, int count, InclusiveValueRange<int> range)
        {
            var numbers = new int[count];
            while(count-->0)
            {
                numbers[count] = random.Next(range);
            }
            return numbers;
        }

        protected abstract IRandom GetRandom();
    }
}
