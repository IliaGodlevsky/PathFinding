using System;

namespace Pathfinding.App.Console.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class ExecuteSafeAttribute : CompanionMethodAttribute
    {
        public ExecuteSafeAttribute(string methodName) : base(methodName)
        {

        }
    }
}
