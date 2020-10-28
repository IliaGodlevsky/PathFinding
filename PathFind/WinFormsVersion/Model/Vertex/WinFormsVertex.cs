using GraphLibrary.Coordinates.Interface;
using GraphLibrary.Extensions.CustomTypeExtensions;
using GraphLibrary.Globals;
using GraphLibrary.Info;
using GraphLibrary.Vertex.Cost;
using GraphLibrary.Vertex.Interface;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsVersion.Vertex
{
    internal class WinFormsVertex : Label, IVertex
    {      
        public WinFormsVertex() : base()
        {
            this.Initialize();
            float fontSize = VertexParametres.VertexSize * (VertexParametres.TextToSizeRatio * 0.75f);
            Font = new Font("Times New Roman", fontSize);
            Size = new Size(VertexParametres.VertexSize,
                VertexParametres.VertexSize);
            TextAlign = ContentAlignment.MiddleCenter;
            //BorderStyle = BorderStyle.FixedSingle;
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
                BackColor = Color.FromKnownColor(KnownColor.WhiteSmoke);
        }

        public void MarkAsStart() => BackColor = Color.FromKnownColor(KnownColor.Green);

        public void MarkAsEnd() => BackColor = Color.FromKnownColor(KnownColor.Red);

        public void MarkAsVisited() => BackColor = Color.FromKnownColor(KnownColor.CadetBlue);

        public void MarkAsPath() => BackColor = Color.FromKnownColor(KnownColor.Yellow);

        public void MarkAsEnqueued() => BackColor = Color.FromKnownColor(KnownColor.Aquamarine);

        public void MakeUnweighted()
        {
            Text = string.Empty;
            cost.MakeUnWeighted();
        }

        public void MakeWeighted()
        {
            cost.MakeWeighted();
            Text = ((int)cost).ToString(string.Empty);
        }

        public VertexInfo Info => new VertexInfo(this);

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
    }
}
