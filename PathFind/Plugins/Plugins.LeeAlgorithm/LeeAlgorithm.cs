using Algorithm.Base;
using Algorithm.Extensions;
using Algorithm.Сompanions;
using AssembleClassesLib.Attributes;
using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System.Linq;

namespace Plugins.LeeAlgorithm
{
    [ClassName("Lee algorithm")]
    public class LeeAlgorithm : WaveAlgorithm
    {
        public LeeAlgorithm(IGraph graph, IEndPoints endPoints)
            : base(graph, endPoints)
        {

        }

        protected override void PrepareForPathfinding()
        {
            base.PrepareForPathfinding();
            var vertices = graph.Vertices.FilterObstacles();
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

                return verticesQueue.DequeueOrDefault();
            }
        }

        #region Relaxing
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
        #endregion
    }
}
