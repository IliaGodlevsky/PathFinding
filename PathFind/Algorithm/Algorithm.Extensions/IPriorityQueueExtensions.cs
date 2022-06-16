using Priority_Queue;

namespace Algorithm.Extensions
{
    public static class IPriorityQueueExtensions
    {
        public static void EnqueueOrUpdatePriority<TItem, TPriority>(this IPriorityQueue<TItem, TPriority> self,
            TItem node, TPriority priority)
        {
            if (self.Contains(node))
            {
                self.UpdatePriority(node, priority);
            }
            else
            {
                self.Enqueue(node, priority);
            }
        }
    }
}
