using Commands.Extensions;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Base.EndPoints.Commands.VerticesCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GraphLib.Base.EndPoints
{
    public abstract class BaseEndPoints : IEndPoints
    {
        private readonly IVerticesCommands markedToReplaceCommands;
        private readonly IVerticesCommands setEndPointsCommands;
        private readonly IVerticesCommands returnColorsCommands;

        public IVertex Source { get; internal set; }

        public IVertex Target { get; internal set; }

        public IEnumerable<IVertex> EndPoints => Intermediates.Prepend(Source).Append(Target);

        internal protected Collection<IVertex> Intermediates { get; }

        internal protected Collection<IVertex> MarkedToReplace { get; }

        protected BaseEndPoints()
        {
            Intermediates = new Collection<IVertex>();
            MarkedToReplace = new Collection<IVertex>();
            setEndPointsCommands = new SetEndPointsCommands(this);
            markedToReplaceCommands = new IntermediateToReplaceCommands(this);
            returnColorsCommands = new RestoreColorsCommands(this);
            Reset();
        }

        public void SubscribeToEvents(IGraph graph)
        {
            graph.ForEach(SubscribeVertex);
        }

        public void UnsubscribeFromEvents(IGraph graph)
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
            returnColorsCommands.ExecuteForEach(EndPoints);
        }

        protected virtual void SetEndPoints(object sender, EventArgs e)
        {
            setEndPointsCommands.Execute(sender.AsVertex());
        }

        protected virtual void MarkIntermediateToReplace(object sender, EventArgs e)
        {
            markedToReplaceCommands.Execute(sender.AsVertex());
        }

        protected abstract void SubscribeVertex(IVertex vertex);

        protected abstract void UnsubscribeVertex(IVertex vertex);
    }
}