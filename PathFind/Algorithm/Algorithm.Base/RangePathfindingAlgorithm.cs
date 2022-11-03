using Algorithm.Infrastructure.EventArguments;
using Algorithm.Interfaces;
using Algorithm.NullRealizations;
using Algorithm.Realizations.GraphPaths;
using Common.Disposables;
using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace Algorithm.Base
{
    public abstract class RangePathfindingAlgorithm : PathfindingAlgorithm
    {
        protected virtual IEndPoints CurrentEndPoints { get; set; }

        protected RangePathfindingAlgorithm(IEndPoints endPoints) 
            : base(endPoints)
        {
        }

        public sealed override IGraphPath FindPath()
        {
            PrepareForPathfinding();
            using (Disposable.Use(CompletePathfinding))
            {
                var path = NullGraphPath.Interface;
                foreach (var endPoint in endPoints.ToSubEndPoints())
                {
                    PrepareForLocalPathfinding(endPoint);
                    VisitVertex(CurrentVertex);
                    while (!IsDestination(CurrentEndPoints))
                    {
                        ThrowIfInterrupted();
                        WaitUntilResumed();
                        InspectVertex(CurrentVertex);
                        CurrentVertex = GetNextVertex();
                        VisitVertex(CurrentVertex);
                    }
                    var subPath = CreateGraphPath();
                    path = new CompositeGraphPath(path, subPath);
                    Reset();
                }
                return path;
            }
        }

        protected virtual void PrepareForLocalPathfinding(IEndPoints endPoints)
        {
            CurrentEndPoints = endPoints;
            CurrentVertex = endPoints.Source;
        }

        protected virtual IGraphPath CreateGraphPath()
        {
            return new GraphPath(parentVertices, CurrentEndPoints);
        }

        protected virtual void Enqueued(IVertex vertex)
        {
            RaiseVertexEnqueued(new AlgorithmEventArgs(vertex));
        }

        protected abstract void InspectVertex(IVertex vertex);

        protected abstract void VisitVertex(IVertex vertex);
    }
}
