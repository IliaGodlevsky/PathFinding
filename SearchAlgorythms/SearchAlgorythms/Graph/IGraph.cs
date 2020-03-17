using SearchAlgorythms.Top;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchAlgorythms.Graph
{
    public interface IGraph
    {
        int Width();
        int Height();
        void Insert();
        Button this[int i, int j] { get;set; }
        GraphTop Start { get; set; }
        GraphTop End { get; set; }
    }
}
