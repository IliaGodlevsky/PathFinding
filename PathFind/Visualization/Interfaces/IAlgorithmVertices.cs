using Algorithm.Interfaces;
using GraphLib.Interfaces;
using System.Collections.Generic;

namespace Visualization.Interfaces
{
    internal interface IAlgorithmVertices
    {
        IReadOnlyCollection<IVertex> GetVertices(IAlgorithm algorithm);
    }
}
