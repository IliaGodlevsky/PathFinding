using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SearchAlgorythms.Top
{
    public class GraphTop : Button, IGraphTop
    {      
        public GraphTop() : base()
        {
            Neighbours = new List<IGraphTop>();
            SetToDefault();
            IsObstacle = false;
        }

        public bool IsStart { get; set; }
        public bool IsEnd { get; set; }
        public bool IsVisited { get; set; }
        public int Value { get; set; }
        public IGraphTop ParentTop { get; set; }
        public List<IGraphTop> Neighbours { get; set; }
        public bool IsSimpleTop => !IsStart && !IsEnd;
        public bool IsObstacle { get; set; }

        public void MarkAsObstacle()
        {
            BackColor = Color.FromName("Black");
            Text = "";
            IsObstacle = true;
        }

        public void MarkAsGraphTop()
        {
            if (!IsObstacle)
                BackColor = Color.FromName("White");
        }

        public void MarkAsStart()
        {
            BackColor = Color.FromName("Green");
        }

        public void MarkAsEnd()
        {
            BackColor = Color.FromName("Red");
        }

        public void MarkAsVisited()
        {
            BackColor = Color.FromName("Yellow");
        }

        public void MarkAsPath()
        {
            BackColor = Color.FromName("Cyan");
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
        }
       
    }
}
