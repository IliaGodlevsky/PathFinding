using System;

namespace GraphLib.Base.EndPoints.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class AttachmentAttribute : Attribute
    {
        public Type CommandsType { get; }

        public AttachmentAttribute(Type type)
        {
            CommandsType = type;
        }

        public bool IsCommandType(Type type)
        {
            return CommandsType.Equals(type);
        }
    }
}
