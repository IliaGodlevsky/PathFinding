using ConsoleVersion.View;
using GraphLib.Coordinates;
using GraphLib.Coordinates.Abstractions;
using GraphLib.Extensions;
using GraphLib.Info;
using GraphLib.Vertex.Cost;
using GraphLib.Vertex.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using Console = Colorful.Console;

namespace ConsoleVersion.Model
{
    internal class Vertex : IVertex
    {
        public event EventHandler OnExtremeVertexChosen;
        public event EventHandler OnCostChanged;
        public event EventHandler OnReverse;

        public Vertex()
        {
            this.Initialize();
        }

        public Vertex(VertexSerializationInfo info) : this()
        {
            this.Initialize(info);
        }

        public bool IsEnd { get; set; }

        public bool IsObstacle { get; set; }

        public bool IsStart { get; set; }

        public bool IsVisited { get; set; }

        public string Text { get; set; }

        private VertexCost cost;
        public VertexCost Cost
        {
            get { return cost; }
            set
            {
                cost = (VertexCost)value.Clone();
                Text = cost.ToString("#");
            }
        }

        public Color Colour { get; set; }

        public IList<IVertex> Neighbours { get; set; }

        public IVertex ParentVertex { get; set; }

        public double AccumulatedCost { get; set; }

        public ICoordinate Position { get; set; }

        public bool IsDefault => false;

        public void ChangeCost()
        {
            OnCostChanged?.Invoke(this, new EventArgs());
        }

        public void Reverse()
        {
            OnReverse?.Invoke(this, new EventArgs());
        }

        public void SetAsExtremeVertex()
        {
            OnExtremeVertexChosen?.Invoke(this, new EventArgs());
        }

        public void MarkAsEnd()
        {
            Colour = Color.FromKnownColor(KnownColor.Red);
            ColorizeVertex();
        }

        public void MarkAsSimpleVertex()
        {
            Colour = Color.FromKnownColor(KnownColor.White);
            ColorizeVertex();
        }

        public void MarkAsObstacle()
        {
            this.WashVertex();
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
            cost.MakeUnWeighted();
            Text = cost.ToString("#");
        }

        public void MakeWeighted()
        {
            cost.MakeWeighted();
            Text = cost.ToString("#");
        }

        public void Show()
        {
            ColorizeVertex();
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
                        const int DistanceBetweenVertices = 3;
                        var position = Position as Coordinate2D;
                        var consolePosition = MainView.GraphFieldBodyConsoleStartCoordinate;
                        var left = consolePosition.X + position.X * DistanceBetweenVertices;
                        var top = consolePosition.Y + position.Y;
                        consoleCoordinate = new Coordinate2D(left, top);
                    }
                }
                return consoleCoordinate;
            }
        }

        private void ColorizeVertex()
        {
            if (ConsoleCoordinate != null)
            {
                Console.SetCursorPosition(ConsoleCoordinate.X, ConsoleCoordinate.Y);
                Console.Write(Text, Colour);
            }
        }
    }
}
