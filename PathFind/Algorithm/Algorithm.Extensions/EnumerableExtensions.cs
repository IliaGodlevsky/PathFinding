using System.Collections.Generic;
using System.Linq;
using Algorithm.Interfaces;
using Algorithm.Common.Exceptions;

namespace Algorithm.Extensions
{
    public static class EnumerableExtensions
    {
        public static Queue<T> ToQueue<T>(this IEnumerable<T> collection)
        {
            return new Queue<T>(collection);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        /// <exception cref="NoAlgorithmsLoadedException"/>
        public static Dictionary<string, IAlgorithm> CheckForEmptiness(this Dictionary<string, IAlgorithm> self)
        {
            if (!self.Any())
            {
                string message = "No algorithms were loaded";
                throw new NoAlgorithmsLoadedException(message);
            }
            return self;
        }
    }
}
