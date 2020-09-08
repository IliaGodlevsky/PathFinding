using GraphLibrary.Model.Vertex;
using GraphLibrary.Vertex;
using System.Collections.Generic;
using System.Linq;

namespace GraphLibrary.Common.Extensions.CollectionExtensions
{
    public static class StackExtension
    {

        public static IVertex PopSecure(this Stack<IVertex> stack)
        {
            if (!stack.Any())
                return NullVertex.GetInstance();
            return stack.Pop();
        }
    }
}
