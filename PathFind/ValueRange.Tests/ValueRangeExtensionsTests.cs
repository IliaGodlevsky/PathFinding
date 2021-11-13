using NUnit.Framework;
using Random.Extensions;
using System.Collections.Generic;
using System.Linq;
using ValueRange;
using ValueRange.Extensions;

namespace Common.Tests
{
    [TestFixture]
    internal sealed class ValueRangeExtensionsTests
    {
        [TestCase(-43, 17)]
        [TestCase(0, 100)]
        [TestCase(-13, 13)]
        [TestCase(45, 77)]
        [TestCase(11, 1021)]
        public void GetRandomValueTest_InclusiveValueRangeInt32_ReturnsValueInRange(
            int lowerValue, int upperValue)
        {
            var valueRange = new InclusiveValueRange<int>(upperValue, lowerValue);

            var randomValues = RandomValues(valueRange);

            Assert.IsTrue(randomValues.All(valueRange.Contains));
        }

        [TestCase(-43.1, 17.5)]
        [TestCase(0, 1)]
        [TestCase(0.01, 13.8)]
        public void GetRandomValueTest_InclusiveValueRangeDouble_ReturnsValueInRange(
            double lowerValue, double upperValue)
        {
            var valueRange = new InclusiveValueRange<double>(upperValue, lowerValue);

            var randomValues = RandomValues(valueRange).ToList();

            Assert.IsTrue(randomValues.All(valueRange.Contains));
        }

        private IEnumerable<int> RandomValues(InclusiveValueRange<int> valueRange)
        {
            for (int i = 0; i < valueRange.Amplitude(); i++)
            {
                yield return valueRange.GetRandomValue();
            }
        }

        private IEnumerable<double> RandomValues(InclusiveValueRange<double> valueRange)
        {
            for (int i = 0; i < valueRange.Amplitude(); i++)
            {
                yield return valueRange.GetRandomValue();
            }
        }
    }
}
