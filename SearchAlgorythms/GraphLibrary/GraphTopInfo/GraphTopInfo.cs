using SearchAlgorythms.Top;
using System;

namespace SearchAlgorythms
{
    [Serializable]
    public class GraphTopInfo
    {
        public GraphTopInfo(IGraphTop graphTop)
        {
            IsObstacle = graphTop.IsObstacle;            
            Text = graphTop.Text;
        }

        public bool IsObstacle { get; set; }
        public string Text { get; set; }        
    }
}
