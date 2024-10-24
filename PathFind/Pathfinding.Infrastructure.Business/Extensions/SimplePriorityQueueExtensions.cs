﻿using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Algorithms.Exceptions;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Priority_Queue;

namespace Pathfinding.Infrastructure.Business.Extensions
{
    public static class SimplePriorityQueueExtensions
    {
        public static double GetPriorityOrInfinity(this SimplePriorityQueue<IVertex, double> self, IVertex vertex)
        {
            return self.TryGetPriority(vertex, out double value) ? value : double.PositiveInfinity;
        }

        public static IVertex TryFirstOrNullVertex(this SimplePriorityQueue<IVertex, double> self)
        {
            return self.TryFirst(out var vertex) ? vertex : NullVertex.Instance;
        }

        public static IVertex TryFirstOrThrowDeadEndVertexException(this SimplePriorityQueue<IVertex, double> self)
        {
            return self.TryFirst(out var vertex) ? vertex : throw new DeadendVertexException();
        }
    }
}
