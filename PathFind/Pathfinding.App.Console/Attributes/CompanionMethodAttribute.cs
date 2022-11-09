using System;

namespace Pathfinding.App.Console.Attributes
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
