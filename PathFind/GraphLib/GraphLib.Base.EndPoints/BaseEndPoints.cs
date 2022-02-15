using Common.Extensions.EnumerableExtensions;
using GraphLib.Base.EndPoints.Commands.VerticesCommands;
using GraphLib.Base.EndPoints.Extensions;
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
        public IVertex Source { get; internal set; }
        public IVertex Target { get; internal set; }
        public IEnumerable<IVertex> EndPoints 
            => intermediates.Prepend(Source).Append(Target);

        public bool IsEndPoint(IVertex vertex)
        {
            return Source.Equals(vertex) 
                || Target.Equals(vertex) 
                || intermediates.Contains(vertex);
        }

        public void SubscribeToEvents(IGraph graph) 
            => graph.ForEach(SubscribeVertex);
        public void UnsubscribeFromEvents(IGraph graph) 
            => graph.ForEach(UnsubscribeVertex);
        public void Reset()
        {
            setEndPointsCommands.Reset();
            markToReplaceCommands.Reset();
        }

        public void RestoreCurrentColors()
        {
            returnColorsCommands.ExecuteForEach(EndPoints);
        }

        internal protected readonly Collection<IVertex> intermediates;
        internal protected readonly Queue<IVertex> markedToReplaceIntermediates;

        protected BaseEndPoints()
        {
            intermediates = new Collection<IVertex>();
            markedToReplaceIntermediates = new Queue<IVertex>();
            markToReplaceCommands = new SetEndPointsCommands(this);
            setEndPointsCommands = new IntermediateToReplaceCommands(this);
            returnColorsCommands = new ReturnColorsCommands(this);
            Reset();
        }

        protected virtual void SetEndPoints(object sender, EventArgs e)
        {
            markToReplaceCommands.Execute(sender as IVertex);
        }

        protected virtual void MarkIntermediateToReplace(object sender, EventArgs e)
        {
            setEndPointsCommands.Execute(sender as IVertex);
        }

        protected abstract void SubscribeVertex(IVertex vertex);
        protected abstract void UnsubscribeVertex(IVertex vertex);

        private readonly IVerticesCommands setEndPointsCommands;
        private readonly IVerticesCommands markToReplaceCommands;
        private readonly IVerticesCommands returnColorsCommands;
    }
}