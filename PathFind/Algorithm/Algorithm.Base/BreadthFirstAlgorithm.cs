using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Base
{
    public abstract class BreadthFirstAlgorithm<TStorage> : WaveAlgorithm<TStorage>
        where TStorage : IEnumerable<IVertex>, new()
    {
        public BreadthFirstAlgorithm(IEndPoints endPoints)
            : base(endPoints)
        {
            
        }

        protected virtual void RelaxVertex(IVertex vertex)
        {
            visited.Add(vertex);
            traces[vertex.Position] = CurrentVertex;
        }

        protected override void RelaxNeighbours(IReadOnlyCollection<IVertex> vertices)
        {
            vertices.ForEach(RelaxVertex);
        }
    }
}
