using Algorithm.Extensions;
using Algorithm.Interfaces;
using Algorithm.Realizations.GraphPaths;
using AssembleClassesLib.Attributes;
using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Base
{
    /// <summary>
    /// A base class for all Greedy algorithms.
    /// This is an abstract class
    /// </summary>
    [NotLoadable]
    public abstract class BaseGreedyAlgorithm : BaseAlgorithm
    {
        protected BaseGreedyAlgorithm(IGraph graph, IEndPoints endPoints)
           : base(graph, endPoints)
        {
            visitedVerticesStack = new Stack<IVertex>();
        }

        public override sealed IGraphPath FindPath()
        {
            PrepareForPathfinding();
            while (!IsDestination())
            {
                PreviousVertex = CurrentVertex;
                CurrentVertex = NextVertex;
                ProcessCurrentVertex();
            }
            CompletePathfinding();

            return new GraphPath(parentVertices, endPoints);
        }

        /// <summary>
        /// A greedy function (heuristic) for algorithm
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        protected abstract double GreedyHeuristic(IVertex vertex);

        protected override void CompletePathfinding()
        {
            base.CompletePathfinding();
            visitedVerticesStack.Clear();
        }

        /// <summary>
        /// Gets next vertex according 
        /// to <see cref="GreedyHeuristic(IVertex)"/> function
        /// </summary>
        protected IVertex NextVertex
        {
            get
            {
                var neighbours = visitedVertices
                    .GetUnvisitedNeighbours(CurrentVertex)
                    .FilterObstacles()
                    .ToArray();

                bool IsLeastCostVertex(IVertex vertex)
                {
                    return GreedyHeuristic(vertex) == neighbours.Min(GreedyHeuristic);
                }

                return neighbours
                    .ForEach(Enqueue)
                    .ToList()
                    .FindOrDefault(IsLeastCostVertex);
            }
        }

        private void VisitCurrentVertex()
        {
            visitedVertices.Add(CurrentVertex);
            var args = CreateEventArgs(CurrentVertex);
            RaiseOnVertexVisitedEvent(args);
            visitedVerticesStack.Push(CurrentVertex);
        }

        private void Enqueue(IVertex vertex)
        {
            var args = CreateEventArgs(vertex);
            RaiseOnVertexEnqueuedEvent(args);
        }

        private void ProcessCurrentVertex()
        {
            if (CurrentVertex.IsNullObject())
            {
                CurrentVertex = visitedVerticesStack.PopOrDefault();
            }
            else
            {
                VisitCurrentVertex();
                parentVertices.Add(CurrentVertex, PreviousVertex);
            }
        }

        private IVertex PreviousVertex { get; set; }

        private readonly Stack<IVertex> visitedVerticesStack;
    }
}