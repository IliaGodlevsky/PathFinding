using ConsoleVersion.Interface;
using ConsoleVersion.Views;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using GraphLib.Realizations.VertexCost;
using GraphLib.Serialization;
using GraphLib.Serialization.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

using Console = Colorful.Console;

namespace ConsoleVersion.Model
{
    [DebuggerDisplay("{Position.ToString()}")]
    internal class Vertex : IVertex, IVisualizable, IWeightable, IDisplayable, IEquatable<IVertex>
    {
        public event EventHandler VertexCostChanged;
        public event EventHandler EndPointChosen;
        public event EventHandler VertexReversed;
        public event EventHandler MarkedToReplaceIntermediate;

        public Vertex(INeighborhood neighbourhood, ICoordinate coordinate, IVisualization<Vertex> visualization)
        {
            this.visualization = visualization;
            Position = coordinate;
            this.Initialize();
            neighbours = new Lazy<IReadOnlyCollection<IVertex>>(() => neighbourhood.GetNeighbours(this));
        }

        public Vertex(VertexSerializationInfo info, IVisualization<Vertex> visualization)
            : this(info.Neighbourhood, info.Position, visualization)
        {
            this.Initialize(info);
        }

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
            }
        }

        private IVertexCost cost;
        public IVertexCost Cost
        {
            get => cost;
            set
            {
                if (value is WeightableVertexCost vertexCost)
                {
                    vertexCost.UnweightedCostView = "#";
                    text = value.ToString();
                }
                cost = value;
            }
        }

        public IGraph Graph { get; }

        public Color Color { get; set; }
        public IReadOnlyCollection<IVertex> Neighbours => neighbours.Value;
        public ICoordinate Position { get; }

        public void OnVertexCostChanged() => VertexCostChanged?.Invoke(this, EventArgs.Empty);
        public void OnVertexReversed() => VertexReversed?.Invoke(this, EventArgs.Empty);
        public void OnEndPointChosen() => EndPointChosen?.Invoke(this, EventArgs.Empty);
        public void OnMarkedToReplaceIntermediate() => MarkedToReplaceIntermediate?.Invoke(this, EventArgs.Empty);

        public void MakeUnweighted()
        {
            (cost as IWeightable)?.MakeUnweighted();
            text = cost.ToString();
        }

        public void MakeWeighted()
        {
            (cost as IWeightable)?.MakeWeighted();
            text = cost.ToString();
        }

        public void Display()
        {
            lock (locker)
            {
                var consoleCoordinate = GetConsoleCoordinates();
                Console.SetCursorPosition(consoleCoordinate.X, consoleCoordinate.Y);
                Console.Write(text, Color);
            }
        }

        public bool Equals(IVertex other) => Equals((object)other);
        public override bool Equals(object obj) => obj is IVertex vertex && vertex.IsEqual(this);
        public override int GetHashCode() => base.GetHashCode();
        public bool IsVisualizedAsPath => visualization.IsVisualizedAsPath(this);
        public bool IsVisualizedAsEndPoint => visualization.IsVisualizedAsEndPoint(this);
        public void VisualizeAsTarget() => visualization.VisualizeAsTarget(this);
        public void VisualizeAsRegular() => visualization.VisualizeAsRegular(this);
        public void VisualizeAsObstacle() => visualization.VisualizeAsObstacle(this);
        public void VisualizeAsPath() => visualization.VisualizeAsPath(this);
        public void VisualizeAsSource() => visualization.VisualizeAsSource(this);
        public void VisualizeAsVisited() => visualization.VisualizeAsVisited(this);
        public void VisualizeAsEnqueued() => visualization.VisualizeAsEnqueued(this);
        public void VisualizeAsIntermediate() => visualization.VisualizeAsIntermediate(this);
        public void VisualizeAsMarkedToReplaceIntermediate() => visualization.VisualizeAsMarkedToReplaceIntermediate(this);

        private Coordinate2D GetConsoleCoordinates()
        {
            var consolePosition = MainView.GraphFieldPosition;
            int lateralDistance = MainView.GetLateralDistanceBetweenVertices();
            int x = Position.CoordinatesValues.FirstOrDefault();
            int y = Position.CoordinatesValues.LastOrDefault();
            int left = consolePosition.X + x * lateralDistance;
            int top = consolePosition.Y + y;
            return new Coordinate2D(left, top);
        }

        private string text;
        private readonly Lazy<IReadOnlyCollection<IVertex>> neighbours;
        private readonly IVisualization<Vertex> visualization;
        private static readonly object locker = new object();
    }
}