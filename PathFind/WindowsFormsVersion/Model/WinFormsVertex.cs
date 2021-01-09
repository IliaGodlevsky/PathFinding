using Common;
using GraphLib.Coordinates.Abstractions;
using GraphLib.Extensions;
using GraphLib.Info;
using GraphLib.Vertex.Cost;
using GraphLib.Vertex.Interface;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsVersion.Model
{
    internal class WinFormsVertex : Label, IVertex
    {
        public WinFormsVertex() : base()
        {
            var fontSizeRatio = (VertexParametres.TextToSizeRatio * 0.75f);
            float fontSize = VertexParametres.VertexSize * fontSizeRatio;
            Font = new Font("Times New Roman", fontSize);
            var size = VertexParametres.VertexSize;
            Size = new Size(size, size);
            TextAlign = ContentAlignment.MiddleCenter;
            this.Initialize();
        }

        public WinFormsVertex(VertexInfo info) : this()
        {
            this.Initialize(info);
        }

        public bool IsStart { get; set; }

        public bool IsEnd { get; set; }

        public bool IsVisited { get; set; }

        public double AccumulatedCost { get; set; }

        public IVertex ParentVertex { get; set; }

        public IList<IVertex> Neighbours { get; set; }

        public bool IsObstacle { get; set; }

        public void MarkAsObstacle()
        {
            BackColor = Color.FromKnownColor(KnownColor.Black);
            this.WashVertex();
        }

        public void MarkAsSimpleVertex()
        {
            if (!IsObstacle)
            {
                BackColor = Color.FromKnownColor(KnownColor.WhiteSmoke);
            }
        }

        public void MarkAsStart()
        {
            BackColor = Color.FromKnownColor(KnownColor.Green);
        }

        public void MarkAsEnd()
        {
            BackColor = Color.FromKnownColor(KnownColor.Red);
        }

        public void MarkAsVisited()
        {
            BackColor = Color.FromKnownColor(KnownColor.CadetBlue);
        }

        public void MarkAsPath()
        {
            BackColor = Color.FromKnownColor(KnownColor.Yellow);
        }

        public void MarkAsEnqueued()
        {
            BackColor = Color.FromKnownColor(KnownColor.Magenta);
        }

        public void MakeUnweighted()
        {
            Text = string.Empty;
            cost.MakeUnWeighted();
        }

        public void MakeWeighted()
        {
            cost.MakeWeighted();
            Text = cost.ToString(string.Empty);
        }

        private VertexCost cost;
        public VertexCost Cost
        {
            get => cost;
            set
            {
                cost = (VertexCost)value.Clone();
                Text = cost.ToString(string.Empty);
            }
        }

        public ICoordinate Position { get; set; }

        public bool IsDefault => false;
    }
}
