using GraphLibrary.ValueRanges;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphLibrary.UnitTests.Tests.ValueRangeTests
{
    [TestClass]
    public class ValueRangeTest
    {
        [TestMethod]
        public void IsInBounds_ValueIsOutOfBounds_ReturnsFalse()
        {
            var valueRange = new ValueRange(25, 100);

            var value = 115;

            Assert.IsFalse(valueRange.IsInBounds(value));
        }

        [TestMethod]
        public void ReturnInBounds_ValueIsOutOfBounds_ReturnsLowerRangeValue()
        {
            var valueRange = new ValueRange(25, 100);
            var value1 = 115;

            value1 = valueRange.ReturnInBounds(value1);

            Assert.AreEqual(valueRange.LowerRange, value1);

        }

        [TestMethod]
        public void IsInBounds_ValueIsInBounds_ReturnsTrue()
        {
            var valueRange = new ValueRange(25, 100);
            var value = 75;

            Assert.IsTrue(valueRange.IsInBounds(value));
        }

        [TestMethod]
        public void ReturnInBounds_ValueIsInOfBounds_ReturnsValue()
        {
            var valueRange = new ValueRange(25, 100);

            var value = 75;
            var temp = value;
            value = valueRange.ReturnInBounds(value);

            Assert.AreEqual(temp, value);

        }
    }
}
