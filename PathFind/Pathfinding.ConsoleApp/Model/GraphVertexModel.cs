using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Shared.Primitives;
using ReactiveUI;
using System.Collections.Generic;

namespace Pathfinding.ConsoleApp.Model
{
    public class GraphVertexModel : ReactiveObject, IVertex, IEntity<int>
    {
        public int Id { get; set; }

        private bool isObstacle;
        public bool IsObstacle
        {
            get => isObstacle;
            set => this.RaiseAndSetIfChanged(ref isObstacle, value);
        }

        private bool isRegular;
        public bool IsRegular
        {
            get => isRegular;
            set => this.RaiseAndSetIfChanged(ref isRegular, value);
        }

        private bool isSource;
        public bool IsSource
        {
            get => isSource;
            set
            {
                this.RaiseAndSetIfChanged(ref isSource, value);
                if (!isSource) IsRegular = true;
            }
        }

        private bool isTarget;
        public bool IsTarget
        {
            get => isTarget;
            set
            {
                this.RaiseAndSetIfChanged(ref isTarget, value);
                if (!isTarget) IsRegular = true;
            }
        }

        private bool isTransit;
        public bool IsTransit
        {
            get => isTransit;
            set
            {
                this.RaiseAndSetIfChanged(ref isTransit, value);
                if (!isTransit) IsRegular = true;
            }
        }

        private IVertexCost cost;
        public IVertexCost Cost
        {
            get => cost;
            set => this.RaiseAndSetIfChanged(ref cost, value);
        }

        public Coordinate Position { get; }

        public ICollection<IVertex> Neighbours { get; } = new HashSet<IVertex>();

        public GraphVertexModel(Coordinate coordinate)
        {
            Position = coordinate;
        }

        public bool Equals(IVertex other) => other.IsEqual(this);

        public override bool Equals(object obj) => obj is IVertex vertex && Equals(vertex);

        public override int GetHashCode() => Position.GetHashCode();
    }
}
