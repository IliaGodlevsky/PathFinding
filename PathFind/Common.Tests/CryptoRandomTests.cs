using NUnit.Framework;

namespace Common.Tests
{
    public class CryptoRandomTests
    {
        [TestCase(-5, -10)]
        [TestCase(14, 0)]
        [TestCase(144, 12)]
        [TestCase(0, -13)]
        public void Next(int maxValue, int minValue)
        {
            int limit = 200;
            while (limit-- > 0)
            {
                using (var random = new CryptoRandom())
                {
                    int number = random.Next(minValue, maxValue);
                    Assert.IsTrue(number >= minValue && number <= maxValue);
                }
            }
        }

        [Test]
        public void NextDouble()
        {
            using (var random = new CryptoRandom())
            {
                double number = random.NextDouble();
                Assert.IsTrue(number >= 0.0 && number <= 1.0);
            }
        }
    }
}
