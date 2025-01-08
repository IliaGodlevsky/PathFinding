using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Pathfinding;
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
    internal class AlgorithmRevisionModel : ReactiveObject, IDisposable
    {
        public readonly record struct VisitedModel(
            Coordinate Visited,
            IReadOnlyList<Coordinate> Enqueued);

        public readonly record struct SubRevisionModel(
            IReadOnlyCollection<VisitedModel> Visited,
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
            public static RevisionUnit CreateVisited(RunVertexModel model, bool value = true) => new(model, RevisionUnitState.Visited, value);

            public static RevisionUnit CreateEnqueued(RunVertexModel model) => new(model, RevisionUnitState.Enqueued, true);

            public static RevisionUnit CreateCrossedPath(RunVertexModel model) => new(model, RevisionUnitState.CrossPath, true);

            public static RevisionUnit CreatePath(RunVertexModel model) => new(model, RevisionUnitState.Path, true);

            public static RevisionUnit CreateSource(RunVertexModel model) => new(model, RevisionUnitState.Source, true);

            public static RevisionUnit CreateTarget(RunVertexModel model) => new(model, RevisionUnitState.Target, true);

            public static RevisionUnit CreateTransit(RunVertexModel model) => new(model, RevisionUnitState.Transit, true);

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
            = new(Graph<RunVertexModel>.Empty,
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
            private set => this.RaiseAndSetIfChanged(ref cursor, CursorRange.ReturnInRange(value));
        }

        public int Id { get; set; }

        public AlgorithmRevisionModel(
            IGraph<RunVertexModel> vertices,
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
            IGraph<RunVertexModel> graph,
            IReadOnlyCollection<SubRevisionModel> pathfindingResult,
            IReadOnlyCollection<Coordinate> range)
        {
            if (graph.Count == 0 || pathfindingResult.Count == 0 || range.Count < 2)
            {
                return Array.AsReadOnly(Array.Empty<RevisionUnit>());
            }

            var previousVisited = new HashSet<Coordinate>();
            var previousPaths = new HashSet<Coordinate>();
            var previousEnqueued = new HashSet<Coordinate>();
            var subAlgorithms = new List<RevisionUnit>();

            range.Skip(1).Take(range.Count - 2)
                .Select(x => RevisionUnit.CreateTransit(graph.Get(x)))
                .Prepend(RevisionUnit.CreateSource(graph.Get(range.First())))
                .Append(RevisionUnit.CreateTarget(graph.Get(range.Last())))
                .ForWhole(subAlgorithms.AddRange);

            foreach (var subAlgorithm in pathfindingResult)
            {
                var visitedIgnore = range.Concat(previousPaths).ToArray();
                var exceptRangePath = subAlgorithm.Path.Except(range).ToArray();

                subAlgorithm.Visited.SelectMany(v =>
                     v.Enqueued.Intersect(previousVisited).Except(visitedIgnore)
                        .Select(x => RevisionUnit.CreateVisited(graph.Get(x), false))
                        .Concat(v.Visited.Enumerate().Except(visitedIgnore)
                        .Select(x => RevisionUnit.CreateVisited(graph.Get(x)))
                        .Concat(v.Enqueued.Except(visitedIgnore).Except(previousEnqueued)
                        .Select(x => RevisionUnit.CreateEnqueued(graph.Get(x))))))
                        .Distinct().ForWhole(subAlgorithms.AddRange);

                exceptRangePath.Intersect(previousPaths)
                    .Select(x => RevisionUnit.CreateCrossedPath(graph.Get(x)))
                    .Concat(exceptRangePath.Except(previousPaths)
                    .Select(x => RevisionUnit.CreatePath(graph.Get(x))))
                    .ForWhole(subAlgorithms.AddRange);

                previousVisited.AddRange(subAlgorithm.Visited.Select(x => x.Visited));
                previousEnqueued.AddRange(subAlgorithm.Visited.SelectMany(x => x.Enqueued));
                previousPaths.AddRange(subAlgorithm.Path);
            }

            return subAlgorithms.AsReadOnly();
        }

        public void Dispose()
        {
            Fraction = 0;
            disposables.Dispose();
        }
    }
}
