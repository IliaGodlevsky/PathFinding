using ConsoleVersion.Interface;
using ConsoleVersion.View;
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
        public event EventHandler OnVertexCostChanged;
        public event EventHandler OnEndPointChosen;
        public event EventHandler OnVertexReversed;

        public Vertex(INeighborhood neighbourhood, ICoordinate coordinate)
        {
            Position = coordinate;
            this.Initialize();
            neighbours = new Lazy<IReadOnlyCollection<IVertex>>(() => neighbourhood.GetNeighbours(this));
        }

        public Vertex(VertexSerializationInfo info)
            : this(info.Neighbourhood, info.Position)
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

        public void ChangeCost() => OnVertexCostChanged?.Invoke(this, EventArgs.Empty);
        public void Reverse() => OnVertexReversed?.Invoke(this, EventArgs.Empty);
        public void SetAsExtremeVertex() => OnEndPointChosen?.Invoke(this, EventArgs.Empty);

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
            var consoleCoordinate = GetConsoleCoordinates();
            Console.SetCursorPosition(consoleCoordinate.X, consoleCoordinate.Y);
            Console.Write(text, Color);
        }

        public bool Equals(IVertex other) => other.IsEqual(this);
        public bool IsVisualizedAsPath => ColorsHub.IsVisualizedAsPath(this);
        public bool IsVisualizedAsEndPoint => ColorsHub.IsVisualizedAsEndPoint(this);
        public void VisualizeAsTarget() => ColorsHub.VisualizeAsTarget(this);
        public void VisualizeAsRegular() => ColorsHub.VisualizeAsRegular(this);
        public void VisualizeAsObstacle() => ColorsHub.VisualizeAsObstacle(this);
        public void VisualizeAsPath() => ColorsHub.VisualizeAsPath(this);
        public void VisualizeAsSource() => ColorsHub.VisualizeAsSource(this);
        public void VisualizeAsVisited() => ColorsHub.VisualizeAsVisited(this);
        public void VisualizeAsEnqueued() => ColorsHub.VisualizeAsEnqueued(this);
        public void VisualizeAsIntermediate() => ColorsHub.VisualizeAsIntermediate(this);
        public void VisualizeAsMarkedToReplaceIntermediate() => ColorsHub.VisualizeAsMarkedToReplaceIntermediate(this);

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
        private static readonly VerticesColorHub ColorsHub = new VerticesColorHub();
    }
}