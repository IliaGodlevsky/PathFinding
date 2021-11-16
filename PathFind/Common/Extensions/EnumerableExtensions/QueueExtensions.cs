using System.Collections.Generic;

namespace Common.Extensions.EnumerableExtensions
{
    public static class QueueExtensions
    {
        public static bool Remove<T>(this Queue<T> queue, T item)
        {
            bool isRemoved = false;
            if (queue.Contains(item))
            {
                var items = new LinkedList<T>(queue);
                queue.Clear();
                isRemoved = items.Remove(item);
                items.ForEach(queue.Enqueue);
            }
            return isRemoved;
        }
    }
}
