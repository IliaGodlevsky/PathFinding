using System;

namespace ConsoleVersion.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    internal sealed class PreValidationMethodAttribute : BaseMethodAttribute
    {
        public PreValidationMethodAttribute(string methodName)
            : base(methodName)
        {

        }
    }
}
