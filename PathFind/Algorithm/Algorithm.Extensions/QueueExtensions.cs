﻿using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Extensions
{
    public static class QueueExtensions
    {
        /// <summary>
        /// Removes vertex from the beginning of queue and returns it
        /// </summary>
        /// <param name="queue"></param>
        /// <returns>First vertex if collection isn't empty and <see cref="NullVertex"/> if is</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static IVertex DequeueOrDefault(this Queue<IVertex> queue)
        {
            return !queue.Any() ? new NullVertex() : queue.Dequeue();
        }

        public static Queue<T> EnqueueRange<T>(this Queue<T> queue, IEnumerable<T> range)
        {
            foreach (var item in range)
            {
                queue.Enqueue(item);
            }

            return queue;
        }

        public static Queue<IVertex> Except(this Queue<IVertex> queue, IEnumerable<IVertex> range)
        {
            return queue.Except<IVertex>(range).ToQueue();
        }
    }
}
