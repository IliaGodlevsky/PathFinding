using SearchAlgorythms.Top;
using System;

namespace SearchAlgorythms
{
    [Serializable]
    public class VertexInfo
    {
        public VertexInfo(IVertex vertex)
        {
            IsObstacle = vertex.IsObstacle;            
            Text = vertex.Text;
        }

        public bool IsObstacle { get; set; }
        public string Text { get; set; }        
    }
}
