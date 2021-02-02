using GraphLib.Interface;
using GraphLib.NullObjects;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Extensions
{
    internal static class QueueExtensions
    {
        /// <summary>
        /// Removes vertex from the begining of queue and return it
        /// </summary>
        /// <param name="queue"></param>
        /// <returns>First vertex if coolection isn't empty and <see cref="DefaultVertex"/> if is</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        internal static IVertex DequeueOrDefault(this Queue<IVertex> queue)
        {
            return !queue.Any() ? new DefaultVertex() : queue.Dequeue();
        }
    }
}
