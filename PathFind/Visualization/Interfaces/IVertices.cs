using Algorithm.Interfaces;
using GraphLib.Interfaces;
using System.Collections.Generic;

namespace Visualization.Interfaces
{
    internal interface IVertices
    {
        IEnumerable<IVertex> GetVertices(IAlgorithm algorithm);
        void Add(IAlgorithm algorithm, IVertex vertex);
        void Clear();
        void Remove(IAlgorithm algorithm);
    }
}
