using Common.Extensions.EnumerableExtensions;
using ConsoleVersion.Attributes;
using ConsoleVersion.Delegates;
using ConsoleVersion.Model.Methods;
using System;
using System.Reflection;

namespace ConsoleVersion.Model.Methods
{
    internal sealed class ConditionMethods : CompanionMethods<Condition, ConditionAttribute>
    {
        public ConditionMethods(object target) : base(target)
        {
        }

        public override Condition GetMethods(MethodInfo targetMethod)
        {
            return GetMethodsInternal(targetMethod).Combine();
        }
    }
}
