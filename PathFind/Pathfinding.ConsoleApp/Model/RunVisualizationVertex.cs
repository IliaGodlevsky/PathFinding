namespace Pathfinding.ConsoleApp.Model
{
    internal abstract class RunVisualizationVertex
    {
        public static RunVisualizationVertex GetTarget(RunVertexModel vertex) => new TargetVisualizationVertex(vertex);

        public static RunVisualizationVertex GetSource(RunVertexModel vertex) => new SourceVisualizationVertex(vertex);

        public static RunVisualizationVertex GetTransit(RunVertexModel vertex) => new TransitVisualizationVertex(vertex);

        public static RunVisualizationVertex GetVisited(RunVertexModel vertex) => new VisitedVisualizationVertex(vertex);

        public static RunVisualizationVertex GetEnqueued(RunVertexModel vertex) => new EnqueuedVisualizationVertex(vertex);

        public static RunVisualizationVertex GetPath(RunVertexModel vertex) => new PathVisualizationVertex(vertex);

        private RunVertexModel Vertex { get; }

        protected RunVisualizationVertex(RunVertexModel vertex)
        {
            Vertex = vertex;
        }

        public abstract void SetVisualizationFlag();

        private sealed class TargetVisualizationVertex : RunVisualizationVertex
        {
            public TargetVisualizationVertex(RunVertexModel vertex)
                : base(vertex)
            {
            }

            public override void SetVisualizationFlag()
            {
                Vertex.IsTarget = true;
            }
        }

        private sealed class SourceVisualizationVertex : RunVisualizationVertex
        {
            public SourceVisualizationVertex(RunVertexModel vertex)
                : base(vertex)
            {
            }

            public override void SetVisualizationFlag()
            {
                Vertex.IsSource = true;
            }
        }

        private sealed class TransitVisualizationVertex : RunVisualizationVertex
        {
            public TransitVisualizationVertex(RunVertexModel vertex)
                : base(vertex)
            {
            }

            public override void SetVisualizationFlag()
            {
                Vertex.IsTransit = true;
            }
        }

        private sealed class EnqueuedVisualizationVertex : RunVisualizationVertex
        {
            public EnqueuedVisualizationVertex(RunVertexModel vertex)
                : base(vertex)
            {
            }

            public override void SetVisualizationFlag()
            {
                Vertex.IsEnqueued = true;
            }
        }

        private sealed class VisitedVisualizationVertex : RunVisualizationVertex
        {
            public VisitedVisualizationVertex(RunVertexModel vertex)
                : base(vertex)
            {
            }

            public override void SetVisualizationFlag()
            {
                Vertex.IsVisited = true;
            }
        }

        private sealed class PathVisualizationVertex : RunVisualizationVertex
        {
            public PathVisualizationVertex(RunVertexModel vertex)
                : base(vertex)
            {
            }

            public override void SetVisualizationFlag()
            {
                Vertex.IsPath = true;
            }
        }
    }
}
