using SearchAlgorythms.Top;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchAlgorythms.Algorythms.SearchAlgorythm
{
    public interface ISearchAlgorythm
    {
        bool IsDestination(Button button);
        void Visit(Button button);
        void ExtractNeighbours(Button button);
        void FindDestionation(GraphTop start);
    }
}
