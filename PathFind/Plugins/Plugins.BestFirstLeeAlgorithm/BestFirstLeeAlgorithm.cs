using Algorithm.Extensions;
using GraphLib.Base;
using GraphLib.Interface;
using System.ComponentModel;
using System.Linq;

namespace Plugins.BestFirstLeeAlgorithm
{
    [Description("Lee algorithm (heuristic)")]
    public class BestFirstLeeAlgorithm : LeeAlgorithm.LeeAlgorithm
    {
        public BestFirstLeeAlgorithm() : this(BaseGraph.NullGraph)
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
