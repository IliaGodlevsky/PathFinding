using System;

namespace ConsoleVersion.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    internal sealed class ConditionAttribute : MethodAttribute
    {
        public ConditionAttribute(string methodName) : base(methodName)
        {

        }
    }
}
