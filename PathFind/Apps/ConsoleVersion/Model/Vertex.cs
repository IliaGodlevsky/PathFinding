using ConsoleVersion.View;
using GraphLib.Extensions;
using GraphLib.Interface;
using GraphLib.Realizations;
using GraphLib.Realizations.VertexCost;
using GraphLib.Serialization;
using GraphLib.Serialization.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using Console = Colorful.Console;

namespace ConsoleVersion.Model
{
    internal class Vertex : IVertex, IMarkableVertex, IWeightableVertex
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

        public string Text { get; set; }

        private IVertexCost cost;
        public IVertexCost Cost
        {
            get { return cost; }
            set
            {
                cost = value;
                (cost as Cost).UnweightedCostView = "#";
                Text = cost.ToString();
            }
        }

        public Color Colour { get; set; }

        public IList<IVertex> Neighbours { get; set; }

        public ICoordinate Position { get; set; }

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
            (cost as Cost).MakeUnWeighted();
            Text = cost.ToString();
        }

        public void MakeWeighted()
        {
            (cost as Cost).MakeWeighted();
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
                        var left = consolePosition.X + position.X * Constants.LateralDistanceBetweenVertices;
                        var top = consolePosition.Y + position.Y;
                        consoleCoordinate = new Coordinate2D(left, top);
                    }
                }
                return consoleCoordinate;
            }
        }
    }
}
