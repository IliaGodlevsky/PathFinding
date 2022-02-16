using System;

namespace GraphLib.Base.EndPoints.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class AttachmentAttribute : Attribute
    {
        private Type AttachedType { get; }

        public AttachmentAttribute(Type attachedTo)
        {
            AttachedType = attachedTo;
        }

        public bool IsAttachedTo(Type type)
        {
            return AttachedType.Equals(type);
        }
    }
}
