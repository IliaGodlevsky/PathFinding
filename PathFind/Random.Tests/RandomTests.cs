using Common.Extensions.EnumerableExtensions;
using NUnit.Framework;
using Random.Interface;
using System.Linq;

namespace Random.Tests
{
    [TestFixture]
    public abstract class RandomTests
    {
        private const int Limit = 15000;

        protected abstract IRandom Random { get; }

        [Test]
        public void NextUint_ReturnsVariousNumbers()
        {
            var numbers = new uint[Limit];

            for (int i = 0; i < Limit; i++)
            {
                numbers[i] = Random.NextUint();
            }
            var unique = numbers.Distinct().ToReadOnly();

            Assert.AreEqual(numbers.Length, unique.Count);
        }
    }
}
