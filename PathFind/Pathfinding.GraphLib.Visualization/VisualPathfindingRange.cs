using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Visualization.Commands.Realizations.VisualizationCommands;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Executable;
using Shared.Executable.Extensions;
using Shared.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Pathfinding.Visualization.Core.Abstractions
{
    public abstract class VisualPathfindingRange<TVertex> : IPathfindingRange, IGraphSubscription<TVertex>, IUndo
        where TVertex : IVertex, IVisualizable
    {
        private readonly IntermediateVerticesToRemoveCommands<TVertex> intermediateVerticesToRemoveCommands;
        private readonly SetPathfindingRangeCommands<TVertex> setPathfindingRangeCommands;
        private readonly IExecutable<TVertex> returnVerticesVisualCommands;

        IVertex IPathfindingRange.Source => Source;

        IVertex IPathfindingRange.Target => Target;

        public TVertex Source { get; internal set; }

        public TVertex Target { get; internal set; }

        internal protected IList<TVertex> IntermediateVertices { get; }

        internal protected IList<TVertex> MarkedToRemoveIntermediateVertices { get; }

        protected VisualPathfindingRange()
        {
            IntermediateVertices = new Collection<TVertex>();
            MarkedToRemoveIntermediateVertices = new Collection<TVertex>();
            setPathfindingRangeCommands = new SetPathfindingRangeCommands<TVertex>(this);
            intermediateVerticesToRemoveCommands = new IntermediateVerticesToRemoveCommands<TVertex>(this);
            returnVerticesVisualCommands = new RestoreVerticesVisualCommands<TVertex>(this);
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
            returnVerticesVisualCommands.Execute(this.OfType<TVertex>());
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

        public IEnumerator<IVertex> GetEnumerator()
        {
            return IntermediateVertices
                .OfType<IVertex>()
                .Prepend(Source)
                .Append(Target)
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}