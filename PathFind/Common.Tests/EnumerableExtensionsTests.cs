using Common.Extensions;
using NUnit.Framework;

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

        [Test]
        public void ContainsReferences_ArrayContainsElements_ReturnsTrue()
        {
            var object1 = new object();
            var object2 = new object();
            var object3 = new object();
            var object4 = new object();
            var refToObject1 = object1;
            var refToObject2 = object2;
            var objects = new[] { object1, object2, object3, object4 };

            bool containsAll = objects.ContainsReferences(refToObject1, refToObject2);

            Assert.IsTrue(containsAll);
        }

        [Test]
        public void ContainsReferences_ArrayDoesntContainsElements_ReturnsFalse()
        {
            string str1 = "Day";
            string str2 = "Night";
            string str3 = "Sun";
            string str4 = "Moon";
            string str5 = new string("Day".ToCharArray());
            string str6 = new string("Night".ToCharArray());
            var strings = new[] { str1, str2, str3, str4 };

            bool containsAll = strings.ContainsReferences(str5, str6);

            Assert.IsFalse(containsAll);
        }
    }
}
