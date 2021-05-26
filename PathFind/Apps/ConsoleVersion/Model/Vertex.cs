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

namespace ConsoleVersion.Model
{
    internal class Vertex : IVertex, IMarkable, IWeightable
    {
        public event EventHandler OnVertexCostChanged;
        public event EventHandler OnEndPointChosen;
        public event EventHandler OnVertexReversed;

        public Vertex(INeighboursCoordinates coordinateRadar,
            ICoordinate coordinate)
        {
            this.Initialize();
            NeighboursCoordinates = coordinateRadar;
            Position = coordinate;
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

        public string Text { get; set; }

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
                    cost = vertexCost;
                }
            }
        }

        public Color Colour { get; set; }

        public ICollection<IVertex> Neighbours { get; set; }

        public ICoordinate Position { get; }

        public void ChangeCost()
        {
            OnVertexCostChanged?.Invoke(this, new EventArgs());
        }

        public void Reverse()
        {
            OnVertexReversed?.Invoke(this, new EventArgs());
        }

        public void SetAsExtremeVertex()
        {
            OnEndPointChosen?.Invoke(this, new EventArgs());
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
            if (ConsoleCoordinate != null)
            {
                Console.SetCursorPosition(ConsoleCoordinate.X, ConsoleCoordinate.Y);
                Console.Write(Text, Colour);
            }
        }

        public bool Equals(IVertex other)
        {
            return other.IsEqual(this);
        }

        private Coordinate2D consoleCoordinate;

        private Coordinate2D ConsoleCoordinate
        {
            get
            {
                if (consoleCoordinate == null)
                {
                    if (Position != null)
                    {
                        var position = Position as Coordinate2D;
                        var consolePosition = MainView.GraphFieldPosition;
                        if (consolePosition != null)
                        {
                            var left = consolePosition.X + position.X * Constants.LateralDistanceBetweenVertices;
                            var top = consolePosition.Y + position.Y;
                            consoleCoordinate = new Coordinate2D(left, top);
                        }
                    }
                }
                return consoleCoordinate;
            }
        }
    }
}
