using Shared.Primitives.Attributes;
using System;

namespace Pathfinding.App.Console.Model.Menu.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    internal sealed class ConditionAttribute : OrderAttribute, IMethodMark
    {
        public string MethodName { get; }

        public ConditionAttribute(string methodName, int executionOrder = 0)
            : base(executionOrder)
        {
            MethodName = methodName;
        }
    }
}
