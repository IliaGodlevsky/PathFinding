using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.VisualizationLib.Core.Interface;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Pathfinding.Visualization.Core.Abstractions
{
    public sealed class VisualPathfindingRange<TVertex> : IPathfindingRange<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        private TVertex source;
        private TVertex target;

        public TVertex Source
        {
            get => source;
            set
            {
                source?.VisualizeAsRegular();
                source = value;
                source?.VisualizeAsSource();
            }
        }

        public TVertex Target
        {
            get => target;
            set
            {
                target?.VisualizeAsRegular();
                target = value;
                target?.VisualizeAsTarget();
            }
        }

        private ObservableCollection<TVertex> Transit { get; } = new ObservableCollection<TVertex>();

        IList<TVertex> IPathfindingRange<TVertex>.Transit => Transit;

        public VisualPathfindingRange()
        {
            Transit.CollectionChanged += OnIntermediatesChanged;
        }

        private void OnIntermediatesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    ((TVertex)e.NewItems[0]).VisualizeAsTransit();
                    break;
                case NotifyCollectionChangedAction.Remove:
                    ((TVertex)e.OldItems[0]).VisualizeAsRegular();
                    break;
            }
        }

        public IEnumerator<TVertex> GetEnumerator()
        {
            return Transit.Append(Target)
                .Prepend(Source)
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}