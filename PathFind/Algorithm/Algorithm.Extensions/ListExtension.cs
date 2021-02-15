using GraphLib.Common.NullObjects;
using GraphLib.Interface;
using System;
using System.Collections.Generic;

namespace Algorithm.Extensions
{
    public static class ListExtension
    {
        /// <summary>
        /// Finds the first element that suits the predicate
        /// </summary>
        /// <param name="list"></param>
        /// <param name="match"></param>
        /// <returns>Suitable vertex or <see cref="DefaultVertex"/> if not found</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IVertex FindOrDefault(this List<IVertex> list, Predicate<IVertex> match)
        {
            return list.Find(match) ?? new DefaultVertex();
        }
    }
}
