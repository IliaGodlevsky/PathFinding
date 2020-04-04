using System.Collections.Generic;
using System.Drawing;
using Console = Colorful.Console;

namespace SearchAlgorythms.Top
{
    public class ConsoleGraphTop : IGraphTop
    {
        public ConsoleGraphTop()
        {
            Neighbours = new List<IGraphTop>();
            SetToDefault();
            IsObstacle = false;
        }

        public ConsoleGraphTop(GraphTopInfo info) : this()
        {
            IsObstacle = info.IsObstacle;
            Text = info.Text;
            if (IsObstacle)
                MarkAsObstacle();
        }

        public bool IsEnd { get; set; }
        public bool IsObstacle { get; set; }

        public bool IsSimpleTop => !IsStart && !IsEnd;

        public bool IsStart { get; set; }
        public bool IsVisited { get; set; }
        public string Text { get; set; }
        public Color Colour { get; set; }
        public List<IGraphTop> Neighbours { get; set; }
        public IGraphTop ParentTop { get; set; }
        public double Value { get; set; }
        public Point Location { get; set; }

        public GraphTopInfo GetInfo()
        {
            return new GraphTopInfo(this);
        }

        public void MarkAsCurrentlyLooked()
        {
 
        }

        public void MarkAsEnd()
        {
            Colour = Color.FromKnownColor(KnownColor.Red);
        }

        public void MarkAsGraphTop()
        {
            Colour = Color.FromKnownColor(KnownColor.White);
        }

        public void MarkAsObstacle()
        {
            Text = " ";
            IsObstacle = true;
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
