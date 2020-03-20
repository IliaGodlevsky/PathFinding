using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchAlgorythms.Extensions.ListExtensions
{
    public static class ListExtension
    {
        public static void Shuffle<TSource>(this List<TSource> list)
        {
            Random rand = new Random();
            for (int i = 0; i < list.Count; i++) 
            {
                int a = rand.Next(list.Count);
                int b = rand.Next(list.Count);
                TSource temp = list[a];
                list[a] = list[b];
                list[b] = temp;
            }
        }
    }
}
