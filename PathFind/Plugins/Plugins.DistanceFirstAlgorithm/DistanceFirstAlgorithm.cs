using Algorithm.Base;
using Algorithm.Extensions;
using Algorithm.Interfaces;
using Algorithm.Realizations;
using Common.Extensions;
using GraphLib.Common.NullObjects;
using GraphLib.Interface;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Plugins.DistanceFirstAlgorithm
{
    [Description("Distance-first algorithm")]
    public class DistanceFirstAlgorithm : BaseAlgorithm
    {
        public DistanceFirstAlgorithm() : this(new NullGraph())
        {

        }

        public DistanceFirstAlgorithm(IGraph graph) : base(graph)
        {
            visitedVerticesStack = new Stack<IVertex>();
        }

        public override IGraphPath FindPath(IEndPoints endpoints)
        {
            PrepareForPathfinding(endpoints);
            while (!IsDestination())
            {
                PreviousVertex = CurrentVertex;
                CurrentVertex = NextVertex;
                ProcessCurrentVertex();
            }
            CompletePathfinding();

            return new GraphPath(parentVertices, endpoints);
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
                var neighbours = GetUnvisitedNeighbours(CurrentVertex);
                bool IsLeastCostVertex(IVertex vertex)
                    => CalculateHeuristic(vertex) == neighbours.Min(CalculateHeuristic);

                return neighbours
                    .ForEach(Enqueue)
                    .ToList()
                    .FindOrDefault(IsLeastCostVertex);
            }
        }

        private void VisitCurrentVertex()
        {
            visitedVertices[CurrentVertex.Position] = CurrentVertex;
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
                parentVertices[CurrentVertex.Position] = PreviousVertex;
            }
        }

        private IVertex PreviousVertex { get; set; }

        private readonly Stack<IVertex> visitedVerticesStack;
    }
}
