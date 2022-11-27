using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Range;
using Pathfinding.VisualizationLib.Core.Interface;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Pathfinding.Visualization.Core.Abstractions
{
    public class VisualPathfindingRange<TVertex> : PathfindingRange<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public override TVertex Source
        {
            get => base.Source;
            protected set
            {
                base.Source?.VisualizeAsRegular();
                base.Source = value;
                base.Source?.VisualizeAsSource();
            }
        }

        public override TVertex Target 
        {
            get => base.Target;
            protected set
            {
                base.Target?.VisualizeAsRegular();
                base.Target = value;
                base.Target?.VisualizeAsTarget();
            }
        }

        protected override IList<TVertex> Intermediates { get; }

        public VisualPathfindingRange()
        {
            var intermediates = new ObservableCollection<TVertex>();
            intermediates.CollectionChanged += OnIntermediatesChanged;
            Intermediates = intermediates;
        }

        private void OnIntermediatesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    ((TVertex)e.NewItems[0]).VisualizeAsIntermediate();
                    break;
                case NotifyCollectionChangedAction.Remove:
                    ((TVertex)e.OldItems[0]).VisualizeAsRegular();
                    break;
            }
        }
    }
}