using NUnit.Framework;
using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange;
using Shared.Random.Extensions;
using System.Linq;

namespace Shared.Random.Tests
{
    [TestFixture]
    public class RandomNumberGeneratorsTests
    {        
        [TestCaseSource(typeof(NextUintMethodTestCaseData), nameof(NextUintMethodTestCaseData.Data))]
        public void NextUint_ReturnUniqueNumbers(IRandom random, int iterations, int tolerance)
        {
            var failMessage = "The sequence of numbers is not unique";
            var array = new uint[iterations];

            while (iterations-- > 0)
            {
                array[iterations] = random.NextUInt();
            }
            var uniqueValues = array.Distinct().ToArray();

            Assert.AreEqual(array.Length, uniqueValues.Length, tolerance, failMessage);
        }

        [TestCaseSource(typeof(NextIntMethodTestCaseData), nameof(NextIntMethodTestCaseData.Data))]
        public void NextInt_ReturnsNumbersWithinRange(IRandom random, int iterations, InclusiveValueRange<int> range)
        {
            var failMessage = $"An array contains values that are out of the range {range}";
            var array = new int[iterations];

            while (iterations-- > 0)
            {
                array[iterations] = random.NextInt(range);
            }
            bool isAllInRange = array.All(value => range.Contains(value));

            Assert.IsTrue(isAllInRange, failMessage);
        }
    }
}