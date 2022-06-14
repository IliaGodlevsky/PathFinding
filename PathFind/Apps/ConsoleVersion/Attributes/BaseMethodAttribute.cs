using System;

namespace ConsoleVersion.Attributes
{
    internal abstract class MethodAttribute : Attribute
    {
        public string MethodName { get; }

        protected MethodAttribute(string methodName)
        {
            MethodName = methodName;
        }
    }
}
