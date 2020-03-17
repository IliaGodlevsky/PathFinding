using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorythms
{
    [Serializable]
    public class GraphTopInfo
    {
        public Point Location { get; set; }
        public string Colour { get; set; }
        public bool IsObstacle { get; set; }
    }
}
