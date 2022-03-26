using System;

namespace ConsoleVersion.Attributes
{
    internal abstract class BaseMethodAttribute : Attribute
    {
        public string MethodName { get; }

        protected BaseMethodAttribute(string methodName)
        {
            MethodName = methodName;
        }
    }
}
