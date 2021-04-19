using System;

namespace AssembleClassesLib.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class ClassNameAttribute : Attribute
    {
        public string Name { get; }

        public ClassNameAttribute(string name)
        {
            Name = name;
        }
    }
}