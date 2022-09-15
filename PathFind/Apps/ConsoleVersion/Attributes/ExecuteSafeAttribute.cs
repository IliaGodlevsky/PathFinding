using System;

namespace ConsoleVersion.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class ExecuteSafeAttribute : CompanionMethodAttribute
    {
        public ExecuteSafeAttribute(string methodName) : base(methodName)
        {

        }
    }
}
