using Common.Extensions;
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
    internal class Vertex : IVertex, IVisualizable, IWeightable, IDisplayable
    {
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
                    VisualizeAsObstacle();
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

        public bool Equals(IVertex other)
        {
            return other.IsEqual(this);
        }

        private void Mark(Color color)
        {
            this.colour = color;
            Display();
        }

        public bool IsVisualizedAsPath => colour.IsOneOf(PathVertexColor, AlreadyPathVertexColor, IntermediateVertexColor);

        public bool IsVisualizedAsEndPoint => colour.IsOneOf(SourceVertexColor, TargetVertexColor, IntermediateVertexColor);

        public void VisualizeAsTarget()
        {
            Mark(TargetVertexColor);
        }

        public void VisualizeAsRegular()
        {
            Mark(RegularVertexColor);
        }

        public void VisualizeAsObstacle()
        {
            Mark(ObstacleVertexColor);
        }

        public void VisualizeAsPath()
        {
            if (IsVisualizedAsPath)
            {
                Mark(AlreadyPathVertexColor);
            }
            else
            {
                Mark(PathVertexColor);
            }
        }

        public void VisualizeAsSource()
        {
            Mark(SourceVertexColor);
        }

        public void VisualizeAsVisited()
        {
            if (!IsVisualizedAsPath)
            {
                Mark(VisitedVertexColor);
            }
        }

        public void VisualizeAsEnqueued()
        {
            if (!IsVisualizedAsPath)
            {
                Mark(EnqueuedVertexColor);
            }
        }

        public void VisualizeAsIntermediate()
        {
            Mark(IntermediateVertexColor);
        }

        public void VisualizeAsMarkedToReplaceIntermediate()
        {
            
        }

        private static readonly Color RegularVertexColor = Color.FromKnownColor(KnownColor.WhiteSmoke);
        private static readonly Color ObstacleVertexColor = Color.FromKnownColor(KnownColor.Black);
        private static readonly Color PathVertexColor = Color.FromKnownColor(KnownColor.Yellow);
        private static readonly Color EnqueuedVertexColor = Color.FromKnownColor(KnownColor.Magenta);
        private static readonly Color SourceVertexColor = Color.FromKnownColor(KnownColor.Green);
        private static readonly Color TargetVertexColor = Color.FromKnownColor(KnownColor.Red);
        private static readonly Color AlreadyPathVertexColor = Color.FromKnownColor(KnownColor.Orange);
        private static readonly Color VisitedVertexColor = Color.FromKnownColor(KnownColor.Blue);
        private static readonly Color IntermediateVertexColor = Color.FromKnownColor(KnownColor.DarkOrange);

        private string text;
        private Color colour;
    }
}