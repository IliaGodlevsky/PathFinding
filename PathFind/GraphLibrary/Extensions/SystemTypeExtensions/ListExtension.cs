using GraphLibrary.Vertex;
using GraphLibrary.Vertex.Interface;
using System;
using System.Collections.Generic;

namespace GraphLibrary.Extensions.SystemTypeExtensions
{
    public static class ListExtension
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
