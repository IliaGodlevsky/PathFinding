using Moq;
using NUnit.Framework;
using Random.Extensions;
using Random.Interface;
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

        [TestCase(default(int), int.MaxValue, (uint)int.MaxValue, ExpectedResult = int.MaxValue)]
        [TestCase(default(int), int.MaxValue, uint.MaxValue, ExpectedResult = int.MaxValue)]
        public int IntPositiveRange_SomeRandomValue_ReturnExpectedResult(int minValue, int maxValue, uint randomValue)
        {
            var random = new Mock<IRandom>();
            random.Setup(r => r.NextUint()).Returns(randomValue);

            return random.Object.NextInt(maxValue, minValue);
        }
    }
}
