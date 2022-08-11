using System;

namespace ConsoleVersion.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    internal sealed class ConditionAttribute : CompanionMethodAttribute
    {
        public ConditionAttribute(string methodName) : base(methodName)
        {

        }
    }
}
