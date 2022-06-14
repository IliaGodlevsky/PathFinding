using System;

namespace ConsoleVersion.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class ExecuteSafeAttribute : MethodAttribute
    {
        public ExecuteSafeAttribute(string methodName)
            : base(methodName)
        {

        }
    }
}
