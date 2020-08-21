using GraphLibrary.Vertex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.Model
{
    public interface IGraphField
    {
        void Add(IVertex vertex, int xCoordinate, int yCoordinate);
    }
}
