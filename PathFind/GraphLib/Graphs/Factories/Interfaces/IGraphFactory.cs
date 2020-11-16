using GraphLib.Coordinates.Interface;
using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex.Interface;
using System;
using System.Collections.Generic;

namespace GraphLib.Graphs.Factories.Interfaces
{
    public interface IGraphFactory
    {
        IGraph CreateGraph(Func<IVertex> vertexFactoryMethod, 
            Func<IEnumerable<int>, ICoordinate> coordinateFactoryMethod);
    }
}
