using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Extensions;
using ReactiveUI;
using System.Collections.Generic;
using Terminal.Gui;

namespace Pathfinding.App.Console.ViewModel
{
    public class VertexViewModel : ReactiveObject, IVertex, IEntity<int>
    {
        private IVertexCost cost;
        private Color color;

        public int Id { get; set; }

        public bool IsObstacle { get; set; }

        public Color Color
        {
            get => color;
            set => this.RaiseAndSetIfChanged(ref color, value);
        }

        public IVertexCost Cost 
        { 
            get => cost;
            set => this.RaiseAndSetIfChanged(ref cost, value);
        }

        public ICoordinate Position { get; }

        public ICollection<IVertex> Neighbours { get; } = new HashSet<IVertex>();

        public VertexViewModel(ICoordinate coordinate)
        {
            Position = coordinate;
        }

        public bool Equals(IVertex other) => other.IsEqual(this);

        public override bool Equals(object obj) => obj is IVertex vertex && Equals(vertex);

        public override int GetHashCode() => Position.GetHashCode();
    }
}
