using Common.Extensions.EnumerableExtensions;
using GraphLib.Base.VerticesConditions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GraphLib.Base
{
    public abstract class BaseEndPoints : IEndPoints
    {
        public IVertex Source { get; internal set; }
        public IVertex Target { get; internal set; }
        public IEnumerable<IVertex> EndPoints => intermediates.Prepend(Source).Append(Target);
        public bool IsEndPoint(IVertex vertex)
        {
            return Source.Equals(vertex)
                || Target.Equals(vertex)
                || intermediates.Contains(vertex);
        }

        public void SubscribeToEvents(IGraph graph) => graph.ForEach(SubscribeVertex);
        public void UnsubscribeFromEvents(IGraph graph) => graph.ForEach(UnsubscribeVertex);
        public void Reset()
        {
            middleButtonConditions.ResetAllExecutings();
            leftButtonConditions.ResetAllExecutings();
        }

        public void ReturnColors()
        {
            EndPoints.ForEach(returnColorsConditions.ExecuteTheFirstTrue);
        }

        internal protected readonly Collection<IVertex> intermediates;
        internal protected readonly Queue<IVertex> markedToReplaceIntermediates;

        protected BaseEndPoints()
        {
            intermediates = new Collection<IVertex>();
            markedToReplaceIntermediates = new Queue<IVertex>();
            leftButtonConditions = new SetEndPointsConditions(this);
            middleButtonConditions = new MarkIntermediateToReplaceEndPointsConditions(this);
            returnColorsConditions = new ReturnColorsConditions(this);
            Reset();
        }

        protected virtual void SetEndPoints(object sender, EventArgs e)
        {
            leftButtonConditions.ExecuteTheFirstTrue(sender as IVertex);
        }

        protected virtual void MarkIntermediateToReplace(object sender, EventArgs e)
        {
            middleButtonConditions.ExecuteTheFirstTrue(sender as IVertex);
        }

        protected abstract void SubscribeVertex(IVertex vertex);
        protected abstract void UnsubscribeVertex(IVertex vertex);

        private readonly IVerticesConditions middleButtonConditions;
        private readonly IVerticesConditions leftButtonConditions;
        private readonly IVerticesConditions returnColorsConditions;
    }
}