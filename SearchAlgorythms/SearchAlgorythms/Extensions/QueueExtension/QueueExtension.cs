using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorythms.Extensions.QueueExtension
{
    public static class QueueExtension
    {
        public static bool IsEmpty<TSource>(this Queue<TSource> queue)
        {
            return queue.Count == 0;
        }
    }
}
