using Common.Extensions;
using NUnit.Framework;
using System;

namespace Common.Tests
{
    [TestFixture]
    public class MemberInfoExtensionsTests
    {
        class ActionMethods
        {
            public void SomeAction()
            {

            }
        }

        class FuncMethods
        {
            public int SomeParamFunc(object obj)
            {
                return obj.GetHashCode();
            }
        }
        
        class PredicateMethods
        {           
            public bool SomePredicate(object obj)
            {
                return obj.Equals(obj);
            }
        }

        [Test]
        public void TryCreateDelegate_ClassWithPredicateMethod_ReturnsTrue()
        {
            var obj = new PredicateMethods();
            var method = obj.GetType().GetMethod(nameof(obj.SomePredicate));

            bool canCreate = method.TryCreateDelegate(obj, out Predicate<object> del);

            Assert.IsTrue(canCreate);
        }

        [Test]
        public void TryCreateDelegate_ClassWithFuncMethodWithParam_ReturnsTrue()
        {
            var obj = new FuncMethods();
            var method = obj.GetType().GetMethod(nameof(obj.SomeParamFunc));

            bool canCreate = method.TryCreateDelegate(obj, out Func<object, int> del);

            Assert.IsTrue(canCreate);
        }

        [Test]
        public void TryCreateDelegate_ClassWithActionMethod_ReturnsTrue()
        {
            var obj = new ActionMethods();
            var method = obj.GetType().GetMethod(nameof(obj.SomeAction));

            bool canCreate = method.TryCreateDelegate(obj, out Action del);

            Assert.IsTrue(canCreate);
        }

        [Test]
        public void TryCreateDelegate_ClassWithActionMethod_DelegateTypeIsWrong_ReturnsFalse()
        {
            var obj = new ActionMethods();
            var method = obj.GetType().GetMethod(nameof(obj.SomeAction));

            bool canCreate = method.TryCreateDelegate(obj, out Predicate<int> del);

            Assert.IsFalse(canCreate);
        }
    }
}
