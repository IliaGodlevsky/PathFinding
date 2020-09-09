using GraphLibrary.Constants;
using GraphLibrary.DTO;
using GraphLibrary.Extensions.CustomTypeExtensions;
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
            Font = new Font("Times New Roman", 8.0f);
            Size = new Size(VertexSize.VERTEX_SIZE,
                VertexSize.SIZE_BETWEEN_VERTICES);
            TextAlign = ContentAlignment.MiddleCenter;
            //BorderStyle = BorderStyle.FixedSingle;
        }

        public WinFormsVertex(VertexDto info) : this()
        {
            this.Initialize(info);
        }

        public bool IsStart { get; set; }
        public bool IsEnd { get; set; }
        public bool IsVisited { get; set; }
        public double AccumulatedCost { get; set; }
        public IVertex ParentVertex { get; set; }
        public List<IVertex> Neighbours { get; set; }
        public bool IsObstacle { get; set; }

        public void MarkAsObstacle()
        {
            BackColor = Color.FromKnownColor(KnownColor.Black);
            this.WashVertex();
        }

        public void MarkAsSimpleVertex()
        {
            if (!IsObstacle)
                BackColor = Color.FromKnownColor(KnownColor.White);
        }

        public void MarkAsStart() => BackColor = Color.FromKnownColor(KnownColor.Green);

        public void MarkAsEnd() => BackColor = Color.FromKnownColor(KnownColor.Red);

        public void MarkAsVisited() => BackColor = Color.FromKnownColor(KnownColor.CadetBlue);

        public void MarkAsPath() => BackColor = Color.FromKnownColor(KnownColor.Yellow);

        public VertexDto Info => new VertexDto(this);

        public int Cost { get => int.Parse(Text); set => Text = value.ToString(); }
    }
}
