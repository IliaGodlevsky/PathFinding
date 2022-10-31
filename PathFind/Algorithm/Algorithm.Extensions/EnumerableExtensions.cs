using Algorithm.NullRealizations;
using GraphLib.Interfaces;
using System.Collections.Generic;

namespace Algorithm.Extensions
{
    public static class EnumerableExtensions
    {
        public static IVertex PopOrDeadEndVertex(this Stack<IVertex> stack)
        {
            return stack.Count == 0 ? DeadEndVertex.Interface : stack.Pop();
        }
    }
}
