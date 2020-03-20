using SearchAlgorythms.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorythms.GraphSaver
{
    public interface IGraphSaver
    {
        void SaveGraph(IGraph graph);
    }
}
