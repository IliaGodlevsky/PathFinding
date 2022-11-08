using Pathfinding.AlgorithmLib.Core.NullObjects;
using Pathfinding.AlgorithmLib.Extensions;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.NullObjects;
using Priority_Queue;

namespace Pathfinding.AlgorithmLib.Extensions
{
    public static class SimplePriorityQueueExtensions
    {
        public static double GetPriorityOrInfinity(this SimplePriorityQueue<IVertex, double> self, IVertex vertex)
        {
            return self.TryGetPriority(vertex, out double value) ? value : double.PositiveInfinity;
        }

        public static IVertex TryFirstOrDefault(this SimplePriorityQueue<IVertex, double> self, IVertex Default)
        {
            return self.TryFirst(out var vertex) ? vertex : Default;
        }

        public static IVertex TryFirstOrNullVertex(this SimplePriorityQueue<IVertex, double> self)
        {
            return self.TryFirstOrDefault(NullVertex.Instance);
        }

        public static IVertex TryFirstOrDeadEndVertex(this SimplePriorityQueue<IVertex, double> self)
        {
            return self.TryFirstOrDefault(DeadEndVertex.Instance);
        }
    }
}
