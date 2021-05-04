using AssembleClassesLib.Attributes;
using Common.Extensions;
using System.Reflection;

namespace AssembleClassesLib.Extensions
{
    public static class MemberInfoExtensions
    {
        public static bool IsNotLoadable(this MemberInfo self)
        {
            return self.GetAttribute<NotLoadableAttribute>() != null;
        }

        public static bool IsLoadable(this MemberInfo self)
        {
            return self.GetAttribute<LoadableAttribute>() != null;
        }
    }
}