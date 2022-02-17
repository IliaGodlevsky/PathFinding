using Common.Extensions;
using GraphLib.Base.EndPoints.Attributes;
using System;

namespace GraphLib.Base.EndPoints.Extensions
{
    internal static class TypeExtensions
    {
        public static bool IsAttachedTo<T>(this Type type, T attachedTo)
        {
            var attribute = type.GetAttributeOrNull<AttachedToAttribute>();
            return attribute?.IsAttachedTo(attachedTo.GetType()) == true;
        }
    }
}
