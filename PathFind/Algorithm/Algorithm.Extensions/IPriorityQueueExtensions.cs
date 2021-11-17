using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Extensions
{
    public static class IPriorityQueueExtensions
    {
        public static void EnqueueOrUpdatePriority<TItem, TPriority>(this IPriorityQueue<TItem, TPriority> self,
            TItem node, TPriority priority)
        {
            if (self.Contains(node))
            {
                self.UpdatePriority(node, priority);
            }
            else
            {
                self.Enqueue(node, priority);
            }
        }

        public static IVertex FirstOrNullVertex(this IPriorityQueue<IVertex, double> self)
        {
            return self.Count == 0 ? NullVertex.Instance : self.First;
        }

        public static void RemoveRange<TItem, TPriority>(this IPriorityQueue<TItem, TPriority> self,
            IEnumerable<TItem> nodes)
        {
            nodes.ForEach(self.RemoveIfContains);
        }

        public static void RemoveIfContains<TItem, TPriority>(this IPriorityQueue<TItem, TPriority> self, TItem node)
        {
            if (self.Contains(node))
            {
                self.Remove(node);
            }
        }

        public static void EnqueueOrUpdateRange<TItem, TPriority>(this IPriorityQueue<TItem, TPriority> self,
            IEnumerable<ValueTuple<TItem, TPriority>> nodes)
        {
            foreach (var node in nodes)
            {
                self.EnqueueOrUpdatePriority(node.Item1, node.Item2);
            }
        }

        public static IList<TItem> TakeOrderedBy<TItem, TPriority>(this IEnumerable<TItem> self, int take,
            Func<TItem, TPriority> selector)
        {
            return self.OrderByDescending(selector).Take(take).ToList();
        }
    }
}
