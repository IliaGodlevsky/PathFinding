using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SearchAlgorythms.Top
{
    public class WinFormsVertex : Label, IVertex
    {      
        public WinFormsVertex() : base()
        {
            Neighbours = new List<IVertex>();
            SetToDefault();
            IsObstacle = false;
        }

        public WinFormsVertex(VertexInfo info) : this()
        {
            IsObstacle = info.IsObstacle;
            Text = info.Text;
            if (IsObstacle)
                MarkAsObstacle();
        }

        public bool IsStart { get; set; }
        public bool IsEnd { get; set; }
        public bool IsVisited { get; set; }
        public double Value { get; set; }
        public IVertex ParentTop { get; set; }
        public List<IVertex> Neighbours { get; set; }
        public bool IsSimpleVertex => !IsStart && !IsEnd;
        public bool IsObstacle { get; set; }

        public void MarkAsObstacle()
        {
            BackColor = Color.FromKnownColor(KnownColor.Black);
            Text = "";
            IsObstacle = true;
        }

        public void MarkAsSimpleVertex()
        {
            if (!IsObstacle)
                BackColor = Color.FromKnownColor(KnownColor.White);
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

        public VertexInfo GetInfo()
        {
            return new VertexInfo(this);
        }

        public void SetToDefault()
        {
            IsStart = false;
            IsEnd = false;
            IsVisited = false;
            Value = 0;
            MarkAsSimpleVertex();
            ParentTop = null;
            Font = new Font("Times New Roman", 7.8f);
            Size = new Size(25, 25);
            TextAlign = ContentAlignment.MiddleCenter;
            //BorderStyle = BorderStyle.FixedSingle;
        }

        public void MarkAsCurrentlyLooked()
        {
            BackColor = Color.FromKnownColor(KnownColor.DarkMagenta);
        }
    }
}
