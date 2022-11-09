using Pathfinding.App.Console.Attributes;
using Pathfinding.App.Console.Delegates;
using Shared.Extensions;
using System;
using System.Reflection;

namespace Pathfinding.App.Console.Model.Methods
{
    internal sealed class ConditionMethods : CompanionMethods<Condition, ConditionAttribute>
    {
        public ConditionMethods(object target) : base(target)
        {
        }

        public override Condition GetMethods(MethodInfo targetMethod)
        {
            return (Condition)GetMethodsInternal(targetMethod)
                .AggregateOrDefault<Delegate>(Delegate.Combine);
        }
    }
}
