using SearchAlgorythms.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorythms.GraphLoader
{
    public interface IGraphLoader
    {
        IGraph GetGraph();
    }
}
