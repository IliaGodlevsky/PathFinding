using Pathfinding.App.Console.Interface;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.NullObjects;
using Pathfinding.Visualization.Extensions;
using Pathfinding.VisualizationLib.Core.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace Pathfinding.App.Console.Model
{
    [DebuggerDisplay("{Position.ToString()}")]
    internal class Vertex : IVertex, IVisualizable, IDisplayable
    {
        private readonly IVisualization<Vertex> visualization;

        private bool isObstacle;

        public bool IsObstacle
        {
            get => isObstacle;
            set
            {
                isObstacle = value;
                if (isObstacle)
                {
                    VisualizeAsObstacle();
                }
                else
                {
                    VisualizeAsRegular();
                }
            }
        }

        public IVertexCost Cost { get; set; } = NullCost.Interface;

        public IList<IVertex> Neighbours { get; set; } = new List<IVertex>();

        public ICoordinate Position { get; } = NullCoordinate.Interface;

        public Point ConsolePosition { get; set; } = Point.Empty;

        public ConsoleColor Color { get; set; }

        public Vertex(ICoordinate coordinate, IVisualization<Vertex> visualization)
        {
            this.visualization = visualization;
            Position = coordinate;
            this.Initialize();
        }

        public void Display()
        {
            Cursor.SetPosition(ConsolePosition);
            using (Cursor.UseColor(Color))
            {
                System.Console.Write(Cost);
            }
        }

        public bool Equals(IVertex other) => other.IsEqual(this);

        public override bool Equals(object obj) => obj is IVertex vertex && Equals(vertex);

        public override int GetHashCode() => HashCode.Combine(Cost, Position);

        public bool IsVisualizedAsPath() => visualization.IsVisualizedAsPath(this);

        public bool IsVisualizedAsPathfindingRange() => visualization.IsVisualizedAsPathfindingRange(this);

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