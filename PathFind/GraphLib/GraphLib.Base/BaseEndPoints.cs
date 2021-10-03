using GraphLib.Base.EndPointsConditions.Interfaces;
using GraphLib.Base.EndPointsConditions.Realizations;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GraphLib.Base
{
    public abstract class BaseEndPoints : IIntermediateEndPoints, IEndPoints
    {
        public IVertex Source { get; internal set; }
        public IVertex Target { get; internal set; }
        public IReadOnlyCollection<IVertex> IntermediateVertices => intermediates;

        public void SubscribeToEvents(IGraph graph) => graph.ForEach(SubscribeVertex);
        public void UnsubscribeFromEvents(IGraph graph) => graph.ForEach(UnsubscribeVertex);
        public void Reset()
        {
            middleButtonConditions.ResetAllExecutings();
            leftButtonConditions.ResetAllExecutings();
        }
        public bool IsEndPoint(IVertex vertex) => this.GetVertices().Contains(vertex);

        internal protected readonly Collection<IVertex> intermediates;
        internal protected readonly Queue<IVertex> markedToReplaceIntermediates;

        protected BaseEndPoints()
        {
            intermediates = new Collection<IVertex>();
            markedToReplaceIntermediates = new Queue<IVertex>();
            leftButtonConditions = new SetEndPointsConditions(this);
            middleButtonConditions = new MarkIntermediateToReplaceEndPointsConditions(this);
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
    }
}