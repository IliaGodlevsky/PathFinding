using System.Collections.Generic;

namespace GraphLib.Interfaces.Factories
{
    public interface IGraphFactory
    {
        IGraph CreateGraph(IEnumerable<IVertex> vertices, int[] dimensionSizes);
    }
}
