using NUnit.Framework;
using System;

namespace Conditional.Tests
{
    [TestFixture]
    public class ConditionalTests
    {
        private const int NoBodyWorkedCode = 0;
        private const int SomethingBodyWorkedCode = 1;
        private const int SomethingElseBodyWorkedCode = 2;
        private const int ElseBodyWorkedCode = 3;

        public ConditionalTests()
        {

        }

        [Test]
        public void PerformFirstSuitableTest()
        {
            var If = new Conditional<TestObject>(IsSomethingBody, t => t.IsSomething)
                .PerformIf(IsSomethingElseBody, t => t.IsSomethingElse);
            var to = new TestObject
            {
                IsSomething = true
            };

            If.PerformFirstSuitable(to);

            Assert.IsTrue(to.SomethingCount == SomethingBodyWorkedCode);
        }

        [Test]
        public void PerformFirstSuitableTestWithFalseWalkCondition()
        {
            var to = new TestObject();
            var If = new Conditional<TestObject>(IsSomethingBody, t => t.IsSomething)
                .PerformIf(ElseBody);

            If.PerformFirstSuitable(to, t => t.IsSomethingElse == true);

            Assert.IsTrue(to.SomethingCount == NoBodyWorkedCode);
        }

        [Test]
        public void ElseIfTest_BodyIsNull_ThrowsArgumentNullException()
        {
            var to = new TestObject();
            var If = new Conditional<TestObject>();

            Assert.Throws<ArgumentNullException>(() =>
            {
                If.PerformIf(null, null);
            });
        }

        private void IsSomethingBody(TestObject to)
        {
            to.SomethingCount = SomethingBodyWorkedCode;
        }

        private void IsSomethingElseBody(TestObject to)
        {
            to.SomethingCount = SomethingElseBodyWorkedCode;
        }

        private void ElseBody(TestObject to)
        {
            to.SomethingCount = ElseBodyWorkedCode;
        }
    }
}