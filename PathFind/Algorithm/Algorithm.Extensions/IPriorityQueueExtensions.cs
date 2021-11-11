using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using Priority_Queue;
using System;
using System.Collections.Generic;

namespace Algorithm.Extensions
{
    public static class IPriorityQueueExtensions
    {
        public static void EnqueueOrUpdatePriority<TItem, TPriority>(this IPriorityQueue<TItem, TPriority> self, TItem node, TPriority priority)
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

        public static IVertex DequeueOrNullVertex(this IPriorityQueue<IVertex, double> self)
        {
            return self.Count == 0 ? NullVertex.Instance : self.Dequeue();
        }

        public static void RemoveRange<TItem, TPriority>(this IPriorityQueue<TItem, TPriority> self, IEnumerable<TItem> nodes)
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

        public static ValueTuple<IVertex, double> ToValueTuple(this SimplePriorityQueue<IVertex, double> self, IVertex item)
        {
            return new ValueTuple<IVertex, double>(item, self.GetPriorityOrInfinity(item));
        }

        public static double GetPriorityOrInfinity(this SimplePriorityQueue<IVertex, double> self, IVertex vertex)
        {
            return self.TryGetPriority(vertex, out double value) ? value : double.PositiveInfinity;
        }

        public static IEnumerable<ValueTuple<IVertex, double>> ToValueTuples(this SimplePriorityQueue<IVertex, double> self,
            IEnumerable<IVertex> vertices)
        {
            foreach (var vertex in vertices)
            {
                yield return self.ToValueTuple(vertex);
            }
        }
    }
}
