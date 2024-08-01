using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Algorithms.Exceptions;
using Pathfinding.Infrastructure.Data.Pathfinding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Infrastructure.Business.Extensions
{
    public static class EnumerableExtensions
    {
        public static IVertex PopOrThrowDeadEndVertexException(this Stack<IVertex> stack)
        {
            return stack.Count == 0 ? throw new DeadendVertexException() : stack.Pop();
        }

        public static IVertex DequeueOrThrowDeadEndVertexException(this Queue<IVertex> queue)
        {
            return queue.Count == 0 ? throw new DeadendVertexException() : queue.Dequeue();
        }

        public static IVertex FirstOrNullVertex(this IEnumerable<IVertex> collection, Func<IVertex, bool> predicate)
        {
            return collection.FirstOrDefault(predicate) ?? NullVertex.Interface;
        }
    }
}
