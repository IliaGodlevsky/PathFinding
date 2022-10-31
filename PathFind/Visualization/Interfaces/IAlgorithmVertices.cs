using Algorithm.Interfaces;
using GraphLib.Interfaces;
using System.Collections.Generic;

namespace Visualization.Interfaces
{
    internal interface IAlgorithmVertices<TVertex>
        where TVertex : IVisualizable
    {
        IReadOnlyCollection<TVertex> GetVertices(IAlgorithm<IGraphPath> algorithm);
    }
}
