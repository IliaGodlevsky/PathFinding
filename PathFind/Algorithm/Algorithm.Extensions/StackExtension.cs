using GraphLib.Common.NullObjects;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Extensions
{
    public static class StackExtension
    {
        public static IVertex PopOrDefault(this Stack<IVertex> stack)
        {
            return !stack.Any() ? new DefaultVertex() : stack.Pop();
        }
    }
}
