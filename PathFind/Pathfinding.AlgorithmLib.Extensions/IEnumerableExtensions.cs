using Pathfinding.AlgorithmLib.Core.NullObjects;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.NullObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.AlgorithmLib.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IVertex PopOrDeadEndVertex(this Stack<IVertex> stack)
        {
            return stack.Count == 0 ? DeadEndVertex.Interface : stack.Pop();
        }

        public static IVertex DequeueOrDeadEndVertex(this Queue<IVertex> queue)
        {
            return queue.Count == 0 ? DeadEndVertex.Interface : queue.Dequeue();
        }

        public static IVertex FirstOrNullVertex(this IEnumerable<IVertex> collection, Func<IVertex, bool> predicate)
        {
            return collection.FirstOrDefault(predicate) ?? NullVertex.Interface;
        }
    }
}
