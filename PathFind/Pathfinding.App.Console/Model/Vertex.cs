using Pathfinding.App.Console.Interface;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.NullObjects;
using Pathfinding.VisualizationLib.Core.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace Pathfinding.App.Console.Model
{
    [DebuggerDisplay("{Position.ToString()}")]
    internal class Vertex : IVertex, ITotallyVisualizable, IDisplayable
    {
        private readonly ITotalVisualization<Vertex> visualization;

        private IVertexCost cost = NullCost.Instance;
        private ConsoleColor color;

        public int Id { get; set; }

        public bool IsObstacle { get; set; }

        public IVertexCost Cost
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

        public ICollection<IVertex> Neighbours { get; set; } = new List<IVertex>();

        public ICoordinate Position { get; } = NullCoordinate.Interface;

        public Point? ConsolePosition { get; set; } = null;

        public Vertex(ICoordinate coordinate, ITotalVisualization<Vertex> visualization)
        {
            this.visualization = visualization;
            Position = coordinate;
        }

        public void Display()
        {
            if (ConsolePosition != null)
            {
                Cursor.SetPosition(ConsolePosition);
                Cursor.Write(Color, Cost);
            }
        }

        public bool Equals(IVertex other) => other.IsEqual(this);

        public override bool Equals(object obj) => obj is IVertex vertex && Equals(vertex);

        public override int GetHashCode() => HashCode.Combine(Cost, Position, IsObstacle);

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