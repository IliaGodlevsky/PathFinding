using System;

namespace ConsoleVersion.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    internal sealed class ValidateAttribute : MethodAttribute
    {
        public ValidateAttribute(string methodName)
            : base(methodName)
        {

        }
    }
}
