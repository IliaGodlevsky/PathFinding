using Common.Extensions;
using GraphLib.Base.EndPoints.Attributes;
using System;

namespace GraphLib.Base.EndPoints.Extensions
{
    internal static class TypeExtensions
    {
        public static bool IsAttachedCommand(this Type type, Type commandsType)
        {
            var attribute = type.GetAttributeOrNull<AttachmentAttribute>();
            return attribute?.IsCommandType(commandsType) == true;
        }
    }
}
