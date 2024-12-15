using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Primitives;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Pathfinding.ConsoleApp.Model
{
    internal record struct SubRevisionModel(
        IReadOnlyCollection<(Coordinate Visited, IReadOnlyList<Coordinate> Enqueued)> Visited,
        IReadOnlyCollection<Coordinate> Path);

    internal sealed class AlgorithmRevisionModel : ReactiveObject, IDisposable
    {
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
            public RevisionUnit Inverse() => new(Vertex, State, !Value);

            public void SetState()
            {
                switch (State)
                {
                    case RevisionUnitState.Visited: Vertex.IsVisited = Value; break;
                    case RevisionUnitState.Enqueued: Vertex.IsEnqueued = Value; break;
                    case RevisionUnitState.CrossPath: Vertex.IsCrossedPath = Value; break;
                    case RevisionUnitState.Path: Vertex.IsPath = Value; break;
                    case RevisionUnitState.Source: Vertex.IsSource = Value; break;
                    case RevisionUnitState.Target: Vertex.IsTarget = Value; break;
                    case RevisionUnitState.Transit: Vertex.IsTransit = Value; break;
                }
            }
        }

        public static readonly AlgorithmRevisionModel Empty 
            = new(Array.Empty<RunVertexModel>(),
                  Array.Empty<SubRevisionModel>(),
                  Array.Empty<Coordinate>());

        private readonly CompositeDisposable disposables = new();
        private readonly Lazy<IReadOnlyList<RevisionUnit>> algorithm;
        private readonly Lazy<InclusiveValueRange<int>> cursorRange;

        private IReadOnlyList<RevisionUnit> Algorithm => algorithm.Value;

        private InclusiveValueRange<int> CursorRange => cursorRange.Value;

        public int Count => Algorithm.Count;

        private float fraction;
        public float Fraction
        {
            get => fraction;
            set => this.RaiseAndSetIfChanged(ref fraction, value);
        }

        private int cursor;
        private int Cursor 
        {
            get => cursor;
            set => cursor = CursorRange.ReturnInRange(value);
        }

        public AlgorithmRevisionModel(
            IReadOnlyCollection<RunVertexModel> vertices, 
            IReadOnlyCollection<SubRevisionModel> pathfindingResult,
            IReadOnlyCollection<Coordinate> range)
        {
            algorithm = new(() => CreateAlgorithmRevision(vertices, pathfindingResult, range));
            cursorRange = new(() => new InclusiveValueRange<int>(Count, 0));
            this.WhenAnyValue(x => x.Fraction)
                .Select(GetCount).Where(x => x > 0).Do(Next)
                .Subscribe().DisposeWith(disposables);
            this.WhenAnyValue(x => x.Fraction)
                .Select(GetCount).Where(x => x < 0).Do(Previous)
                .Subscribe().DisposeWith(disposables);
        }

        private double GetCount(float fraction)
            => Math.Floor(Count * fraction - Cursor);

        private void Next(double count)
        {
            while (count-- > 0)
            {
                Algorithm.ElementAtOrDefault(Cursor++).SetState();
            }
        }

        private void Previous(double count)
        {
            while (count++ <= 0)
            {
                Algorithm.ElementAtOrDefault(--Cursor).Inverse().SetState();
            }
        }

        private static IReadOnlyList<RevisionUnit> CreateAlgorithmRevision(
            IReadOnlyCollection<RunVertexModel> graph,
            IReadOnlyCollection<SubRevisionModel> pathfindingResult,
            IReadOnlyCollection<Coordinate> range)
        {
            if (graph.Count == 0)
            {
                return Array.Empty<RevisionUnit>();
            }
            var dictionary = graph.ToDictionary(x => x.Position);
            var previousVisited = new HashSet<Coordinate>();
            var previousPaths = new HashSet<Coordinate>();
            var previousEnqueued = new HashSet<Coordinate>();
            var subAlgorithms = new List<HashSet<RevisionUnit>>();
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
                subAlgorithms.Add(sub);
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
            return vertices.Concat(subAlgorithms.SelectMany(x => x)).ToArray();
        }

        public void Dispose()
        {
            disposables.Dispose();
        }
    }
}
