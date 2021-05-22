using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Extensions
{
    public static class StackExtension
    {
        public static IVertex PopOrDefault(this Stack<IVertex> stack)
        {
            return !stack.Any() ? new NullVertex() : stack.Pop();
        }
    }
}
