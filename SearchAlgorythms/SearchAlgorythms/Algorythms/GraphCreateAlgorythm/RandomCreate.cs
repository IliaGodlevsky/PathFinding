using SearchAlgorythms.Top;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchAlgorythms.Algorythms.GraphCreateAlgorythm
{
    public class RandomCreate : ICreateAlgorythm
    {
        private Random rand = new Random();
        private List<Button> graph = new List<Button>();
        public List<Button> GetGraph(int x, int y)
        {
            int limit = x * y;
            for (int i = 0; i < limit; i++)
                graph.Add(new GraphTop());
            SetNeighbours(x, y);
            return graph;           
        }

        public void SetNeighbours(int x, int y)
        {
            for (int i = 0; i < x; i++) 
            {
                for (int j = 0; j < y; j++)
                {
                    
                }
            }
        }
    }
}
