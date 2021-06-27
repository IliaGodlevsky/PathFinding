using System;

namespace AssembleClassesLib.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
    public sealed class ClassOrderAttribute : Attribute
    {
        public ClassOrderAttribute(int order)
        {
            Order = order;
        }

        public int Order { get; }
    }
}
