using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SearchAlgorythms.Top
{
    public class GraphTop : Label, IGraphTop
    {      
        public GraphTop() : base()
        {
            Neighbours = new List<IGraphTop>();
            SetToDefault();
            IsObstacle = false;
        }

        public GraphTop(IGraphTopInfo info) : this()
        {
            IsObstacle = info.IsObstacle;
            Location = info.Location;
            Text = info.Text;
            BackColor = Color.FromName(info.Colour);
            SetToDefault();
        }

        public bool IsStart { get; set; }
        public bool IsEnd { get; set; }
        public bool IsVisited { get; set; }
        public double Value { get; set; }
        public IGraphTop ParentTop { get; set; }
        public List<IGraphTop> Neighbours { get; set; }
        public bool IsSimpleTop => !IsStart && !IsEnd;
        public bool IsObstacle { get; set; }

        public void MarkAsObstacle()
        {
            BackColor = Color.FromKnownColor(KnownColor.Black);
            Text = "";
            IsObstacle = true;
        }

        public void MarkAsGraphTop()
        {
            if (!IsObstacle)
                BackColor = Color.FromKnownColor(KnownColor.Ivory);
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
            BackColor = Color.FromKnownColor(KnownColor.Magenta);
        }

        public void MarkAsPath()
        {
            BackColor = Color.FromKnownColor(KnownColor.Yellow);
        }

        public IGraphTopInfo GetInfo()
        {
            return new GraphTopInfo(this);
        }

        public void SetToDefault()
        {
            IsStart = false;
            IsEnd = false;
            IsVisited = false;
            Value = 0;
            MarkAsGraphTop();
            ParentTop = null;
            Font = new Font("Tahoma", 8.0f);
            TextAlign = ContentAlignment.MiddleCenter;
            Size = new Size(27, 27);
            BorderStyle = BorderStyle.Fixed3D;
        }

    }
}
