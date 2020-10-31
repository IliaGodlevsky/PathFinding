using GraphLib.Vertex;
using GraphLib.Vertex.Interface;
using System;
using System.Collections.Generic;

namespace Algorithm.Extensions
{
    internal static class ListExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="match"></param>
        /// <returns>if no match returns NullVertex</returns>
        public static IVertex FindOrNullVertex(this List<IVertex> list, Predicate<IVertex> match)
        {
            return list.Find(match) ?? NullVertex.Instance;
        }
    }
}
