using Algorithm.Interfaces;
using GraphLib.Interfaces;
using System.Collections.Generic;

namespace GraphViewModel.Interfaces
{
    internal interface IProcessedVertices
    {
        IEnumerable<IVertex> GetVertices(IAlgorithm algorithm);
        void Add(IAlgorithm algorithm, IVertex vertex);
        void Remove(IAlgorithm algorithm);
        void Clear();
    }
}
