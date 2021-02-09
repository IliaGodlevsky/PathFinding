using Algorithm.Algorithms;
using Algorithm.Extensions;
using Algorithm.Handlers;
using GraphLib.Interface;
using GraphLib.NullObjects;
using System.ComponentModel;
using System.Linq;

namespace Algorithm.PathFindingAlgorithms
{
    [Description("Lee algorithm (heuristic)")]
    public class BestFirstLeeAlgorithm : LeeAlgorithm
    {
        public HeuristicHandler HeuristicFunction { get; set; }

        public BestFirstLeeAlgorithm() : this(new NullGraph())
        {

        }

        public BestFirstLeeAlgorithm(IGraph graph) : base(graph)
        {

        }

        public override void Reset()
        {
            base.Reset();
            HeuristicFunction = null;
        }

        protected override IVertex NextVertex
        {
            get
            {
                verticesQueue = verticesQueue
                    .OrderBy(vertex => accumulatedCosts[vertex.Position])
                    .ToQueue();

                return base.NextVertex;
            }
        }

        protected override void PrepareForPathfinding(IVertex start, IVertex end)
        {
            base.PrepareForPathfinding(start, end);
            if (HeuristicFunction == null)
            {
                HeuristicFunction = vertex => vertex.CalculateChebyshevDistanceTo(End);
            }
        }

        protected override double CreateWave()
        {
            return base.CreateWave() + HeuristicFunction(CurrentVertex);
        }
    }
}
