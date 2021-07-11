using NullObject.Attributes;
using NullObject.Extensions;
using NUnit.Framework;

namespace NullObject.Tests
{

    class NotNullObject
    {

    }

    [Null]
    class NullObject
    {

    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void IsNullObject_NullObject_ReturnsTrue()
        {
            var nullObject = new NullObject();

            bool isNull = nullObject.IsNull();

            Assert.IsTrue(isNull);
        }

        [Test]
        public void IsNullObject_NotNullObject_ReturnsFalse()
        {
            var nullObject = new NotNullObject();

            bool isNull = nullObject.IsNull();

            Assert.IsFalse(isNull);
        }

        [Test]
        public void IsNullObject_Null_ReturnsTrue()
        {
            object nullObject = null;

            bool isNull = nullObject.IsNull();

            Assert.IsTrue(isNull);
        }
    }
}