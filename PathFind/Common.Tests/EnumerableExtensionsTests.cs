﻿using Common.Extensions;
using NUnit.Framework;

namespace Common.Tests
{
    [TestFixture]
    public class EnumerableExtensionsTests
    {
        [TestCase(new int[] { 1, 2, 7, 100, 15, 33, -1 }, new int[] { 1, 2, 7, 100, 15, 33, -1 })]
        public void Match_ArraysAreEqual_ComparesForEquality_ReturnsTrue(int[] firstArray, int[] secondArray)
        {
            bool matches = firstArray.Match(secondArray, (a, b) => a == b);

            Assert.IsTrue(matches);
        }

        [TestCase(new int[] { 1, 2, 7, 100, 15, 11, -1 }, new int[] { 1, 3, 7, 100, 15, 33, -1 })]
        public void Match_ArraysAreNotEqual_CpmparesForEquality_ReturnsFalse(int[] firstArray, int[] secondArray)
        {
            bool matches = firstArray.Match(secondArray, (a, b) => a == b);

            Assert.IsFalse(matches);
        }

        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7 }, new int[] { 2, 3, 4, 5, 6, 7, 8 })]
        public void Match_EachValueInFirstArrayIsLessThanInSecond_ComparesForLess_ReturnsTrue(
            int[] firstArray, int[] secondArray)
        {
            bool matches = firstArray.Match(secondArray, (a, b) => a < b);

            Assert.IsTrue(matches);
        }
    }
}
