using Algorithm.Extensions;
using Algorithm.Interfaces;
using Algorithm.Realizations.GraphPaths;
using Common.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Base
{
    public abstract class BaseGreedyAlgorithm : BaseAlgorithm
    {
        protected BaseGreedyAlgorithm(IGraph graph, IEndPoints endPoints)
           : base(graph, endPoints)
        {
            visitedVerticesStack = new Stack<IVertex>();
        }

        public override IGraphPath FindPath()
        {
            PrepareForPathfinding();
            while (!IsDestination())
            {
                PreviousVertex = CurrentVertex;
                CurrentVertex = NextVertex;
                ProcessCurrentVertex();
            }
            CompletePathfinding();

            return new GraphPath(parentVertices, endPoints, graph);
        }

        protected abstract double GreedyHeuristic(IVertex vertex);

        protected override void CompletePathfinding()
        {
            base.CompletePathfinding();
            visitedVerticesStack.Clear();
        }

        protected IVertex NextVertex
        {
            get
            {
                var neighbours = visitedVertices
                    .GetUnvisitedNeighbours(CurrentVertex).ToArray();

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