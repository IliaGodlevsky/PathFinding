using GraphLib.Vertex;
using GraphLib.Vertex.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Extensions
{
    internal static class QueueExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queue"></param>
        /// <returns>if queue is empty returns NullVertex</returns>
        public static IVertex DequeueOrDefaultVertex(this Queue<IVertex> queue)
        {
            return !queue.Any() ? new DefaultVertex() : queue.Dequeue();
        }
    }
}
