using Algorithm.Algorithms;
using Algorithm.Extensions;
using GraphLib.Interface;
using GraphLib.NullObjects;
using System.ComponentModel;
using System.Linq;

namespace Algorithm.PathFindingAlgorithms
{
    [Description("Lee algorithm (heuristic)")]
    public class BestFirstLeeAlgorithm : LeeAlgorithm
    {
        public BestFirstLeeAlgorithm() : this(new NullGraph())
        {

        }

        public BestFirstLeeAlgorithm(IGraph graph) : base(graph)
        {

        }

        protected override IVertex NextVertex
        {
            get
            {
                verticesQueue = verticesQueue.OrderBy(GetAccumulatedCost).ToQueue();
                return base.NextVertex;
            }
        }

        protected virtual double CalculateHeuristic(IVertex vertex)
        {
            return vertex.CalculateChebyshevDistanceTo(endPoints.End);
        }

        protected override double CreateWave()
        {
            return base.CreateWave() + CalculateHeuristic(CurrentVertex);
        }
    }
}
