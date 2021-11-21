using GraphLib.Interfaces;
using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Extensions
{
    public static class SimplePriorityQueueExtensions
    {
        public static Tuple<IVertex, double> ToTuple(this SimplePriorityQueue<IVertex, double> self, IVertex item,
            Func<IVertex, double> heuristic)
        {
            return new Tuple<IVertex, double>(item, self.GetPriorityOrInfinity(item));
        }

        public static double GetPriorityOrInfinity(this SimplePriorityQueue<IVertex, double> self, IVertex vertex)
        {
            return self.TryGetPriority(vertex, out double value) ? value : double.PositiveInfinity;
        }

        public static IEnumerable<Tuple<IVertex, double>> ToTuples(this SimplePriorityQueue<IVertex, double> self,
            IEnumerable<IVertex> vertices, Func<IVertex, double> heuristic)
        {
            return vertices.Select(vertex => self.ToTuple(vertex, heuristic));
        }
    }
}
