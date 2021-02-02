using GraphLib.Interface;
using GraphLib.NullObjects;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Extensions
{
    internal static class StackExtension
    {
        internal static IVertex PopOrDefault(this Stack<IVertex> stack)
        {
            return !stack.Any() ? new DefaultVertex() : stack.Pop();
        }
    }
}
