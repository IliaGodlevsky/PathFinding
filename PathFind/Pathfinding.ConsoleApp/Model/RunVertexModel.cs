using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Shared.Primitives;
using ReactiveUI;
using System.Collections.Generic;

namespace Pathfinding.ConsoleApp.Model
{
    internal sealed class RunVertexModel : ReactiveObject, IVertex
    {
        public RunVertexModel(Coordinate position)
        {
            Position = position;
        }

        private bool isObstacle;
        public bool IsObstacle
        {
            get => isObstacle;
            set => this.RaiseAndSetIfChanged(ref isObstacle, value);
        }

        public Coordinate Position { get; }

        private bool isVisited;
        public bool IsVisited
        {
            get => isVisited;
            set => this.RaiseAndSetIfChanged(ref isVisited, value);
        }

        private bool isEnqueued;
        public bool IsEnqueued
        {
            get => isEnqueued;
            set => this.RaiseAndSetIfChanged(ref isEnqueued, value);
        }

        private bool isPath;
        public bool IsPath
        {
            get => isPath;
            set => this.RaiseAndSetIfChanged(ref isPath, value);
        }

        private bool isCrossedPath;
        public bool IsCrossedPath
        {
            get => isCrossedPath;
            set => this.RaiseAndSetIfChanged(ref isCrossedPath, value);
        }

        private bool isSource;
        public bool IsSource
        {
            get => isSource;
            set => this.RaiseAndSetIfChanged(ref isSource, value);
        }

        private bool isTarget;
        public bool IsTarget
        {
            get => isTarget;
            set => this.RaiseAndSetIfChanged(ref isTarget, value);
        }

        private bool isTransit;
        public bool IsTransit
        {
            get => isTransit;
            set => this.RaiseAndSetIfChanged(ref isTransit, value);
        }

        private IVertexCost cost;
        public IVertexCost Cost 
        {
            get => cost;
            set => this.RaiseAndSetIfChanged(ref cost, value);
        }

        public ICollection<IVertex> Neighbours { get; } = new HashSet<IVertex>();

        public bool Equals(IVertex other) => other.IsEqual(this);

        public override bool Equals(object obj) => obj is IVertex vertex && Equals(vertex);

        public override int GetHashCode() => Position.GetHashCode();
    }
}
