using Algorithm.Interfaces;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using GraphViewModel.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphViewModel.Visualizations
{
    internal abstract class ResultVertices : IProcessedVertices, IVisualization
    {
        public ResultVertices()
        {
            vertices = new ConcurrentDictionary<IAlgorithm, ConcurrentBag<IVertex>>();
        }

        public void Add(IAlgorithm algorithm, IVertex vertex)
        {
            vertices.TryGetOrAddNew(algorithm).Add(vertex);
        }

        public void Clear()
        {
            vertices.Clear();
        }

        public ICollection<IVertex> GetVertices(IAlgorithm algorithm)
        {
            return vertices.GetOrEmpty(algorithm);
        }

        public void Remove(IAlgorithm algorithm)
        {
            throw new NotImplementedException();
        }

        public void Visualize(IAlgorithm algorithm)
        {
            throw new NotImplementedException();
        }

        private readonly ConcurrentDictionary<IAlgorithm, ConcurrentBag<IVertex>> vertices;
    }
}
