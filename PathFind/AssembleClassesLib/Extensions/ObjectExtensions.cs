using AssembleClassesLib.Attributes;
using Common.Extensions;

namespace AssembleClassesLib.Extensions
{
    public static class ObjectExtensions
    {
        public static string GetClassName(this object self)
        {
            return self?.GetType().GetAttributeOrNull<ClassNameAttribute>()?.Name
                ?? self?.GetType().FullName
                ?? string.Empty;
        }

        public static int GetOrder(this object self)
        {
            return self?.GetType().GetAttributeOrNull<ClassOrderAttribute>()?.Order ?? default;
        }
    }
}
