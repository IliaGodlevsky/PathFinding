using System;

namespace ConsoleVersion.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    internal sealed class PreValidationMethodAttribute : Attribute
    {
        public string MethodName { get; }

        public PreValidationMethodAttribute(string methodName)
        {
            MethodName = methodName;
        }
    }
}
