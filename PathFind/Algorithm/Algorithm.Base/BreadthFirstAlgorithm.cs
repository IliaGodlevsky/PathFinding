using GraphLib.Interfaces;
using System.Collections.Generic;

namespace Algorithm.Base
{
    public abstract class BreadthFirstAlgorithm<TStorage> : WaveAlgorithm<TStorage>
        where TStorage : new()
    {
        public BreadthFirstAlgorithm(IEndPoints endPoints)
            : base(endPoints)
        {

        }

        protected override void RelaxVertex(IVertex vertex)
        {
            visited.Add(vertex);
            traces[vertex.Position] = CurrentVertex;
        }
    }
}
