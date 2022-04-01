using System;

namespace ConsoleVersion.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class ExecuteSafeAttribute : BaseMethodAttribute
    {
        public ExecuteSafeAttribute(string methodName)
            : base(methodName)
        {

        }
    }
}
