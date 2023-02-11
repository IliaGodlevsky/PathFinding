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
        where TVertex : IVertex, IVisualizable
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

        private ObservableCollection<TVertex> Transit { get; }

        IList<TVertex> IPathfindingRange<TVertex>.Transit => Transit;

        public VisualPathfindingRange(IPathfindingRange<TVertex> range)
        {
            this.range = range;
            Transit = new ObservableCollection<TVertex>(range.Transit);
            Transit.CollectionChanged += OnIntermediatesChanged;
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
                case NotifyCollectionChangedAction.Replace:
                    var val = range.Transit[e.OldStartingIndex];
                    range.Transit[e.NewStartingIndex] = val;
                    break;
                case NotifyCollectionChangedAction.Move:
                    val = range.Transit[e.OldStartingIndex];
                    range.Transit.RemoveAt(e.OldStartingIndex);
                    range.Transit.Insert(e.NewStartingIndex, val);
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