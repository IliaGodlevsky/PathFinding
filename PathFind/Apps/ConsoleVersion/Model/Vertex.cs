using ConsoleVersion.View;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using GraphLib.Realizations.VertexCost;
using GraphLib.Serialization;
using GraphLib.Serialization.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;

using Console = Colorful.Console;

using static ConsoleVersion.Constants;

namespace ConsoleVersion.Model
{
    internal class Vertex : IVertex, IMarkable, IWeightable
    {
        public event EventHandler OnVertexCostChanged;
        public event EventHandler OnEndPointChosen;
        public event EventHandler OnVertexReversed;

        public Vertex(INeighboursCoordinates coordinateRadar, ICoordinate coordinate)
        {
            NeighboursCoordinates = coordinateRadar;
            Position = coordinate;
            consoleCoordinates = new Lazy<Coordinate2D>(GetConsoleCoordinates);
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
                    Text = vertexCost.ToString();
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

        public void MarkAsEnd()
        {
            Colour = Color.FromKnownColor(KnownColor.Red);
            ColorizeVertex();
        }

        public void MarkAsRegular()
        {
            Colour = Color.FromKnownColor(KnownColor.White);
            ColorizeVertex();
        }

        public void MarkAsObstacle()
        {
            Colour = Color.FromKnownColor(KnownColor.Black);
        }

        public void MarkAsPath()
        {
            Colour = Color.FromKnownColor(KnownColor.Yellow);
            ColorizeVertex();
        }

        public void MarkAsStart()
        {
            Colour = Color.FromKnownColor(KnownColor.Green);
            ColorizeVertex();
        }

        public void MarkAsVisited()
        {
            Colour = Color.FromKnownColor(KnownColor.Blue);
            ColorizeVertex();
        }

        public void MarkAsEnqueued()
        {
            Colour = Color.FromKnownColor(KnownColor.Magenta);
            ColorizeVertex();
        }

        public void MakeUnweighted()
        {
            (cost as IWeightable)?.MakeUnweighted();
            Text = cost.ToString();
        }

        public void MakeWeighted()
        {
            (cost as IWeightable)?.MakeWeighted();
            Text = cost.ToString();
        }

        public void ColorizeVertex()
        {
            Console.SetCursorPosition(ConsoleCoordinate.X, ConsoleCoordinate.Y);
            Console.Write(Text, Colour);
        }

        public bool Equals(IVertex other)
        {
            return other.IsEqual(this);
        }

        private Coordinate2D GetConsoleCoordinates()
        {
            var position = Position as Coordinate2D;
            var consolePosition = MainView.GraphFieldPosition;
            var left = consolePosition.X + position.X * LateralDistanceBetweenVertices;
            var top = consolePosition.Y + position.Y;
            return new Coordinate2D(left, top);
        }

        private string Text { get; set; }
        private Color Colour { get; set; }
        private Coordinate2D ConsoleCoordinate => consoleCoordinates.Value;

        private readonly Lazy<Coordinate2D> consoleCoordinates;
    }
}
