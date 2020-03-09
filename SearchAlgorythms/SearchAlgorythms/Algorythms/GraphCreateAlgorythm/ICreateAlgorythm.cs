using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchAlgorythms.Algorythms.GraphCreateAlgorythm
{
    public interface ICreateAlgorythm
    {
        List<Button> GetGraph(int x, int y);
        void SetNeighbours(int x, int y);
    }
}
