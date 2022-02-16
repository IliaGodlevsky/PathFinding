using System;

namespace GraphLib.Base.EndPoints.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class AttachedToAttribute : Attribute
    {
        private Type AttachedType { get; }

        public AttachedToAttribute(Type attachedTo)
        {
            AttachedType = attachedTo;
        }

        public bool IsAttachedTo(Type type)
        {
            return AttachedType.Equals(type);
        }
    }
}
