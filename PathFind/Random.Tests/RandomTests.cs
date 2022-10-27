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
            var numbers = new uint[Limit];

            for (int i = 0; i < Limit; i++)
            {
                numbers[i] = Random.NextUint();
            }
            var unique = numbers.Distinct().ToReadOnly();

            Assert.IsTrue(numbers.Length == unique.Count);
        }
    }
}
