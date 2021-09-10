using Algorithm.Base;
using Algorithm.Extensions;
using Algorithm.Сompanions;
using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System.Linq;

namespace Algorithm.Algos.Algos
{
    public class LeeAlgorithm : WaveAlgorithm
    {
        public LeeAlgorithm(IGraph graph, IIntermediateEndPoints endPoints)
            : base(graph, endPoints)
        {

        }

        protected override void PrepareForLocalPathfinding()
        {
            base.PrepareForLocalPathfinding();
            var vertices = graph.GetNotObstacles();
            accumulatedCosts = new AccumulatedCosts(vertices, 0);
        }

        protected override void CompletePathfinding()
        {
            base.CompletePathfinding();
            verticesQueue.Clear();
        }

        protected override IVertex NextVertex
        {
            get
            {
                verticesQueue = verticesQueue
                    .Where(visitedVertices.IsNotVisited)
                    .ToQueue();

                return verticesQueue.DequeueOrNullVertex();
            }
        }

        protected virtual double CreateWave()
        {
            return accumulatedCosts.GetAccumulatedCost(CurrentVertex) + 1;
        }

        protected virtual void RelaxNeighbour(IVertex vertex)
        {
            accumulatedCosts.Reevaluate(vertex, CreateWave());
            parentVertices.Add(vertex, CurrentVertex);
        }

        protected bool VertexIsUnwaved(IVertex vertex)
        {
            return accumulatedCosts.GetAccumulatedCost(vertex) == 0;
        }

        protected override void RelaxNeighbours(IVertex[] neighbours)
        {
            neighbours
                .Where(VertexIsUnwaved)
                .ForEach(RelaxNeighbour);
        }
    }
}
