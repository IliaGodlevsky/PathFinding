using AssembleClassesLib.Attributes;
using System;
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
            return Attribute.IsDefined(self, typeof(NotLoadableAttribute));
        }
    }
}