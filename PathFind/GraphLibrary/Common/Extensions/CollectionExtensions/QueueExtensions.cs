using System.Collections.Generic;

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
    }
}
