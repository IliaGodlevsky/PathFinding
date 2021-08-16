using ConsoleVersion.View;
using ConsoleVersion.View.Interface;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using GraphLib.Realizations.VertexCost;
using GraphLib.Serialization;
using GraphLib.Serialization.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using Console = Colorful.Console;

namespace ConsoleVersion.Model
{
    internal class Vertex : IVertex, IMarkable, IWeightable, IDisplayable
    {
        private static readonly Color RegularVertexColor;
        private static readonly Color ObstacleVertexColor;
        private static readonly Color PathVertexColor;
        private static readonly Color EnqueuedVertexColor;
        private static readonly Color SourceVertexColor;
        private static readonly Color TargetVertexColor;
        private static readonly Color AlreadyPathVertexColor;
        private static readonly Color VisitedVertexColor;

        static Vertex()
        {
            RegularVertexColor = Color.FromKnownColor(KnownColor.WhiteSmoke);
            ObstacleVertexColor = Color.FromKnownColor(KnownColor.Black);
            PathVertexColor = Color.FromKnownColor(KnownColor.Yellow);
            EnqueuedVertexColor = Color.FromKnownColor(KnownColor.Magenta);
            SourceVertexColor = Color.FromKnownColor(KnownColor.Green);
            TargetVertexColor = Color.FromKnownColor(KnownColor.Red);
            AlreadyPathVertexColor = Color.FromKnownColor(KnownColor.Orange);
            VisitedVertexColor = Color.FromKnownColor(KnownColor.Blue);
        }

        public event EventHandler OnVertexCostChanged;
        public event EventHandler OnEndPointChosen;
        public event EventHandler OnVertexReversed;

        public Vertex(INeighboursCoordinates coordinateRadar, ICoordinate coordinate)
        {
            NeighboursCoordinates = coordinateRadar;
            Position = coordinate;
            this.Initialize();
        }

        public Vertex(VertexSerializationInfo info)
            : this(info.NeighboursCoordinates, info.Position)
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
                    MarkAsObstacle();
                }
            }
        }

        public INeighboursCoordinates NeighboursCoordinates { get; }

        private IVertexCost cost;
        public IVertexCost Cost
        {
            get => cost;
            set
            {
                if (value is WeightableVertexCost vertexCost)
                {
                    vertexCost.UnweightedCostView = "#";
                    text = vertexCost.ToString();
                }
                cost = value;
            }
        }

        public IReadOnlyCollection<IVertex> Neighbours { get; set; }

        public ICoordinate Position { get; }

        public void ChangeCost()
        {
            OnVertexCostChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Reverse()
        {
            OnVertexReversed?.Invoke(this, EventArgs.Empty);
        }

        public void SetAsExtremeVertex()
        {
            OnEndPointChosen?.Invoke(this, EventArgs.Empty);
        }

        public void MarkAsTarget() => Mark(TargetVertexColor);

        public void MarkAsRegular() => Mark(RegularVertexColor);

        public void MarkAsObstacle() => Mark(ObstacleVertexColor);

        public void MarkAsPath()
        {
            if (IsMarkedAsPath())
            {
                Mark(AlreadyPathVertexColor);
            }
            else
            {
                Mark(PathVertexColor);
            }
        }

        public void MarkAsSource() => Mark(SourceVertexColor);

        public void MarkAsVisited()
        {
            if (!IsMarkedAsPath())
            {
                Mark(VisitedVertexColor);
            }
        }

        public void MarkAsEnqueued()
        {
            if (!IsMarkedAsPath())
            {
                Mark(EnqueuedVertexColor);
            }
        }

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
            Console.Write(text, colour);
        }

        public bool Equals(IVertex other)
        {
            return other.IsEqual(this);
        }

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

        private void Mark(Color color)
        {
            this.colour = color;
            Display();
        }

        private bool IsMarkedAsPath()
        {
            return colour == PathVertexColor || colour == AlreadyPathVertexColor;
        }

        public void MarkAsIntermediate() { }

        private string text;
        private Color colour;
    }
}