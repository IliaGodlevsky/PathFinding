using GraphLibrary.Vertex;
using GraphLibrary.Vertex.Interface;
using System.Collections.Generic;
using System.Linq;

namespace GraphLibrary.Extensions.SystemTypeExtensions
{
    public static class StackExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stack"></param>
        /// <returns>if stack is empty returns NullVertex</returns>
        public static IVertex PopSecure(this Stack<IVertex> stack)
        {
            if (!stack.Any())
                return NullVertex.GetInstance();
            return stack.Pop();
        }
    }
}
