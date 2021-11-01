using System.Collections.Generic;

namespace Common.Extensions.EnumerableExtensions
{
    public static class QueueExtensions
    {
        public static Queue<T> EnqueueRange<T>(this Queue<T> queue, IEnumerable<T> range)
        {
            range.ForEach(queue.Enqueue);
            return queue;
        }

        public static Queue<T> Except<T>(this Queue<T> queue, IEnumerable<T> range)
        {
            return queue.Except(range).ToQueue();
        }

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
