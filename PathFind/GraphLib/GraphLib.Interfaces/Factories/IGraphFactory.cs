using System.Collections.Generic;

namespace GraphLib.Interfaces.Factories
{
    public interface IGraphFactory
    {
        IGraph CreateGraph(IReadOnlyCollection<IVertex> vertices, int[] dimensionSizes);
    }
}
