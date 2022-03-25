using System;

namespace ConsoleVersion.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class ExecutionCheckMethodAttribute : Attribute
    {
        public string MethodName { get; }

        public ExecutionCheckMethodAttribute(string methodName)
        {
            MethodName = methodName;
        }
    }
}
