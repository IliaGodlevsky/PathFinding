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

        public ICollection<IVertex> Neighbours { get; set; }

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

        public void MarkAsTarget() => Mark(KnownColor.Red);

        public void MarkAsRegular() => Mark(KnownColor.White);

        public void MarkAsObstacle()
        {
            colour = Color.FromKnownColor(KnownColor.Black);
        }

        public void MarkAsPath() => Mark(KnownColor.Yellow);

        public void MarkAsSource() => Mark(KnownColor.Green);

        public void MarkAsVisited() => Mark(KnownColor.Blue);

        public void MarkAsEnqueued() => Mark(KnownColor.Magenta);

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

        private void Mark(KnownColor colour)
        {
            this.colour = Color.FromKnownColor(colour);
            Display();
        }

        private string text;
        private Color colour;
    }
}