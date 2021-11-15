using GraphLib.Interfaces;
using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Extensions
{
    public static class SimplePriorityQueueExtensions
    {
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
            return vertices.Select(self.ToValueTuple);
        }
    }
}
