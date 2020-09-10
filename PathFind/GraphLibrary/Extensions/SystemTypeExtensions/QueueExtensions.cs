﻿using GraphLibrary.Vertex;
using GraphLibrary.Vertex.Interface;
using System.Collections.Generic;
using System.Linq;

namespace GraphLibrary.Extensions.SystemTypeExtensions
{
    public static class QueueExtensions
    {
        public static void EnqueueRange<TSource>(this Queue<TSource> queue,
            IEnumerable<TSource> collection)
        {
            foreach (var item in collection)
                queue.Enqueue(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queue"></param>
        /// <returns>if queue is empty returns NullVertex</returns>
        public static IVertex DequeueOrNullVertex(this Queue<IVertex> queue)
        {
            return !queue.Any() ? NullVertex.Instance : queue.Dequeue();
        }
    }
}