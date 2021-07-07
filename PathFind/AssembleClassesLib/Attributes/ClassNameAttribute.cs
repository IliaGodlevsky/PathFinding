using System;

namespace AssembleClassesLib.Attributes
{
    /// <summary>
    /// An attribute that  replaces the class 
    /// name with the one specified in the 
    /// property <see cref="Name"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct, Inherited = false)]
    public sealed class ClassNameAttribute : Attribute
    {
        public string Name { get; }

        public ClassNameAttribute(string name)
        {
            Name = name;
        }
    }
}