using AssembleClassesLib.Attributes;
using Common.Extensions;
using System.Reflection;

namespace AssembleClassesLib.Extensions
{
    public static class MemberInfoExtensions
    {
        /// <summary>
        /// Checks, whether 
        /// <see cref="MemberInfo"/> is loadable
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsNotLoadable(this MemberInfo self)
        {
            return self.GetAttributeOrNull<NotLoadableAttribute>() != null;
        }
    }
}