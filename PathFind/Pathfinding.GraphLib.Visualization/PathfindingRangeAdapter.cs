using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Visualization.Commands.Realizations.VisualizationCommands;
using Pathfinding.Visualization.Extensions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Executable;
using Shared.Executable.Extensions;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Visualization.Core.Abstractions
{
    public abstract class PathfindingRangeAdapter<TVertex> : IPathfindingRangeAdapter<TVertex>, IGraphSubscription<TVertex>, IUndo
        where TVertex : IVertex, IVisualizable
    {
        private readonly IntermediateVerticesToRemoveCommands<TVertex> intermediateVerticesToRemoveCommands;
        private readonly SetPathfindingRangeCommands<TVertex> setPathfindingRangeCommands;
        private readonly IExecutable<TVertex> returnVerticesVisualCommands;
        private readonly IPathfindingRangeFactory rangeFactory;

        public TVertex Source { get; internal set; }

        public TVertex Target { get; internal set; }

        IReadOnlyCollection<TVertex> IPathfindingRangeAdapter<TVertex>.Intermediates => Intermediates.AsReadOnly();

        internal List<TVertex> Intermediates { get; }

        internal protected IList<TVertex> MarkedToRemoveIntermediateVertices { get; }

        protected PathfindingRangeAdapter(IPathfindingRangeFactory rangeFactory)
        {
            this.rangeFactory = rangeFactory;
            Intermediates = new List<TVertex>();
            MarkedToRemoveIntermediateVertices = new List<TVertex>();
            setPathfindingRangeCommands = new SetPathfindingRangeCommands<TVertex>(this);
            intermediateVerticesToRemoveCommands = new IntermediateVerticesToRemoveCommands<TVertex>(this);
            returnVerticesVisualCommands = new RestoreVerticesVisualCommands<TVertex>(this);
        }

        public IPathfindingRange GetPathfindingRange()
        {
            return rangeFactory.CreateRange(Source, Target, Intermediates.OfType<IVertex>());
        }

        public void Subscribe(IGraph<TVertex> graph)
        {
            graph.ForEach(SubscribeVertex);
        }

        public void Unsubscribe(IGraph<TVertex> graph)
        {
            graph.ForEach(UnsubscribeVertex);
        }

        public void Undo()
        {
            setPathfindingRangeCommands.Undo();
            intermediateVerticesToRemoveCommands.Undo();
        }

        public void RestoreVerticesVisualState()
        {
            var vertices = this.GetRange().ToArray();
            returnVerticesVisualCommands.Execute(vertices);
        }

        protected virtual void SetPathfindingRange(TVertex vertex)
        {
            setPathfindingRangeCommands.Execute(vertex);
        }

        protected virtual void MarkIntermediateVertexToReplace(TVertex vertex)
        {
            intermediateVerticesToRemoveCommands.Execute(vertex);
        }

        protected abstract void SubscribeVertex(TVertex vertex);

        protected abstract void UnsubscribeVertex(TVertex vertex);
    }
}