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
        public void WalkTest()
        {
            var If = new If<TestObject>(t => t.IsSomething, IsSomethingBody)
                .ElseIf(t => t.IsSomethingElse, IsSomethingElseBody);
            var to = new TestObject
            {
                IsSomething = true
            };

            If.Walk(to);

            Assert.IsTrue(to.SomethingCount == SomethingBodyWorkedCode);
        }

        [Test]
        public void WalkTestWithElse()
        {
            var to = new TestObject();
            var If = new If<TestObject>(t => t.IsSomething, IsSomethingBody)
                .Else(ElseBody);

            If.Walk(to);

            Assert.IsTrue(to.SomethingCount == ElseBodyWorkedCode);
        }

        [Test]
        public void WalkTestWithFalseWalkCondition()
        {
            var to = new TestObject();
            var If = new If<TestObject>(t => t.IsSomething, IsSomethingBody)
                .Else(ElseBody);

            If.Walk(to, t => t.IsSomethingElse == true);

            Assert.IsTrue(to.SomethingCount == NoBodyWorkedCode);
        }

        [Test]
        public void ElseIfTestAfterElse_ThrowsInvalidOperationException()
        {
            var to = new TestObject();
            var If = new If<TestObject>(t => t.IsSomething, IsSomethingBody);

            Assert.Throws<InvalidOperationException>(() =>
            {
                If.Else(ElseBody)
                .ElseIf(t => t.IsSomethingElse, ElseBody);
            });
        }

        [Test]
        public void ElseIfTest_BodyIsNull_ThrowsArgumentNullException()
        {
            var to = new TestObject();
            var If = new If<TestObject>(t => t.IsSomething, IsSomethingBody);

            Assert.Throws<ArgumentNullException>(() =>
            {
                If.ElseIf(null, null);
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