using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using System.Collections.Generic;

namespace Algorithm.Extensions
{
    public static class StackExtension
    {
        public static IVertex PopOrNullVertex(this Stack<IVertex> stack)
        {
            return stack.Count == 0 ? NullVertex.Instance : stack.Pop();
        }
    }
}
