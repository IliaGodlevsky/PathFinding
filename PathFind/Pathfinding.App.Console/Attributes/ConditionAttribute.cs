using System;

namespace Pathfinding.App.Console.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    internal sealed class ConditionAttribute : CompanionMethodAttribute
    {
        public ConditionAttribute(string methodName) : base(methodName)
        {

        }
    }
}
