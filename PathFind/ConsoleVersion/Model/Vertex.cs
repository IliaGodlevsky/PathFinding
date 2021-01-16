using GraphLib.Coordinates.Abstractions;
using GraphLib.Extensions;
using GraphLib.Info;
using GraphLib.Vertex.Cost;
using GraphLib.Vertex.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;

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

        public Vertex(VertexInfo info) : this()
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
        }

        public void MarkAsSimpleVertex()
        {
            Colour = Color.FromKnownColor(KnownColor.White);
        }

        public void MarkAsObstacle()
        {
            this.WashVertex();
            Colour = Color.FromKnownColor(KnownColor.Black);
        }

        public void MarkAsPath()
        {
            Colour = Color.FromKnownColor(KnownColor.Yellow);
        }

        public void MarkAsStart()
        {
            Colour = Color.FromKnownColor(KnownColor.Green);
        }

        public void MarkAsVisited()
        {
            Colour = Color.FromKnownColor(KnownColor.Blue);
        }

        public void MarkAsEnqueued()
        {
            Colour = Color.FromKnownColor(KnownColor.Magenta);
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
    }
}
