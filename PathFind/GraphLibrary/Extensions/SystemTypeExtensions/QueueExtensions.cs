using GraphLibrary.Vertex;
using GraphLibrary.Vertex.Interface;
using System.Collections.Generic;
using System.Linq;

namespace GraphLibrary.Extensions.SystemTypeExtensions
{
    public static class QueueExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queue"></param>
        /// <returns>if queue is empty returns NullVertex</returns>
        public static IVertex DequeueOrNullVertex(this Queue<IVertex> queue)
        {
            return !queue.AsParallel().Any() ? NullVertex.Instance : queue.Dequeue();
        }
    }
}
