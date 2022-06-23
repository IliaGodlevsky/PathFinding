using Common.Attrbiutes;
using Common.Extensions;
using Common.Extensions.EnumerableExtensions;
using ConsoleVersion.Attributes;
using System;
using System.Linq;
using System.Reflection;

namespace ConsoleVersion.Model.Methods
{
    internal sealed class ValidationFailRouteMethods : CompanionMethods<Action, ValidationFailRouteAttribute>
    {
        public ValidationFailRouteMethods(object target) : base(target)
        {
        }

        public override Action GetMethods(MethodInfo targetMethod)
        {
            return GetMethodsInternal(targetMethod)
                .OrderBy(del => GetOrder(del.Method))
                .AggregateOrDefault((x, y) => (Action)Delegate.Combine(x, y));
        }

        private static int GetOrder(MethodInfo methodInfo)
        {
            return methodInfo.GetAttributeOrNull<OrderAttribute>()?.Order ?? OrderAttribute.Default.Order;
        }
    }
}
