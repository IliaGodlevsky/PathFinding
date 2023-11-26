using Pathfinding.App.Console.DataAccess.Repo;
using Pathfinding.GraphLib.Core.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Pathfinding.App.Console.Model
{
    internal sealed class DbServicePathfindingRange : IPathfindingRange<Vertex>
    {
        private readonly IDbContextService service;
        private readonly IPathfindingRange<Vertex> range;

        public Vertex Source 
        {
            get => range.Source;
            set
            {
                service.RemoveRangeVertex(range.Source);
                if (value is not null)
                {
                    range.Source = value;
                    service.AddRangeVertex(range.Source, 0);
                }
            }
        }

        public Vertex Target
        {
            get => range.Target;
            set
            {
                service.RemoveRangeVertex(range.Target);
                if (value is not null)
                {
                    range.Target = value;
                    int order = range.Transit.Count + 1;
                    service.AddRangeVertex(range.Target, order);
                }
            }
        }

        public IList<Vertex> Transit { get; }

        public DbServicePathfindingRange(IPathfindingRange<Vertex> range, IDbContextService service)
        {
            this.range = range;
            this.service = service;
            var transit = new ObservableCollection<Vertex>(range.Transit);
            transit.CollectionChanged += OnTransitChanged;
            Transit = transit;
        }

        private void OnTransitChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    var vertex = (Vertex)e.NewItems[0];
                    range.Transit.Add(vertex);
                    int order = range.Transit.Count;
                    service.AddRangeVertex(vertex, order);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    vertex = (Vertex)e.OldItems[0];
                    range.Transit.Remove(vertex);
                    service.RemoveRangeVertex(vertex);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    foreach(var vert in range.Transit)
                    {
                        service.RemoveRangeVertex(vert);
                    }
                    range.Transit.Clear();
                    break;
            }
        }

        public IEnumerator<Vertex> GetEnumerator()
        {
            return range.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return range.GetEnumerator();
        }
    }
}
