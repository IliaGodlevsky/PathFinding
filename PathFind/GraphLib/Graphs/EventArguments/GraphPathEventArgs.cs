using GraphLib.Vertex.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLib.Graphs.EventArguments
{
    public class GraphPathEventArgs : EventArgs
    {
        public GraphPathEventArgs(IVertex vertex)
        {
            Vertex = vertex;
        }

        public IVertex Vertex { get; private set; }
    }
}
