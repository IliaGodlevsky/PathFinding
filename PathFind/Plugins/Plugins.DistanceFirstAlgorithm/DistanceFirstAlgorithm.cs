using Algorithm.Base;
using Algorithm.Extensions;
using Algorithm.Interfaces;
using Algorithm.Realizations;
using AssembleClassesLib.Attributes;
using Common.Extensions;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Plugins.DistanceFirstAlgorithm
{
    [ClassName("Distance-first algorithm")]
    public class DistanceFirstAlgorithm : BaseAlgorithm
    {
        public DistanceFirstAlgorithm(IGraph graph, IEndPoints endPoints) 
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

        protected virtual double CalculateHeuristic(IVertex vertex)
        {
            return vertex.CalculateChebyshevDistanceTo(endPoints.End);
        }

        protected override void CompletePathfinding()
        {
            base.CompletePathfinding();
            visitedVerticesStack.Clear();
        }

        protected override IVertex NextVertex
        {
            get
            {
                var neighbours = visitedVertices
                    .GetUnvisitedNeighbours(CurrentVertex).ToArray();

                bool IsLeastCostVertex(IVertex vertex)
                {
                    return CalculateHeuristic(vertex) == neighbours.Min(CalculateHeuristic);
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
            if (CurrentVertex.IsDefault())
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
