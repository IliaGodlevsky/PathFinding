using GraphLibrary.Extensions.SystemTypeExtensions;
using GraphLibrary.Vertex.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.PathFindingAlgorithm
{
    public class BestFirstLeeAlgorithm : LeeAlgorithm
    {
        public Func<IVertex, double> HeuristicFunction { protected get; set; }

        public BestFirstLeeAlgorithm()
        {

        }

        protected override IVertex GetNextVertex()
        {
            neighbourQueue = new Queue<IVertex>(neighbourQueue.OrderBy(vertex => vertex.AccumulatedCost));
            return base.GetNextVertex();
        }

        protected override double WaveFunction(IVertex vertex)
        {
            return base.WaveFunction(vertex) + HeuristicFunction(vertex);
        }
    }
}
