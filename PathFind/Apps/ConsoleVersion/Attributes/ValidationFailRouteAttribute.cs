using System;

namespace ConsoleVersion.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    internal sealed class ValidationFailRouteAttribute : MethodAttribute
    {
        public ValidationFailRouteAttribute(string methodName) : base(methodName)
        {
           
        }
    }
}
