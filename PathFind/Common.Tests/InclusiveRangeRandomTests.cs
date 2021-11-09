﻿using Common.Extensions;
using Common.ValueRanges;
using NUnit.Framework;

namespace Common.Tests
{
    public class InclusiveRangeRandomTests
    {
        [TestCase(-5, -10)]
        [TestCase(14, 0)]
        [TestCase(144, 12)]
        [TestCase(0, -13)]
        [TestCase(int.MaxValue, int.MaxValue - 1)]
        public void Next(int maxValue, int minValue)
        {
            var range = new InclusiveValueRange<int>(maxValue, minValue);
            int limit = 200;
            using (var random = new InclusiveRangeRandom())
            {
                while (limit-- > 0)
                {
                    int number = random.Next(range);
                    Assert.IsTrue(range.Contains(number));
                }
            }
        }

        [Test]
        public void NextDouble()
        {
            var range = new InclusiveValueRange<double>(1, 0);
            int limit = 200;
            using (var random = new InclusiveRangeRandom())
            {
                while (limit-- > 0)
                {
                    double number = random.NextDouble();
                    Assert.IsTrue(range.Contains(number));
                }
            }
        }
    }
}