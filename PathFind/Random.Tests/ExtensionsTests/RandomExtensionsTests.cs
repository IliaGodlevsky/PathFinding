using Moq;
using NUnit.Framework;
using Random.Extensions;
using Random.Interface;
using System;
using System.Linq;
using ValueRange;

namespace Random.Tests
{
    [TestFixture]
    public class RandomExtensionsTests
    {
        [Test]
        public void MaxUint_MaxIntRange_ReturnMaxIntValue()
        {
            var random = new Mock<IRandom>();

            random.Setup(r => r.NextUint()).Returns(uint.MaxValue);
            int value = random.Object.NextInt(ValueRanges.IntRange);

            Assert.AreEqual(value, int.MaxValue);
        }

        [Test]
        public void MinUint_MaxIntRange_ReturnsMinIntValue()
        {
            var random = new Mock<IRandom>();

            random.Setup(r => r.NextUint()).Returns(uint.MinValue);
            int value = random.Object.NextInt(ValueRanges.IntRange);

            Assert.AreEqual(value, int.MinValue);
        }

        [TestCase(-50.0, 50.0, (uint)int.MaxValue, ExpectedResult = 50.0)]
        [TestCase(-50.0, 50.0, (uint)int.MaxValue / 2, ExpectedResult = 0.0)]
        [TestCase(-50.0, 50.0, default(uint), ExpectedResult = -50.0)]
        public double NextDouble(double minValue, double maxValue, uint randomValue)
        {
            var range = new InclusiveValueRange<double>(maxValue, minValue);

            var random = new Mock<IRandom>();
            random.Setup(r => r.NextUint()).Returns(randomValue);

            return Math.Round(random.Object.NextDouble(range));
        }

        [Test]
        public void NextBytes_FillsWithNonZeroBytes()
        {
            const int bytesLength = 100;
            var bytes = new byte[bytesLength];
            var random = new Mock<IRandom>();
            random.Setup(r => r.NextUint()).Returns(345u);

            random.Object.NextBytes(bytes);

            Assert.IsTrue(bytes.All(b => b > 0));
        }

        [TestCase(default(int), int.MaxValue, (uint)int.MaxValue, ExpectedResult = int.MaxValue)]
        [TestCase(default(int), int.MaxValue, uint.MaxValue, ExpectedResult = int.MaxValue)]
        public int IntPositiveRange_SomeRandomValue_ReturnExpectedResult(int minValue,
            int maxValue, uint randomValue)
        {
            var random = new Mock<IRandom>();

            random.Setup(r => r.NextUint()).Returns(randomValue);

            return random.Object.NextInt(maxValue, minValue);
        }
    }
}
