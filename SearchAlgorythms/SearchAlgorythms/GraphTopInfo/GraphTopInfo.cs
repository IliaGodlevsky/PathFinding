using SearchAlgorithms.Top;
using System;
using System.Drawing;

namespace SearchAlgorithms
{
    [Serializable]
    public class GraphTopInfo : IGraphTopInfo
    {
        public GraphTopInfo(IGraphTop graphTop)
        {
            IsObstacle = graphTop.IsObstacle;
            Colour = (graphTop as GraphTop).BackColor.Name;
            Location = graphTop.Location;
            Text = graphTop.Text;
        }

        public Point Location { get; set; }
        public string Colour { get; set; }
        public bool IsObstacle { get; set; }
        public string Text { get; set; }
    }
}
