using Priority_Queue;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Extensions
{
    public static class PriorityQueueExtensions
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

        public static void EnqueueOrUpdatePriority<TItem, TPriority>(this IPriorityQueue<TItem, TPriority> self,
            KeyValuePair<TItem, TPriority> item)
        {
            self.EnqueueOrUpdatePriority(item.Key, item.Value);
        }
    }
}
