using GraphLib.Vertex;
using GraphLib.Vertex.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Extensions
{
    public static class StackExtension
    {
        internal static IVertex PopOrDefault(this Stack<IVertex> stack)
        {
            return !stack.Any() ? new DefaultVertex() : stack.Pop();
        }
    }
}
