using Pathfinding.App.Console.EventArguments;
using Pathfinding.App.Console.EventHandlers;
using Pathfinding.App.Console.Interface;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations.Coordinates;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Visualization.Extensions;
using Pathfinding.VisualizationLib.Core.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

using ColorfulConsole = Colorful.Console;

namespace Pathfinding.App.Console.Model
{
    [DebuggerDisplay("{Position.ToString()}")]
    internal class Vertex : IVertex, IVisualizable, IDisplayable
    {
        public event VertexEventHandler CostChanged;
        public event VertexEventHandler IncludedInRange;
        public event VertexEventHandler Reversed;
        public event VertexEventHandler MarkedAsIntermediateToReplace;

        private readonly IVisualization<Vertex> visualization;

        private bool isObstacle;

        public bool IsObstacle
        {
            get => isObstacle;
            set { isObstacle = value; if (isObstacle) VisualizeAsObstacle(); }
        }

        public IVertexCost Cost { get; set; }

        public IReadOnlyCollection<IVertex> Neighbours { get; set; }

        public ICoordinate Position { get; }

        public Coordinate2D ConsolePosition { get; set; } = Coordinate2D.Empty;

        public Color Color { get; set; }

        public Vertex(ICoordinate coordinate, IVisualization<Vertex> visualization)
        {
            this.visualization = visualization;
            Position = coordinate;
            this.InitializeComponents();
            this.Initialize();
        }

        public Vertex(VertexSerializationInfo info, IVisualization<Vertex> visualization)
            : this(info.Position, visualization)
        {
            this.Initialize(info);
        }

        public void Display()
        {
            Cursor.SetPosition(ConsolePosition);
            ColorfulConsole.Write(Cost, Color);
        }

        public void ChangeCost() => CostChanged?.Invoke(this, new VertexEventArgs(this));

        public void Reverse() => Reversed?.Invoke(this, new VertexEventArgs(this));

        public void IncludeInRange() => IncludedInRange?.Invoke(this, new VertexEventArgs(this));

        public void MarkAsIntermediateToReplace() => MarkedAsIntermediateToReplace?.Invoke(this, new VertexEventArgs(this));

        public bool Equals(IVertex other) => other.IsEqual(this);

        public override bool Equals(object obj) => obj is IVertex vertex && Equals(vertex);

        public override int GetHashCode() => HashCode.Combine(Cost, Position);

        public bool IsVisualizedAsPath() => visualization.IsVisualizedAsPath(this);

        public bool IsVisualizedAsEndPoint() => visualization.IsVisualizedAsEndPoint(this);

        public void VisualizeAsTarget() => visualization.VisualizeAsTarget(this);

        public void VisualizeAsRegular() => visualization.VisualizeAsRegular(this);

        public void VisualizeAsObstacle() => visualization.VisualizeAsObstacle(this);

        public void VisualizeAsPath() => visualization.VisualizeAsPath(this);

        public void VisualizeAsSource() => visualization.VisualizeAsSource(this);

        public void VisualizeAsVisited() => visualization.VisualizeAsVisited(this);

        public void VisualizeAsEnqueued() => visualization.VisualizeAsEnqueued(this);

        public void VisualizeAsIntermediate() => visualization.VisualizeAsIntermediate(this);

        public void VisualizeAsMarkedToReplaceIntermediate() => visualization.VisualizeAsMarkedToReplaceIntermediate(this);
    }
}