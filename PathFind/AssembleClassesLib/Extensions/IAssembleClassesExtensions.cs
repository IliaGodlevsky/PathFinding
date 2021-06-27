using AssembleClassesLib.Interface;
using System.Collections.Generic;

namespace AssembleClassesLib.Extensions
{
    public static class IAssembleClassesExtensions
    {
        public static IEnumerable<T> OfType<T>(this IAssembleClasses assembleClasses, 
            params object[] ctorParams)
        {
            foreach (var name in assembleClasses.ClassesNames)
            {
                if (assembleClasses.Get(name, ctorParams) is T value)
                {
                    yield return value;
                }
            }
        }
    }
}
