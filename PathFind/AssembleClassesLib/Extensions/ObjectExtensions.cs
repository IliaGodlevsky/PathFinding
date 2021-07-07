using AssembleClassesLib.Attributes;
using Common.Extensions;

namespace AssembleClassesLib.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Returns the class's name of <paramref name="self"/>
        /// that specified in <see cref="ClassNameAttribute"/>
        /// or class's full name if <paramref name="self"/> is 
        /// not marked with <see cref="ClassNameAttribute"/>
        /// or empty string if <paramref name="self"/> is null
        /// </summary>
        /// <param name="self"></param>
        /// <returns>A string that represents thaclass's name</returns>
        public static string GetClassName(this object self)
        {
            return self?.GetType().GetAttributeOrNull<ClassNameAttribute>()?.Name
                ?? self?.GetType().FullName
                ?? string.Empty;
        }

        /// <summary>
        /// Returns class's order that specified 
        /// in <see cref="ClassOrderAttribute"/>
        /// or <see cref="default(int)"/> if class 
        /// is not marked with <see cref="ClassOrderAttribute"/>
        /// </summary>
        /// <param name="self"></param>
        /// <returns>A number that represents class's order</returns>
        public static int GetOrder(this object self)
        {
            return self?.GetType().GetAttributeOrNull<ClassOrderAttribute>()?.Order ?? default;
        }
    }
}
