using AssembleClassesLib.Attributes;
using Common.Extensions;
using System.Reflection;

namespace AssembleClassesLib.Extensions
{
    public static class MemberInfoExtensions
    {
        public static bool IsNotLoadable(this MemberInfo self)
        {
            return self.GetAttributeOrNull<NotLoadableAttribute>() != null;
        }

        public static bool IsLoadable(this MemberInfo self)
        {
            return self.GetAttributeOrNull<LoadableAttribute>() != null;
        }
    }
}