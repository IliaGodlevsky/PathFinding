using GraphLib.Vertex;
using GraphLib.Vertex.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Extensions
{
    internal static class StackExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stack"></param>
        /// <returns>if stack is empty returns NullVertex</returns>
        public static IVertex PopOrNullVertex(this Stack<IVertex> stack)
        {
            return !stack.AsParallel().Any() ? NullVertex.Instance : stack.Pop();
        }
    }
}
