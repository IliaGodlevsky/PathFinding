using GraphLib.Vertex;
using GraphLib.Vertex.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Extensions
{
    public static class StackExtension
    {
        internal static IVertex PopOrDefaultVertex(this Stack<IVertex> stack)
        {
            return !stack.Any() ? new DefaultVertex() : stack.Pop();
        }
    }
}
