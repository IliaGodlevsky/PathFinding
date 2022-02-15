using GraphLib.Interfaces;
using GraphLib.NullRealizations;
using System.Collections.Generic;

namespace GraphLib.Extensions
{
    public static class StackExtension
    {
        public static IVertex PopOrNullVertex(this Stack<IVertex> stack)
        {
            return stack.Count == 0 ? NullVertex.Instance : stack.Pop();
        }
    }
}
