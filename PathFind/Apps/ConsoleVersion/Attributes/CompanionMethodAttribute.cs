using System;

namespace ConsoleVersion.Attributes
{
    internal abstract class CompanionMethodAttribute : Attribute
    {
        public string MethodName { get; }

        protected CompanionMethodAttribute(string methodName)
        {
            MethodName = methodName;
        }
    }
}
