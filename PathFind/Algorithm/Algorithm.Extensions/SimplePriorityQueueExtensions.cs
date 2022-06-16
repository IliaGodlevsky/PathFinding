using GraphLib.Interfaces;
using Priority_Queue;

namespace Algorithm.Extensions
{
    public static class SimplePriorityQueueExtensions
    {
        public static double GetPriorityOrInfinity(this SimplePriorityQueue<IVertex, double> self, IVertex vertex)
        {
            return self.TryGetPriority(vertex, out double value) ? value : double.PositiveInfinity;
        }
    }
}
