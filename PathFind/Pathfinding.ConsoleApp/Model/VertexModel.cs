using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Shared.Primitives;
using ReactiveUI;
using System.Collections.Generic;

namespace Pathfinding.ConsoleApp.Model
{
    public class VertexModel : ReactiveObject, IVertex, IEntity<int>
    {
        private IVertexCost cost;
        private bool isObstacle;

        public int Id { get; set; }

        public bool IsObstacle
        {
            get => isObstacle;
            set => this.RaiseAndSetIfChanged(ref isObstacle, value);
        }

        public IVertexCost Cost
        {
            get => cost;
            set => this.RaiseAndSetIfChanged(ref cost, value);
        }

        public Coordinate Position { get; }

        public ICollection<IVertex> Neighbours { get; } = new HashSet<IVertex>();

        public VertexModel(Coordinate coordinate)
        {
            Position = coordinate;
        }

        public bool Equals(IVertex other) => other.IsEqual(this);

        public override bool Equals(object obj) => obj is IVertex vertex && Equals(vertex);

        public override int GetHashCode() => Position.GetHashCode();
    }
}
