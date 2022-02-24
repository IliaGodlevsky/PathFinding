using Commands.Attributes;
using Common.Extensions;
using System;

namespace Commands.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsAttachedTo<T>(this Type type, T attachedTo)
        {
            var attribute = type.GetAttributeOrNull<AttachedToAttribute>();
            return attribute?.IsAttachedTo(attachedTo.GetType()) == true;
        }
    }
}
