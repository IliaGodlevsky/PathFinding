using AssembleClassesLib.Attributes;
using Common.Extensions;
using System.Reflection;

namespace AssembleClassesLib.Extensions
{
    public static class MemberInfoExtensions
    {
        public static bool IsFilterable(this MemberInfo self)
        {
            return self.GetAttribute<FilterableAttribute>() != null;
        }
    }
}