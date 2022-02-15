using GraphLib.Interfaces;
using GraphLib.NullRealizations;
using System.Collections.Generic;

namespace GraphLib.Extensions
{
    public static class IDictionaryExtensions
    {
        /// <summary>
        /// Tries to return a value for the <paramref name="key"/>
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <returns>A vertex if it exists 
        /// and <see cref="NullVertex"/> if doesn't</returns>
        public static IVertex GetOrNullVertex<TKey>(this IDictionary<TKey, IVertex> self, TKey key)
        {
            if (self.TryGetValue(key, out var value))
            {
                return value;
            }

            return NullVertex.Instance;
        }
    }
}
