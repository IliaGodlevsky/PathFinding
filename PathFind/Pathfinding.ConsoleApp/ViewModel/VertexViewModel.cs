using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Visualization;
using Pathfinding.Shared.Primitives;
using ReactiveUI;
using System.Collections.Generic;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.ViewModel
{
    public class VertexViewModel : ReactiveObject, ITotallyVisualizable, IVertex, IEntity<int>
    {
        private IVertexCost cost;
        private Color color;
        private bool isObstacle;

        public int Id { get; set; }

        public bool IsObstacle
        {
            get => isObstacle;
            set => this.RaiseAndSetIfChanged(ref isObstacle, value);
        }

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

        public Coordinate Position { get; }

        public ICollection<IVertex> Neighbours { get; } = new HashSet<IVertex>();

        public VertexViewModel(Coordinate coordinate)
        {
            Position = coordinate;
        }

        public bool Equals(IVertex other) => other.IsEqual(this);

        public override bool Equals(object obj) => obj is IVertex vertex && Equals(vertex);

        public override int GetHashCode() => Position.GetHashCode();

        public bool IsVisualizedAsPath()
        {
            throw new System.NotImplementedException();
        }

        public void VisualizeAsPath()
        {
            throw new System.NotImplementedException();
        }

        public bool IsVisualizedAsRange()
        {
            throw new System.NotImplementedException();
        }

        public void VisualizeAsSource()
        {
            Color = Color.Magenta;
        }

        public void VisualizeAsTarget()
        {
            Color = Color.Red;
        }

        public void VisualizeAsTransit()
        {
            Color = Color.Green;
        }

        public void VisualizeAsVisited()
        {
            throw new System.NotImplementedException();
        }

        public void VisualizeAsEnqueued()
        {
            throw new System.NotImplementedException();
        }

        public void VisualizeAsObstacle()
        {
            Color = Color.Black;
        }

        public void VisualizeAsRegular()
        {
            Color = Color.White;
        }
    }
}
