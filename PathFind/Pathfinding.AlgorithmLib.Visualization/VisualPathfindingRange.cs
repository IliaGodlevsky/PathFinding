using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Pathfinding.Visualization.Core.Abstractions
{
    public abstract class VisualPathfindingRange<TVertex> : IPathfindingRange, IGraphSubscription<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        private readonly IVisualizationCommand<TVertex> markedToReplaceCommands;
        private readonly IVisualizationCommand<TVertex> setEndPointsCommands;
        private readonly IVisualizationCommand<TVertex> returnColorsCommands;

        IVertex IPathfindingRange.Source => Source;

        IVertex IPathfindingRange.Target => Target;

        public TVertex Source { get; internal set; }

        public TVertex Target { get; internal set; }

        internal protected Collection<TVertex> IntermediateVertices { get; }

        internal protected Collection<TVertex> MarkedToRemoveIntermediateVertices { get; }

        protected VisualPathfindingRange()
        {
            IntermediateVertices = new Collection<TVertex>();
            MarkedToRemoveIntermediateVertices = new Collection<TVertex>();
            setEndPointsCommands = new SetEndPointsCommands<TVertex>(this);
            markedToReplaceCommands = new IntermediateToReplaceCommands<TVertex>(this);
            returnColorsCommands = new RestoreColorsCommands<TVertex>(this);
        }

        public void Subscribe(IGraph<TVertex> graph)
        {
            graph.ForEach(SubscribeVertex);
        }

        public void Unsubscribe(IGraph<TVertex> graph)
        {
            graph.ForEach(UnsubscribeVertex);
        }

        public void Reset()
        {
            markedToReplaceCommands.Undo();
            setEndPointsCommands.Undo();
        }

        public void RestoreCurrentColors()
        {
            returnColorsCommands.Execute(this.OfType<TVertex>());
        }

        protected virtual void SetEndPoints(object sender, EventArgs e)
        {
            setEndPointsCommands.Execute((TVertex)sender);
        }

        protected virtual void MarkIntermediateToReplace(object sender, EventArgs e)
        {
            markedToReplaceCommands.Execute((TVertex)sender);
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
