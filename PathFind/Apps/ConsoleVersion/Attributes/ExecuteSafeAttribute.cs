using System;

namespace ConsoleVersion.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class ExecuteSafeAttribute : Attribute
    {
        public string MethodName { get; }

        public ExecuteSafeAttribute(string methodName)
        {
            MethodName = methodName;
        }
    }
}
