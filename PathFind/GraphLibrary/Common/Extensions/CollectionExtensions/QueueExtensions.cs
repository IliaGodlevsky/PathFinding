using GraphLibrary.Model.Vertex;
using GraphLibrary.Vertex;
using System.Collections.Generic;
using System.Linq;

namespace GraphLibrary.Common.Extensions
{
    public static class QueueExtensions
    {
        public static void EnqueueRange<TSource>(this Queue<TSource> queue, 
            IEnumerable<TSource> collection)
        {
            foreach (var item in collection)           
                queue.Enqueue(item);           
        }

        public static IVertex DequeueSecure(this Queue<IVertex> queue)
        {
            if(!queue.Any())
                return NullVertex.GetInstance();
            return queue.Dequeue();
        }
    }
}
