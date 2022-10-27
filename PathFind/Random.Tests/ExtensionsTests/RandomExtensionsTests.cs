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
        [TestCase]
        public void MaxUint_MaxIntRange_ReturnMaxIntValue()
        {
            var random = new Mock<IRandom>();
            random.Setup(r => r.NextUint()).Returns(uint.MaxValue);

            int value = random.Object.NextInt(ValueRanges.IntRange);

            Assert.AreEqual(value, int.MaxValue);
        }

        [TestCase]
        public void MinUint_MaxIntRange_ReturnsMinIntValue()
        {
            var random = new Mock<IRandom>();
            random.Setup(r => r.NextUint()).Returns(uint.MinValue);

            int value = random.Object.NextInt(ValueRanges.IntRange);

            Assert.AreEqual(value, int.MinValue);
        }
    }
}
