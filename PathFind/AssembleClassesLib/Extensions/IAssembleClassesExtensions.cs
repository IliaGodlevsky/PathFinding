using AssembleClassesLib.Interface;
using System.Collections.Generic;
using System.Linq;

namespace AssembleClassesLib.Extensions
{
    public static class IAssembleClassesExtensions
    {
        /// <summary>
        /// Returns a collection of instances of <typeparamref name="T"/> 
        /// from <paramref name="assembleClasses"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assembleClasses"></param>
        /// <param name="ctorParams"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns a dictionary where class name is key and class instance is value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assembleClasses"></param>
        /// <returns></returns>
        public static IDictionary<string, T> AsNameInstanceDictionary<T>(
            this IAssembleClasses assembleClasses,
            params object[] ctorParams)
        {
            return assembleClasses
                .GetOfType<T>(ctorParams)
                .OrderByDescending(item => item.GetOrder())
                .AsNameInstanceDictionary();
        }
    }
}
