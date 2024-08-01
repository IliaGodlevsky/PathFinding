using Pathfinding.App.Console.Interface;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Service.Interface.Visualization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace Pathfinding.App.Console.Model
{
    [DebuggerDisplay("{Position.ToString()}")]
    internal class Vertex : IVertex, IEntity<int>, ITotallyVisualizable, IDisplayable
    {
        private readonly ITotalVisualization<Vertex> visualization;

        private IVertexCost cost = NullCost.Instance;
        private ConsoleColor color;

        public int Id { get; set; }

        public virtual bool IsObstacle { get; set; }

        public virtual IVertexCost Cost
        {
            get => cost;
            set
            {
                cost = value;
                Display();
            }
        }

        public ConsoleColor Color
        {
            get => color;
            set
            {
                color = value;
                Display();
            }
        }

        public virtual ICollection<IVertex> Neighbours { get; } = new HashSet<IVertex>();

        public virtual ICoordinate Position { get; } = NullCoordinate.Interface;

        public Point? ConsolePosition { get; set; } = null;

        public Vertex(ICoordinate coordinate, ITotalVisualization<Vertex> visualization)
        {
            this.visualization = visualization;
            Position = coordinate;
        }

        public void Display()
        {
            if (ConsolePosition.HasValue)
            {
                Cursor.SetPosition(ConsolePosition.Value);
                Cursor.Write(Color, Cost.CurrentCost);
            }
        }

        public bool Equals(IVertex other) => other.IsEqual(this);

        public override bool Equals(object obj) => obj is IVertex vertex && Equals(vertex);

        public override int GetHashCode() => Position.GetHashCode();

        public bool IsVisualizedAsPath() => visualization.IsVisualizedAsPath(this);

        public bool IsVisualizedAsRange() => visualization.IsVisualizedAsRange(this);

        public void VisualizeAsTarget() => visualization.VisualizeAsTarget(this);

        public void VisualizeAsRegular() => visualization.VisualizeAsRegular(this);

        public void VisualizeAsObstacle() => visualization.VisualizeAsObstacle(this);

        public void VisualizeAsPath() => visualization.VisualizeAsPath(this);

        public void VisualizeAsSource() => visualization.VisualizeAsSource(this);

        public void VisualizeAsVisited() => visualization.VisualizeAsVisited(this);

        public void VisualizeAsEnqueued() => visualization.VisualizeAsEnqueued(this);

        public void VisualizeAsTransit() => visualization.VisualizeAsTransit(this);
    }
}