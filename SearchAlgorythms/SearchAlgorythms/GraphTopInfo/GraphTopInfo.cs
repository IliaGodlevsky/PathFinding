using SearchAlgorythms.Top;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SearchAlgorythms
{
    [Serializable]
    public class GraphTopInfo
    {
        public GraphTopInfo(Button button)
        {
            IsObstacle = button as GraphTop == null;
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
