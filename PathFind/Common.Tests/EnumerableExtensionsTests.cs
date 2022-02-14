using Common.Extensions.EnumerableExtensions;
using NUnit.Framework;
using Random.Extensions;
using Random.Realizations.Generators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Tests
{
    [TestFixture]
    public class EnumerableExtensionsTests
    {
        [TestCase(new int[] { 1, 2, 7, 100, 15, 33, -1 }, new int[] { 1, 2, 7, 100, 15, 33, -1 })]
        public void Juxtapose_ArraysAreEqual_ComparesForEquality_ReturnsTrue(int[] firstArray, int[] secondArray)
        {
            bool matches = firstArray.Juxtapose(secondArray, (a, b) => a == b);

            Assert.IsTrue(matches);
        }

        [TestCase(new int[] { 1, 2, 7, 100, 15, 11, -1 }, new int[] { 1, 3, 7, 100, 15, 33, -1 })]
        public void Juxtapose_ArraysAreNotEqual_CpmparesForEquality_ReturnsFalse(int[] firstArray, int[] secondArray)
        {
            bool matches = firstArray.Juxtapose(secondArray, (a, b) => a == b);

            Assert.IsFalse(matches);
        }

        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7 }, new int[] { 2, 3, 4, 5, 6, 7, 8 })]
        public void Juxtapose_EachValueInFirstArrayIsLessThanInSecond_ComparesForLess_ReturnsTrue(
            int[] firstArray, int[] secondArray)
        {
            bool matches = firstArray.Juxtapose(secondArray, (a, b) => a < b);

            Assert.IsTrue(matches);
        }

        [TestCase(new int[] { }, new int[] { })]
        public void Juxtapose_EmptyCollections_ReturnsTrue(
            int[] firstArray, int[] secondArray)
        {
            bool matches = firstArray.Juxtapose(secondArray, (a, b) => a == b);

            Assert.IsTrue(matches);
        }

        [TestCase(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 })]
        public void Shuffle_OrderedCollection_ReturnShuffledCollection(int[] ordered)
        {
            var random = new PseudoRandom();
            var toOrder = ordered.ToArray();
            var unordered = toOrder.Shuffle(random.Next);

            Assert.IsFalse(unordered.Juxtapose(ordered));
        }

        [TestCase(10, new[] { 1, 2, 4, 5, 6, 5 })]
        [TestCase(6, new[] { 1, 2, 4, 5, 5 })]
        public void TakeOrDefault_TakeFromValues_RetunsValuesAndDefaults(int take, int[] values)
        {
            var taken = values.TakeOrDefault(take).ToArray();
            var notDefaultCount = taken.TakeWhile(i => i != default).Count();

            Assert.Multiple(() =>
            {
                Assert.IsTrue(taken.Length == take);
                Assert.IsTrue(notDefaultCount == values.Length);
            });
        }

        [TestCase(new[] { 5, 6, 7 }, new[] { 5, 6, 7, 1, 2, 3, 4 }, new[] { 1, 2, 3, 4 })]
        public void Without_ArrayContainingValues_ReturnValuesWithoutValues(int[] valuesToRemove,
            int[] values, int[] valuesWithout)
        {
            var without = values.Without(valuesToRemove);

            Assert.IsTrue(without.Juxtapose(valuesWithout));
        }
    }
}