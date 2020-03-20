using SearchAlgorythms.Top;
using System;
using System.Drawing;

namespace SearchAlgorythms
{
    [Serializable]
    public class GraphTopInfo : IGraphTopInfo
    {
        public GraphTopInfo(IGraphTop graphTop)
        {
            GraphTop button = graphTop as GraphTop;
            IsObstacle = graphTop.IsObstacle;
            if (IsObstacle)
                Colour = button.BackColor.Name;
            else
                Colour = "White";
            Location = button.Location;
            Text = button.Text;
        }

        public Point Location { get; set; }
        public string Colour { get; set; }
        public bool IsObstacle { get; set; }
        public string Text { get; set; }
    }
}
