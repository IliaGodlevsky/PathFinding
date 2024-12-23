using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Primitives;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Pathfinding.ConsoleApp.Model
{
    internal sealed class AlgorithmRevisionModel : ReactiveObject, IDisposable
    {
        public record struct SubRevisionModel(
            IReadOnlyCollection<(Coordinate Visited, IReadOnlyList<Coordinate> Enqueued)> Visited,
            IReadOnlyCollection<Coordinate> Path);

        private enum RevisionUnitState
        {
            No, 
            Source, 
            Target,
            Transit,
            Visited, 
            Enqueued, 
            Path, 
            CrossPath
        }

        private readonly record struct RevisionUnit(
            RunVertexModel Vertex,
            RevisionUnitState State, 
            bool Value)
        {
            public void Set() => Set(Value);

            public void SetInverse() => Set(!Value);

            private void Set(bool value)
            {
                switch (State)
                {
                    case RevisionUnitState.Visited: Vertex.IsVisited = value; break;
                    case RevisionUnitState.Enqueued: Vertex.IsEnqueued = value; break;
                    case RevisionUnitState.CrossPath: Vertex.IsCrossedPath = value; break;
                    case RevisionUnitState.Path: Vertex.IsPath = value; break;
                    case RevisionUnitState.Source: Vertex.IsSource = value; break;
                    case RevisionUnitState.Target: Vertex.IsTarget = value; break;
                    case RevisionUnitState.Transit: Vertex.IsTransit = value; break;
                }
            }
        }

        public static readonly AlgorithmRevisionModel Empty 
            = new(Array.Empty<RunVertexModel>(),
                  Array.Empty<SubRevisionModel>(),
                  Array.Empty<Coordinate>());

        public static readonly InclusiveValueRange<float> FractionRange = new(1, 0);

        private readonly CompositeDisposable disposables = new();
        private readonly Lazy<ReadOnlyCollection<RevisionUnit>> algorithm;
        private readonly Lazy<InclusiveValueRange<int>> cursorRange;

        private ReadOnlyCollection<RevisionUnit> Algorithm => algorithm.Value;

        private InclusiveValueRange<int> CursorRange => cursorRange.Value;

        public int Count => Algorithm.Count;

        private float fraction;
        public float Fraction
        {
            get => fraction;
            set => this.RaiseAndSetIfChanged(ref fraction, FractionRange.ReturnInRange(value));
        }

        private int cursor;
        public int Cursor 
        {
            get => cursor;
            private set => cursor = CursorRange.ReturnInRange(value);
        }

        public int Id { get; set; }

        public AlgorithmRevisionModel(
            IReadOnlyCollection<RunVertexModel> vertices, 
            IReadOnlyCollection<SubRevisionModel> pathfindingResult,
            IReadOnlyCollection<Coordinate> range)
        {
            algorithm = new(() => CreateAlgorithmRevision(vertices, pathfindingResult, range));
            cursorRange = new(() => new InclusiveValueRange<int>(Count - 1, 0));
            this.WhenAnyValue(x => x.Fraction)
                .DistinctUntilChanged().Select(GetCount)
                .Where(x => x > 0).Do(Next)
                .Subscribe().DisposeWith(disposables);
            this.WhenAnyValue(x => x.Fraction)
                .DistinctUntilChanged().Select(GetCount)
                .Where(x => x < 0).Do(Previous)
                .Subscribe().DisposeWith(disposables);
        }

        private int GetCount(float fraction)
            => (int)Math.Floor(Count * fraction - Cursor);

        private void Next(int count)
        {
            while (count-- >= 0) Algorithm[Cursor++].Set();
        }

        private void Previous(int count)
        {
            while (count++ <= 0) Algorithm[Cursor--].SetInverse();
        }

        private static ReadOnlyCollection<RevisionUnit> CreateAlgorithmRevision(
            IReadOnlyCollection<RunVertexModel> graph,
            IReadOnlyCollection<SubRevisionModel> pathfindingResult,
            IReadOnlyCollection<Coordinate> range)
        {
            if (graph.Count == 0)
            {
                return Array.AsReadOnly(Array.Empty<RevisionUnit>());
            }
            var dictionary = graph.ToDictionary(x => x.Position);
            var previousVisited = new HashSet<Coordinate>();
            var previousPaths = new HashSet<Coordinate>();
            var previousEnqueued = new HashSet<Coordinate>();
            var subAlgorithms = new List<RevisionUnit>();
            foreach (var subAlgorithm in pathfindingResult)
            {
                var sub = new HashSet<RevisionUnit>();
                var visitedIgnore = range.Concat(previousPaths).ToArray();
                foreach (var (Visited, Enqueued) in subAlgorithm.Visited)
                {
                    foreach (var enqueued in Enqueued.Intersect(previousVisited).Except(visitedIgnore))
                    {
                        sub.Add(new(dictionary[enqueued], RevisionUnitState.Visited, false));
                    }
                    foreach (var visited in Visited.Enumerate().Except(visitedIgnore))
                    {
                        sub.Add(new(dictionary[visited], RevisionUnitState.Visited, true));
                    }
                    foreach (var enqueued in Enqueued.Except(visitedIgnore).Except(previousEnqueued))
                    {
                        sub.Add(new(dictionary[enqueued], RevisionUnitState.Enqueued, true));
                    }
                }
                var exceptRangePath = subAlgorithm.Path.Except(range).ToArray();
                foreach (var path in exceptRangePath.Intersect(previousPaths))
                {
                    sub.Add(new(dictionary[path], RevisionUnitState.CrossPath, true));
                }
                foreach (var path in exceptRangePath.Except(previousPaths))
                {
                    sub.Add(new(dictionary[path], RevisionUnitState.Path, true));
                }

                previousVisited.AddRange(subAlgorithm.Visited.Select(x => x.Visited));
                previousEnqueued.AddRange(subAlgorithm.Visited.SelectMany(x => x.Enqueued));
                previousPaths.AddRange(subAlgorithm.Path);
                subAlgorithms.AddRange(sub);
            }
            var vertices = new List<RevisionUnit>
            {
                new(dictionary[range.First()], RevisionUnitState.Source, true)
            };
            foreach (var transit in range.Skip(1).Take(range.Count - 2))
            {
                vertices.Add(new(dictionary[transit], RevisionUnitState.Transit, true));
            }
            vertices.Add(new(dictionary[range.Last()], RevisionUnitState.Target, true));
            vertices.AddRange(subAlgorithms);
            return vertices.AsReadOnly();
        }

        public void Dispose()
        {
            Fraction = 0;
            disposables.Dispose();
        }
    }
}
