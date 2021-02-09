using Algorithm.Base;
using Algorithm.Extensions;
using Algorithm.Handlers;
using Common.Extensions;
using GraphLib.Infrastructure;
using GraphLib.Interface;
using GraphLib.NullObjects;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Algorithm.Algorithms
{
    [Description("Depth-first algorithm")]
    public class DepthFirstAlgorithm : BaseAlgorithm
    {
        public HeuristicHandler GreedyFunction { get; set; }

        public DepthFirstAlgorithm() : this(new NullGraph())
        {

        }

        public DepthFirstAlgorithm(IGraph graph) : base(graph)
        {
            visitedVertices = new Stack<IVertex>();
        }

        public override GraphPath FindPath(IVertex start, IVertex end)
        {
            PrepareForPathfinding(start, end);
            while (!IsDestination())
            {
                PreviousVertex = CurrentVertex;
                CurrentVertex = NextVertex;
                ProcessCurrentVertex();
            }
            CompletePathfinding();

            return new GraphPath(parentVertices, Start, End, visitedVerticesCoordinates.Count);
        }

        public override void Reset()
        {
            base.Reset();
            GreedyFunction = null;
        }

        protected override void PrepareForPathfinding(IVertex start, IVertex end)
        {
            base.PrepareForPathfinding(start, end);
            TrySetDefaultGreedyFunction();
        }

        protected virtual void TrySetDefaultGreedyFunction()
        {
            if (GreedyFunction == null)
            {
                GreedyFunction = vertex => vertex.CalculateChebyshevDistanceTo(Start);
            }
        }

        protected override void CompletePathfinding()
        {
            base.CompletePathfinding();
            visitedVertices.Clear();
        }

        protected override IVertex NextVertex
        {
            get
            {
                var neighbours = GetUnvisitedNeighbours(CurrentVertex);
                bool IsLeastCostVertex(IVertex vertex)
                    => GreedyFunction(vertex) == neighbours.Min(GreedyFunction);

                return neighbours
                    .ForEach(Enqueue)
                    .ToList()
                    .FindOrDefault(IsLeastCostVertex);
            }
        }

        private void VisitCurrentVertex()
        {
            visitedVerticesCoordinates.Add(CurrentVertex.Position);
            var args = CreateEventArgs(CurrentVertex);
            RaiseOnVertexVisitedEvent(args);
            visitedVertices.Push(CurrentVertex);
        }

        private void Enqueue(IVertex vertex)
        {
            var args = CreateEventArgs(vertex);
            RaiseOnVertexEnqueuedEvent(args);
        }

        private void ProcessCurrentVertex()
        {
            if (CurrentVertex.IsDefault)
            {
                CurrentVertex = visitedVertices.PopOrDefault();
            }
            else
            {
                VisitCurrentVertex();
                parentVertices[CurrentVertex.Position] = PreviousVertex;
            }
        }

        private IVertex PreviousVertex { get; set; }

        private readonly Stack<IVertex> visitedVertices;
    }
}
