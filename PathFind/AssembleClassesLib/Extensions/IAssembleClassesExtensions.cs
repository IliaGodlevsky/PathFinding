using AssembleClassesLib.Interface;
using System.Collections.Generic;
using System.Linq;

namespace AssembleClassesLib.Extensions
{
    public static class IAssembleClassesExtensions
    {
        public static IEnumerable<T> GetOfType<T>(this IAssembleClasses assembleClasses,
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

        public static IDictionary<string, T> AsNameInstanceDictionary<T>(this IAssembleClasses assembleClasses)
        {
            return assembleClasses
                .GetOfType<T>()
                .OrderByDescending(item=>item.GetOrder())
                .AsNameInstanceDictionary();
        }
    }
}
