using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Pathfinding.Visualization.Core.Abstractions
{
    public sealed class VisualPathfindingRange<TVertex> : IPathfindingRange<TVertex>
        where TVertex : IVertex, IRangeVisualizable, IGraphVisualizable
    {
        private readonly IPathfindingRange<TVertex> range;

        public TVertex Source
        {
            get => range.Source;
            set
            {
                range.Source?.VisualizeAsRegular();
                range.Source = value;
                range.Source?.VisualizeAsSource();
            }
        }

        public TVertex Target
        {
            get => range.Target;
            set
            {
                range.Target?.VisualizeAsRegular();
                range.Target = value;
                range.Target?.VisualizeAsTarget();
            }
        }

        public IList<TVertex> Transit { get; }

        public VisualPathfindingRange(IPathfindingRange<TVertex> range)
        {
            this.range = range;
            var transit = new ObservableCollection<TVertex>(range.Transit);
            transit.CollectionChanged += OnIntermediatesChanged;
            Transit = transit;
        }

        private void OnIntermediatesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    range.Transit.Add((TVertex)e.NewItems[0]);
                    ((TVertex)e.NewItems[0]).VisualizeAsTransit();
                    break;
                case NotifyCollectionChangedAction.Remove:
                    range.Transit.Remove((TVertex)e.OldItems[0]);
                    ((TVertex)e.OldItems[0]).VisualizeAsRegular();
                    break;
                case NotifyCollectionChangedAction.Reset:
                    range.Transit.ForEach(v => v.VisualizeAsRegular());
                    range.Transit.Clear();
                    break;
            }
        }

        public IEnumerator<TVertex> GetEnumerator()
        {
            return range.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return range.GetEnumerator();
        }
    }
}