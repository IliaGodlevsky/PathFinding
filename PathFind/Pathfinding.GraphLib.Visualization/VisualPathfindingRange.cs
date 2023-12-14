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
        private readonly ObservableCollection<TVertex> transit;

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

        public IList<TVertex> Transit => transit;

        public VisualPathfindingRange(IPathfindingRange<TVertex> range)
        {
            this.range = range;
            transit = new ObservableCollection<TVertex>(range.Transit);
            transit.CollectionChanged += OnTransitChanged;
        }

        private void OnTransitChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    var vertex = (TVertex)e.NewItems[0];
                    range.Transit.Insert(e.NewStartingIndex, vertex);
                    vertex.VisualizeAsTransit();
                    break;
                case NotifyCollectionChangedAction.Remove:
                    vertex = (TVertex)e.OldItems[0];
                    range.Transit.Remove(vertex);
                    vertex.VisualizeAsRegular();
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