using EnumerationValues.Realizations;
using NUnit.Framework;
using System.Linq;

namespace EnumerationValues.Tests
{
    [TestFixture]
    public class EnumValuesWithoutIgnoresTests
    {
        [Test]
        public void EnumValuesWithoutIgnored_ReturnValuesWithoutIgnored()
        {
            var values = new EnumValuesWithoutIgnored<TestEnum>();

            Assert.Multiple(() =>
            {
                Assert.True(values.Values.Count == 7);
                Assert.False(values.Values.Contains(TestEnum.All));
                Assert.False(values.Values.Contains(TestEnum.None));
            });
        }
    }
}