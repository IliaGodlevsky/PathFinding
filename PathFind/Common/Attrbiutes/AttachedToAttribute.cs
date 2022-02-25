using System;

namespace Common.Attrbiutes
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class AttachedToAttribute : Attribute
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
