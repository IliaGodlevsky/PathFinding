using Pathfinding.AlgorithmLib.Core.Exceptions;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.NullObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.AlgorithmLib.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IVertex PopOrThrowDeadEndVertexException(this Stack<IVertex> stack)
        {
            return stack.Count == 0 ? throw new DeadendVertexException() : stack.Pop();
        }

        public static IVertex DequeueOrDeadEndVertex(this Queue<IVertex> queue)
        {
            return queue.Count == 0 ? throw new DeadendVertexException() : queue.Dequeue();
        }

        public static IVertex FirstOrNullVertex(this IEnumerable<IVertex> collection, Func<IVertex, bool> predicate)
        {
            return collection.FirstOrDefault(predicate) ?? NullVertex.Interface;
        }
    }
}
