using GraphLib.Coordinates.Abstractions;
using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex.Interface;
using System;
using System.Collections.Generic;

namespace GraphLib.Graphs.Factories.Interfaces
{
    public interface IGraphFactory
    {
        event Action<string> OnExceptionCaught;

        IGraph CreateGraph(Func<IVertex> vertexFactoryMethod,
            Func<IEnumerable<int>, ICoordinate> coordinateFactoryMethod);
    }
}
